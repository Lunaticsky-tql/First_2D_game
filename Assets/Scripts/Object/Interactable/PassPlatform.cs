using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassPlatform : MonoBehaviour
{

    int targetLayer; 
    PlatformEffector2D platformEffector;

    private void Start()
    {
        platformEffector = transform.GetComponent<PlatformEffector2D>();
    }

    // Make the player fall through the platform when this function is called
    public void Fall( GameObject target )
    {
        // Get the layer of the target
        targetLayer = 1 << target.layer;
        //Set the collider to ignore the target
        platformEffector.colliderMask &= ~targetLayer;
        // Reset the collider mask after 0.5 seconds
        Invoke(nameof(ResetLayer), 0.5f);
    }

    public void ResetLayer() {
        platformEffector.colliderMask |= targetLayer;
    }

}