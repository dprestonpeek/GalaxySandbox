using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Starting Direction")]
    [SerializeField]
    public bool left = false;


    [Header("Spin Options")]
    [SerializeField]
    [Range(0, 5)]
    public float spinSpeed = .1f;

    [SerializeField]
    public bool spinX;
    [SerializeField]
    public bool spinY;
    [SerializeField]
    public bool spinZ;

    [Header("Pan Options")]
    [SerializeField]
    public bool panX;
    [SerializeField]
    public bool panY;
    [SerializeField]
    public bool panZ;

    [SerializeField]
    [Range(0, 5)]
    public float panSpeed = .1f;

    [SerializeField]
    [Range(0, 100)]
    public int panAmount = 10;


    float initY = 0;

    // Start is called before the first frame update
    void Start()
    {
        initY = transform.localEulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        Pan();
        Spin();
    }

    void Spin()
    {
        Vector3 newangle = transform.localEulerAngles;
        int dir = 1;
        if (left)
        {
            dir = -1;
        }
        else 
        {
            dir = 1;
        }
        if (spinX)
        {
            newangle.x += spinSpeed * Time.deltaTime * dir;
        }
        if (spinY)
        {
            newangle.y += spinSpeed * Time.deltaTime * dir;
        }
        if (spinZ)
        {
            newangle.z += spinSpeed * Time.deltaTime * dir;
        }
        transform.localEulerAngles = newangle;
    }

    void Pan()
    {
        if (left)
        {
            Vector3 newangle = transform.localEulerAngles;
            if (panX)
            {
                newangle.x += panSpeed * Time.deltaTime;
            }
            if (panY)
            {
                newangle.y += panSpeed * Time.deltaTime;
            }
            if (panZ)
            {
                newangle.z += panSpeed * Time.deltaTime;
            }
            transform.localEulerAngles = newangle;
            if (newangle.y >= Mathf.Abs(initY + panAmount))
            {
                left = false;
            }
        }
        if (!left)
        {
            Vector3 newangle = transform.localEulerAngles;
            if (panX)
            {
                newangle.x -= panSpeed * Time.deltaTime;
            }
            if (panY)
            {
                newangle.y -= panSpeed * Time.deltaTime;
            }
            if (panZ)
            {
                newangle.z -= panSpeed * Time.deltaTime;
            }
            transform.localEulerAngles = newangle;
            if (newangle.y <= Mathf.Abs(initY - panAmount))
            {
                left = true;
            }
        }
    }
}
