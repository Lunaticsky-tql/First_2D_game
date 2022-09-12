using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public static int keyCount = 0;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagConst.Player))
        {
            // get the key
            keyCount++;
            // get the hubdoor object 
            HubDoor hubDoor = GameObject.Find("HubDoor").GetComponent<HubDoor>();
            //make the key invisible
            gameObject.SetActive(false);

            if (hubDoor == null)
            {
                throw new System.Exception("no hubdoor in this scene!");
            }

            //set the player uncontrollable
            PlayerInput.instance.SetEnable(false);


            // make the camera focus to the door
            Camera.main.GetComponent<CameraFollowTarget>().SetFollowTarget(hubDoor.transform, 33, 1);

            //change the door's state after 1.5s
            Invoke(nameof(ChangeHubDoorStatus), 1.5f);
            // Reset the camera after 2s and destroy the key   
            Invoke(nameof(ResetToNormal), 2f);
        }
    }

    public void ChangeHubDoorStatus()
    {
        HubDoor hubDoor = GameObject.Find("HubDoor").GetComponent<HubDoor>();
        hubDoor.SetStatus((HubDoorStatus)keyCount);
    }

    public void ResetToNormal()
    {
        CameraFollowTarget cameraFollowTarget = Camera.main.GetComponent<CameraFollowTarget>();
        cameraFollowTarget.SetFollowTarget(GameObject.Find("Player/FollowTarget").transform,
            cameraFollowTarget.defualtView, 1);
        //reset the player's control
        PlayerInput.instance.SetEnable(true);
        // destroy the key
        Destroy(gameObject);
    }
}