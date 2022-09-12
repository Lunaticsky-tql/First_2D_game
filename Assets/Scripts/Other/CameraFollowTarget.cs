using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public Transform target;
    public Vector2 rangeMin; //min range of camera to follow
    public Vector2 rangeMax; //max range of camera to follow
    private bool isFollowWithTime = false; //if true, camera will follow to the target slowly
    private float delayTime; // time to follow to the target
    private float timer;     // timer of delayTime
    private Vector3 startPos; //start position of camera
    private bool isChangeView; //if true, camera will change field of view
    Camera camera;//camera component
    
    public float defualtView;
    private float currentView;
    private float targetView;
    private Transform defaultTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = transform.GetComponent<Camera>() ;
        defualtView = camera.fieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        Follow();

    }
    public void Follow()
    {
        if ( target is null )
        {
            //no target to follow
            return; 
        }
        Vector3 targetPos;

        if (isFollowWithTime)
        {
            timer += Time.deltaTime;
            targetPos = Vector3.Lerp(startPos, target.position, timer / delayTime);
            targetPos.z = transform.position.z;

            if (isChangeView)
            {
                GetComponent<Camera>().fieldOfView = Mathf.Lerp(currentView, targetView, timer / delayTime);
                
            }

            if (timer / delayTime > 1) {
                isChangeView = false;
                isFollowWithTime = false;
            }

        }
        else {
            targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
        }

        transform.position = LimitPos( targetPos);


    }
    
    public Vector3 LimitPos(Vector3 targetPos)
    {
        targetPos.x = Mathf.Clamp(targetPos.x, rangeMin.x, rangeMax.x);
        targetPos.y = Mathf.Clamp(targetPos.y, rangeMin.y, rangeMax.y);
        return targetPos;
    }
    
    //directly follow to the target
    public void SetFollowTarget( Transform target )
    {
        isFollowWithTime = false;
        this.target = target;
    }

    //follow to target with delay time
    public void SetFollowTarget(Transform target, float time) {
        this.target = target;
        isFollowWithTime = true;
        timer = 0;
        delayTime = time;
        startPos = transform.position;
    }
    
    //follow target with delay time and change field of view
    public void SetFollowTarget(Transform target, float view, float time) {
        isChangeView = true;
        targetView = view;
        currentView = GetComponent<Camera>().fieldOfView;
        SetFollowTarget(target, time);

    }
}
