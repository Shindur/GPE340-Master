using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    //grab the transform of the camera
    public Transform PlayerTransform;
    //offset the camera
    private Vector3 cameraOffset;

    //makes it smoother (supposed to)
    [Range(0.01f, 1.0f)]
    public float SmoothnessFactor = 0.5f;

    //boolean to check if the camera is looking at the player
    public bool LookAtPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        //offsets the camera on load
        cameraOffset = transform.position - PlayerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //grabs the new position between the player and camera offset
        Vector3 newPos = PlayerTransform.position + cameraOffset;
        //interpolates the position of the transform
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothnessFactor);
        //If true, make the camera look at the player
        if(LookAtPlayer)
        {
            transform.LookAt(PlayerTransform);
        }
    }
}
