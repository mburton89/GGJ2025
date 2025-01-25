using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public TargetParent targetParent;
    [HideInInspector] public bool hasBeenHit;
    public void HandleHit()
    { 
        hasBeenHit = true;
        targetParent.DisableTargets();
    }
}
