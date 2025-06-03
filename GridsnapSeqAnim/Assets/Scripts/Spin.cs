using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField]
    public bool left = false;

    [SerializeField]
    [Range(0, 5)]
    public float spinSpeed = .1f;

    [SerializeField]
    public bool spinX;
    [SerializeField]
    public bool spinY;
    [SerializeField]
    public bool spinZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DoSpin();
    }

    void DoSpin()
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
}
