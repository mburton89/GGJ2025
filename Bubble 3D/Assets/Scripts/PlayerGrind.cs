using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

public class PlayerGrind : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] bool b_jump;         
    [SerializeField] Vector3 v3_input;

    [Header("Variables")]
    public bool b_onRail;
    [SerializeField] float f_grindSpeed;
    [SerializeField] float f_heightOffset;
    float f_timeForFullSpline;
    float f_elapsedTime;
    [SerializeField] float f_lerpSpeed = 10f;

    [Header("Scripts")]
    [SerializeField] RailScript rs_currentRailScript;
    Rigidbody rb_playerRigidbody;
    MovementController mc_Controller;

    private void Start()
    {
        rb_playerRigidbody = GetComponent<Rigidbody>();
        mc_Controller = GetComponent<MovementController>();
    }
    public void HandleJump(InputAction.CallbackContext context)
    {
        b_jump = Convert.ToBoolean(context.ReadValue<float>());
    }
    public void HandleMovement(InputAction.CallbackContext context)
    {
        Vector2 rawInput = context.ReadValue<Vector2>();
        v3_input.x = rawInput.x;
    }
    private void FixedUpdate()
    {
        if (b_onRail) 
        {
            MovePlayerAlongRail();
        }
    }
    private void Update()
    {

    }
    void MovePlayerAlongRail()
    {
        if (rs_currentRailScript != null && b_onRail) 
        {
            float progress = f_elapsedTime / f_timeForFullSpline;

            if (progress < 0 || progress > 1)
            {
                ThrowOffRail();
                return;
            }

            float nextTimeNormalised;
            if (rs_currentRailScript.b_normalDir)
                nextTimeNormalised = (f_elapsedTime + Time.deltaTime) / f_timeForFullSpline;
            else
                nextTimeNormalised = (f_elapsedTime - Time.deltaTime) / f_timeForFullSpline;


            float3 pos, tangent, up;
            float3 nextPosfloat, nextTan, nextUp;
            SplineUtility.Evaluate(rs_currentRailScript.sc_railSpline.Spline, progress, out pos, out tangent, out up);
            SplineUtility.Evaluate(rs_currentRailScript.sc_railSpline.Spline, nextTimeNormalised, out nextPosfloat, out nextTan, out nextUp);

            Vector3 worldPos = rs_currentRailScript.LocalToWorldConversion(pos);
            Vector3 nextPos = rs_currentRailScript.LocalToWorldConversion(nextPosfloat);

            transform.position = worldPos + (transform.up * f_heightOffset);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(nextPos - worldPos), f_lerpSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(transform.up, up) * transform.rotation, f_lerpSpeed * Time.deltaTime);

            if (rs_currentRailScript.b_normalDir)
                f_elapsedTime += Time.deltaTime;
            else
                f_elapsedTime -= Time.deltaTime;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Rail")
        {
            b_onRail = true;
            rs_currentRailScript = collision.gameObject.GetComponent<RailScript>();
            CalculateAndSetRailPosition();
        }
    }
    void CalculateAndSetRailPosition()
    {

        f_timeForFullSpline = rs_currentRailScript.f_totalSplineLength / f_grindSpeed;
        Vector3 splinePoint;

        float normalisedTime = rs_currentRailScript.CalculateTargetRailPoint(transform.position, out splinePoint);
        f_elapsedTime = f_timeForFullSpline * normalisedTime;

        float3 pos, forward, up;
        SplineUtility.Evaluate(rs_currentRailScript.sc_railSpline.Spline, normalisedTime, out pos, out forward, out up);

        rs_currentRailScript.CalculateDirection(forward, transform.forward);

        transform.position = splinePoint + (transform.up * f_heightOffset);
    }
    void ThrowOffRail()
    {

        b_onRail = false;
        rs_currentRailScript = null;
        transform.position += transform.forward * 1;
    }
}