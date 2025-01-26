using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTrickDetection : MonoBehaviour
{
    float totalXRotation;
    float totalYRotation;

    public static CarTrickDetection Instance;

    private void Awake()
    {
        Instance = this;
    }

    

    public void DetermineTrick()
    {
        // TRIX:
        // 360 on X = frontflip, -360 on X = backflip
        // +/- 360 on Y = sideflip
        // ~225 on X+Y = front diagonal flip, -225 on X+Y = back diagonal flip

        totalXRotation = GetComponentInParent<MovementController>().totalXRotation;
        totalYRotation = GetComponentInParent<MovementController>().totalYRotation;

        // Qualify car as having done a trick
        if (Mathf.Abs(totalXRotation) > 180 || Mathf.Abs(totalYRotation) > 180)
        {
            // A sane person would tell me to use a switch statement. Good thing we don't have any here.

            // Singles
            if (totalXRotation >= 300 && Mathf.Abs(totalYRotation) < 180)
            {
                print("Trick determined as: Frontflip!");
                FindObjectOfType<UIManager>().AdjustSlider(35f);
                FindObjectOfType<UIManager>().AddScore(30);
                FindObjectOfType<UIManager>().PauseFuelDrain();
            }
            if (totalXRotation <= -300 && Mathf.Abs(totalYRotation) < 180)
            {
                print("Trick determined as: Backflip!");
                FindObjectOfType<UIManager>().AdjustSlider(35f);
                FindObjectOfType<UIManager>().AddScore(45);
                FindObjectOfType<UIManager>().PauseFuelDrain();
            }
            if (Mathf.Abs(totalYRotation) >= 300 && Mathf.Abs(totalXRotation) < 240)
            {
                print("Trick determined as: Sideflip!");
                FindObjectOfType<UIManager>().AdjustSlider(35f);
                FindObjectOfType<UIManager>().AddScore(30);
                FindObjectOfType<UIManager>().PauseFuelDrain();
            }
            if (totalXRotation >= 180 && totalXRotation < 300 && Mathf.Abs(totalYRotation) >= 180 && Mathf.Abs(totalYRotation) < 300)
            {
                print("Trick determined as: Front Diagonal Flip!");
                FindObjectOfType<UIManager>().AdjustSlider(15f);
                FindObjectOfType<UIManager>().AddScore(5);
                FindObjectOfType<UIManager>().PauseFuelDrain();
            }
            if (totalXRotation <= -180 && totalXRotation > -300 && Mathf.Abs(totalYRotation) >= 180 && Mathf.Abs(totalYRotation) < 300)
            {
                print("Trick determined as: Back Diagonal Flip!");
                FindObjectOfType<UIManager>().AdjustSlider(15f);
                FindObjectOfType<UIManager>().AddScore(5);
                FindObjectOfType<UIManager>().PauseFuelDrain();
            }

            // Doubles
            if (totalXRotation >= 660 && Mathf.Abs(totalYRotation) < 180)
            {
                print("Trick determined as: Double Frontflip!");
                FindObjectOfType<UIManager>().AdjustSlider(50f);
                FindObjectOfType<UIManager>().AddScore(60);
                FindObjectOfType<UIManager>().PauseFuelDrain();

            }
            if (totalXRotation <= -660 && Mathf.Abs(totalYRotation) < 180)
            {
                print("Trick determined as: Double Backflip!");
                FindObjectOfType<UIManager>().AdjustSlider(50f);
                FindObjectOfType<UIManager>().AddScore(75);
                FindObjectOfType<UIManager>().PauseFuelDrain();
            }
            if (Mathf.Abs(totalYRotation) >= 660 && Mathf.Abs(totalXRotation) < 240)
            {
                print("Trick determined as: Double Sideflip!");
                FindObjectOfType<UIManager>().AdjustSlider(50f);
                FindObjectOfType<UIManager>().AddScore(60);
                FindObjectOfType<UIManager>().PauseFuelDrain();
            }
            if (totalXRotation >= 450 && totalXRotation < 600 && Mathf.Abs(totalYRotation) >= 450 && Mathf.Abs(totalYRotation) < 600)
            {
                print("Trick determined as: Double Front Diagonal Flip!");
                FindObjectOfType<UIManager>().AdjustSlider(40f);
                FindObjectOfType<UIManager>().AddScore(15);
                FindObjectOfType<UIManager>().PauseFuelDrain();

            }
            if (totalXRotation <= -450 && totalXRotation > -600 && Mathf.Abs(totalYRotation) >= 450 && Mathf.Abs(totalYRotation) < 600)
            {
                print("Trick determined as: Double Back Diagonal Flip!");
                FindObjectOfType<UIManager>().AdjustSlider(40f);
                FindObjectOfType<UIManager>().AddScore(15);
                FindObjectOfType<UIManager>().PauseFuelDrain();
            }

            // Triples and beyond aka "Masters"
            if (totalXRotation >= 1020 && Mathf.Abs(totalYRotation) < 180)
            {
                print("Trick determined as: Master Frontflip!");
                FindObjectOfType<UIManager>().AdjustSlider(100f);
                FindObjectOfType<UIManager>().AddScore(75);
                FindObjectOfType<UIManager>().PauseFuelDrain();
            }
            if (totalXRotation <= -1020 && Mathf.Abs(totalYRotation) < 180)
            {
                print("Trick determined as: Master Backflip!");
                FindObjectOfType<UIManager>().AdjustSlider(100f);
                FindObjectOfType<UIManager>().AddScore(100);
                FindObjectOfType<UIManager>().PauseFuelDrain();
            }
            if (Mathf.Abs(totalYRotation) >= 1020 && Mathf.Abs(totalXRotation) < 240)
            {
                print("Trick determined as: Master Sideflip!");
                FindObjectOfType<UIManager>().AdjustSlider(100f);
                FindObjectOfType<UIManager>().AddScore(75);
                FindObjectOfType<UIManager>().PauseFuelDrain();
            }
            if (totalXRotation >= 720 && totalXRotation < 1020 && Mathf.Abs(totalYRotation) >= 720 && Mathf.Abs(totalYRotation) < 1020)
            {
                print("Trick determined as: Master Front Diagonal Flip!");
                FindObjectOfType<UIManager>().AdjustSlider(100f);
                FindObjectOfType<UIManager>().AddScore(50);
                FindObjectOfType<UIManager>().PauseFuelDrain();
            }
            if (totalXRotation <= -720 && totalXRotation > -1020 && Mathf.Abs(totalYRotation) >= 720 && Mathf.Abs(totalYRotation) < 1020)
            {
                print("Trick determined as: Master Back Diagonal Flip!");
                FindObjectOfType<UIManager>().AdjustSlider(100f);
                FindObjectOfType<UIManager>().AddScore(50);
                FindObjectOfType<UIManager>().PauseFuelDrain();
            }


            print("Total trick X rotation: " + totalXRotation);
            print("Total trick Y rotation: " + totalYRotation);
        }

        // Reset total rotations calculated from last trick so it doesn't count for the next one
        totalXRotation = 0;
        totalYRotation = 0;
        GetComponentInParent<MovementController>().totalXRotation = 0;
        GetComponentInParent<MovementController>().totalYRotation = 0;
    }
}
