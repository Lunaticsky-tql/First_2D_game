using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController:Singleton<SceneController>
{


    public AsyncOperation currentLoadOperation; 
    
    public void LoadScene(int target)
    {
        
        currentLoadOperation = SceneManager.LoadSceneAsync(target);
        SceneLoadPanel.Instance.UpdateProcess(currentLoadOperation);
        
    }


    public void LoadScene(int target, Action<AsyncOperation> onComplete) {
        currentLoadOperation = SceneManager.LoadSceneAsync(target);
      
        SceneLoadPanel.Instance.UpdateProcess(currentLoadOperation);
        currentLoadOperation.completed += onComplete;
    }
    
    public void LoadScene(int target ,string objName,string posName,bool isFlipX = false)
    {
        LoadScene(target, (asyncOperation) => {
            GameObject gameObject = GameObject.Find(objName);
            GameObject posObject = GameObject.Find(posName);

            gameObject.transform.position = posObject.transform.position;
            gameObject.transform.rotation = posObject.transform.rotation;

            if ( gameObject.GetComponent<SpriteRenderer>() != null )
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = isFlipX;
            }

        });
    }



}