using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Move : MonoBehaviour
{
    [Header("Automatic Travel")]
    [SerializeField]
    public bool autoTravel = true;
    public bool releaseControl = false;

    [SerializeField]
    Vector3 direction = Vector3.zero;

    [SerializeField]
    public float speed = 4.8f;

    Rigidbody rb;

    [Header("Manual Travel")]
    [SerializeField]
    public bool logManualMovement = false;
    [HideInInspector]
    public string logMovementFile = @"C:\Users\dpres\Documents\GitHub\GridsnapSeqAnim\GridsnapSeqAnim\Assets\MovementLogs\Luminaris3Movement.txt";
    List<string> movementLogs = new List<string>();

    public Vector3 forward;
    public Vector3 velocity;
    [SerializeField]
    GameObject left;
    [SerializeField]
    GameObject right;
    [SerializeField]
    GameObject up;
    [SerializeField]
    GameObject down;
    [SerializeField]
    GameObject forwd;
    [SerializeField]
    GameObject back;

    float hor = 0;
    float ver = 0;
    float fwd = 0;
    float pit = 0;
    float yaw = 0;
    float roll = 0;
    bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (autoTravel)
        {
            //ManualTravel(direction.x, direction.y, direction.z, 0, 0, 0, false);
            MoveInDirection(direction);
        }
        else if (releaseControl)
        {
            if (rb == null)
            {
                if (!GetRigidbody())
                {
                    AddRigidbody();
                }
            }
        }
        else
        {
            ControllerInput();
        }
    }

    private void FixedUpdate()
    {
        if (logManualMovement && !autoTravel)
        {
            if (File.Exists(logMovementFile))
            {
                File.WriteAllLines(logMovementFile, movementLogs);
            }
        }
    }

    void MoveInDirection(Vector3 direction)
    {
        Vector3 newPos = transform.localPosition;

        newPos.x += speed * Time.deltaTime * direction.x;
        newPos.y += speed * Time.deltaTime * direction.y;
        newPos.z += speed * Time.deltaTime * direction.z;
        transform.localPosition = newPos;
        //if (!GetRigidbody())
        //{
        //    AddRigidbody();
        //}
        //rb.AddForce(direction * speed);
    }

    void ControllerInput()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        fwd = Input.GetAxis("Forward");
        pit = Input.GetAxis("Pitch") * Time.deltaTime * speed * 2;
        yaw = Input.GetAxis("Yaw") * Time.deltaTime * speed * 2;
        roll = Input.GetAxis("Roll") * Time.deltaTime * speed * 2;
        stop = Input.GetButton("Stop");
        ManualTravel();
        if (logManualMovement && !autoTravel)
        {
            if (File.Exists(logMovementFile))
            {
                LogMovement();
            }
        }
    }

    public void ManualTravel()
    {
        if (rb == null)
        {
            if (!GetRigidbody())
            {
                AddRigidbody();
                //rb.constraints = RigidbodyConstraints.FreezeRotationX & RigidbodyConstraints.FreezeRotationY & RigidbodyConstraints.FreezeRotationZ;
                //rb.freezeRotation = true;
            }
        }

        if (stop)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, .05f * speed);
            Debug.Log("Stop");
        }
        else
        {
            Vector3 dir = Vector3.zero;
            if (hor < 0)
            {
                dir = (right.transform.position - transform.localPosition).normalized;
            }
            if (hor > 0)
            {
                dir = (left.transform.position - transform.localPosition).normalized;
            }
            if (ver < 0)
            {
                dir = (down.transform.position - transform.localPosition).normalized;
            }
            if (ver > 0)
            {
                dir = (up.transform.position - transform.localPosition).normalized;
            }
            if (fwd < 0)
            {
                dir = (back.transform.position - transform.localPosition).normalized;
            }
            if (fwd > 0)
            {
                dir = (forwd.transform.position - transform.localPosition).normalized;
            }
            if (roll > 0 || roll < 0)
            {
            }
            else
            {
                roll = 0;
            }
            rb.AddForce(dir);
        }

        transform.Rotate(Vector3.right, roll);
        transform.Rotate(Vector3.up, yaw);
        transform.Rotate(Vector3.forward, pit);

        forward = transform.localRotation.eulerAngles;
        velocity = rb.velocity;
    }

    private void LogMovement()
    {
        int ticks = AnimationSequence.Instance.ticks;
        movementLogs.Add(ticks + "," + hor + "," + ver + "," + fwd + "," + pit + "," + yaw
             + "," + roll + "," + stop);
    }

    public void ReleaseControl()
    {
        releaseControl = true;
    }

    void AddRigidbody()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    bool GetRigidbody()
    {
        rb = GetComponentInChildren<Rigidbody>();
        return rb;
    }
}
