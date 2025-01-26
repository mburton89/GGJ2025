using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetParent : MonoBehaviour
{
    public Target linkedTarget;
    public Target linkedTargetArea;
    public ParticleSystem linkedParticleSystem;
    public MeshRenderer cyclinder;
    public GameObject linkedArrow;

    private void Start()
    {
        cyclinder.enabled = false;
    }

    public void DisableTargets()
    {
        linkedTarget.hasBeenHit = true;
        linkedTargetArea.hasBeenHit = true;
        linkedParticleSystem.Stop();
        linkedArrow.SetActive(false);
    }
}
