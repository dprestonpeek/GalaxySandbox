using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    Rigidbody rb;

    int frameDelay = 0;
    int maxFrameDelay = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (frameDelay > 0)
        {
            frameDelay++;
        }
        if (frameDelay >= maxFrameDelay)
        {
            Destroy(gameObject);
            frameDelay = 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 6 &&
            collision.gameObject.layer != 8 &&
            collision.gameObject.layer != 9)
        {
            frameDelay++;
        }
    }
}
