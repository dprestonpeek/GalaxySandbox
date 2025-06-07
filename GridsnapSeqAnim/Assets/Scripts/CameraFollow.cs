using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    CamAngle camHolder;

    [SerializeField]
    Transform target;
    
    bool follow = false;

    // Start is called before the first frame update
    void Start()
    {
        if (camHolder != null)
        {
            transform.position = camHolder.transform.position;
            follow = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (follow)
        {
            //Invoke("InvokeFollow", .25f);

            transform.position = camHolder.transform.position;
            transform.rotation = camHolder.transform.rotation; 
            //transform.LookAt(target);
        }
    }

    private void InvokeFollow()
    {
        Follow(camHolder.transform.position);
    }

    private void Follow(Vector3 cmHldr)
    {
        Vector3 lerpedPosition = Vector3.Lerp(transform.position, cmHldr, Time.deltaTime * 50f);
        transform.position = lerpedPosition;

        transform.LookAt(target);
    }
}
