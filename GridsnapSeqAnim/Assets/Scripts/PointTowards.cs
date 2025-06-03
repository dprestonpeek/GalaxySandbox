using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointTowards : MonoBehaviour
{
    [SerializeField]
    public GameObject objToRotate;

    [SerializeField]
    public Transform objToLookAt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (objToRotate)
        {
            objToRotate.transform.LookAt(objToLookAt);
        }
        else
        {
            transform.LookAt(objToLookAt);
        }
    }
}
