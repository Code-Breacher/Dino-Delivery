using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    //private float smoothTime = 0.25f;
    //private Vector3 velocity = Vector3.zero;

    public Transform player;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(player != null)
        {
            transform.position = player.transform.position + offset;
        }
        /*Vector3 targetPosition = player.transform.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);*/
        //transform.position = player.transform.position + new Vector3(0, 0, -10);
    }
}
