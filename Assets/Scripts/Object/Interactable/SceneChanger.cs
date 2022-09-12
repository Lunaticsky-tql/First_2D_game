using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{

    public int targetScene;

    public string posName; // the name of the position object in the next scene
    public string objName; // the name of the object to be moved to the position
    public bool isFlip;    // if the object should be flipped

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player") ) {
            Debug.Log("Change scene to "+targetScene);
            if (string.IsNullOrEmpty(posName) || string.IsNullOrEmpty(objName))
            {
                SceneController.Instance.LoadScene(targetScene);
            }
            else {
                SceneController.Instance.LoadScene(targetScene, objName, posName,isFlip);
            }

        }
    }


}