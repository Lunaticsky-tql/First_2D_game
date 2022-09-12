using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteOffsetWithMouse : MonoBehaviour
{
    // Start is called before the first frame update

    public float spriteScaler;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = startPosition + Input.mousePosition * spriteScaler;
    }
}
