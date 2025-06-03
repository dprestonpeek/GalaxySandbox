using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObj : MonoBehaviour
{
    [SerializeField]
    bool follow = false;

    [SerializeField]
    GameObject objToFollow;

    Vector3 prevObjToFollowPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            Vector3 newPos = transform.position;
            newPos.x += objToFollow.transform.position.x - prevObjToFollowPos.x;
            newPos.y += objToFollow.transform.position.y - prevObjToFollowPos.y;
            newPos.z += objToFollow.transform.position.z - prevObjToFollowPos.z;
            transform.position = newPos;
        }
        prevObjToFollowPos = objToFollow.transform.position;
    }
}
