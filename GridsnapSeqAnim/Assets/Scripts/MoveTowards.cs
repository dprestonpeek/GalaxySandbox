using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour
{
    [SerializeField]
    public GameObject objToMove;

    [SerializeField]
    public Transform objToMoveTowards;

    [SerializeField]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (objToMove)
        {
            objToMove.transform.position = Vector3.MoveTowards(objToMove.transform.position, objToMoveTowards.transform.position, speed * Time.deltaTime); ;
        }
    }
}
