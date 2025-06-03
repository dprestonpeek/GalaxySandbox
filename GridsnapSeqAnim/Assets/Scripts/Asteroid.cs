using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [Header("Spin")]
    [SerializeField]
    bool spin = false;
    [SerializeField]
    Vector3 SpinVelocity = Vector3.zero;
    [SerializeField]
    bool randomizeSpin = false;
    [Header("Velocity")]
    [SerializeField]
    bool move = false;
    [SerializeField]
    Vector3 MoveVelocity = Vector3.zero;
    [SerializeField]
    bool randomizeMovement = false;
    [Header("Explosion")]
    [SerializeField]
    bool explode = false;
    [SerializeField]
    float explosionForce;
    [SerializeField]
    float explosionRadius;

    float spinMod = .5f;
    float moveMod = 2;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (spin)
        {
            if (randomizeSpin)
            {
                float hi = spinMod;
                float lo = spinMod * -1;
                SpinVelocity = new Vector3(Random.Range(lo, hi), Random.Range(lo, hi), Random.Range(lo, hi));
            }
        }
        if (move)
        {
            if (randomizeMovement)
            {
                float hi = moveMod;
                float lo = moveMod * -1;
                MoveVelocity = new Vector3(Random.Range(lo, hi), Random.Range(lo, hi), Random.Range(lo, hi));
            }
        }
        if (explode)
        {
            rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
        }
        rb.angularVelocity = SpinVelocity;
        rb.velocity = MoveVelocity;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
