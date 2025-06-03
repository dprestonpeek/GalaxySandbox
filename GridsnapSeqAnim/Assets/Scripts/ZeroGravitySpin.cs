using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeroGravitySpin : MonoBehaviour
{
    [SerializeField]
    public Vector3 SpinVelocity = Vector3.zero;
    [SerializeField]
    public bool randomize = false;

    float randomMod = .5f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (randomize)
        {
            float hi = randomMod;
            float lo = randomMod * -1;
            SpinVelocity = new Vector3(Random.Range(lo, hi), Random.Range(lo, hi), Random.Range(lo, hi));
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.angularVelocity = SpinVelocity;
    }
}
