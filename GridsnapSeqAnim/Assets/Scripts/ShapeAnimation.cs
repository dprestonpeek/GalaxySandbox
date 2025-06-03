using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeAnimation : MonoBehaviour
{
    [Header("Rotation Control")]
    [SerializeField]
    [Range(-10, 10)]
    public float xRotationSpeed = 1;

    [SerializeField]
    [Range(-10, 10)]
    public float yRotationSpeed = 0;

    [SerializeField]
    [Range(-10, 10)]
    public float zRotationSpeed = 0;

    [SerializeField]
    [Range(.01f, 1)]
    public float rotationSmoothing = .1f;

    Vector3 rotLerp = Vector3.zero;

    [Header("Size Control")]
    [SerializeField]
    [Range(0, 100)]
    public float xSize = 40;

    [SerializeField]
    [Range(0, 100)]
    public float ySize = 40;

    [SerializeField]
    [Range(0, 100)]
    public float zSize = 40;

    [SerializeField]
    [Range(.01f, 1)]
    public float resizeSpeed = .1f;

    [SerializeField]
    public bool pulseShape = false;

    [SerializeField]
    public float pulseAmount = 0;

    [SerializeField]
    bool disappearing = false;

    [Header("Position Control")]
    public bool freezePositionIfBelowThreshold = false;
    public bool hideIfBelowThreshold = false;
    public float threshold;

    [HideInInspector]
    public bool shouldFreeze = false;

    [Header("Velocity Control")]
    [SerializeField]
    public bool toss = false;

    [SerializeField]
    [Range(0, 500)]
    public float force = 0;

    [Header("Color Control")]
    [SerializeField]
    public Color color;

    [SerializeField]
    public bool setColor = false;

    [SerializeField]
    [Range(.01f, 1)]
    public float colorSpeed = .1f;

    [HideInInspector]
    public Vector3 startSize = Vector3.one;
    Vector3 oldSize;
    Vector3 sizeLerp = Vector3.one;

    bool lerpingPos = false;
    Vector3 lerpPos = Vector3.zero;
    float lerpSpeed = 0;
    Vector3 startPos;
    bool pulseStatus = false;

    [HideInInspector]
    public Rigidbody rb;

    public Material mat;
    public Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        startSize = new Vector3(xSize, ySize, zSize);
        if (GetComponent<Renderer>())
        {
            renderer = GetComponent<Renderer>();
            mat = renderer.material;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RotateShape();
        if (pulseShape)
        {
            PulseShape(pulseAmount);
        }
        else
        {
            SizeShape();
        }

        if (lerpingPos)
        {
            LerpToPosition();
        }
    }

    private void Update()
    {
        if (freezePositionIfBelowThreshold)
        {
            if (transform.position.y < threshold - 10)
            {
                shouldFreeze = true;
                rb.velocity = Vector3.zero;
                rb.useGravity = false;
            }
            else
            {
                shouldFreeze = false;
            }
        }
        if (hideIfBelowThreshold)
        {
            if (transform.position.y < threshold - 10)
            {
                Disappear(resizeSpeed);
            }
        }
        if (disappearing)
        {
            Disappear(resizeSpeed);
        }
        if (setColor)
        {
            StartCoroutine(SetColor());
        }
    }

    public void StartPositionLerp(Vector3 position, float speed)
    {
        lerpingPos = true;
        lerpPos = position;
        lerpSpeed = speed;
    }

    public void ResetPosition()
    {
        transform.position = startPos;
    }

    public void SetUseGravity(bool useGravity)
    {
        rb.isKinematic = !useGravity;
        rb.useGravity = useGravity;
    }

    public void Toss(Vector3 velocity)
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
        rb.AddForce(velocity, ForceMode.Impulse);
        toss = false;
    }

    public void TossUp(float speed)
    {
        rb.velocity = Vector3.zero;
        rb.useGravity = true;
        rb.AddForce(new Vector3(0, speed, 0), ForceMode.Impulse);
        toss = false;
    }

    public void Disappear(float speed)
    {
        resizeSpeed = speed;
        oldSize = new Vector3(xSize, ySize, zSize);
        xSize = 0;
        ySize = 0;
        zSize = 0;
    }

    public void Delete()
    {
        Disappear(resizeSpeed);
        StartCoroutine(DestroyAfterDisappearing());
    }

    public void Reappear(float speed)
    {
        resizeSpeed = speed;
        xSize = startSize.x;
        ySize = startSize.y;
        zSize = startSize.z;
    }

    private void RotateShape()
    {
        rotLerp = new Vector3(Mathf.Lerp(rotLerp.x, xRotationSpeed, rotationSmoothing), Mathf.Lerp(rotLerp.y, yRotationSpeed, rotationSmoothing),
            Mathf.Lerp(rotLerp.z, zRotationSpeed, rotationSmoothing));
        transform.Rotate(Vector3.right, rotLerp.x);
        transform.Rotate(Vector3.up, rotLerp.y);
        transform.Rotate(Vector3.forward, rotLerp.z);
    }

    private void PulseShape(float amount)
    {
        if (pulseStatus)
        {
            SizeShape(startSize, resizeSpeed);
            pulseStatus = false;
        }
        else
        {
            SizeShape(new Vector3(transform.localScale.x + amount,
                transform.localScale.y + amount, transform.localScale.z + amount), resizeSpeed);
            pulseStatus = true;
        }
        pulseShape = false;
    }

    private void SizeShape()
    {
        SizeShape(new Vector3(xSize, ySize, zSize), resizeSpeed);
    }

    private void SizeShape(float speed)
    {
        SizeShape(new Vector3(xSize, ySize, zSize), speed);
    }

    private void SizeShape(Vector3 newSize, float speed) 
    {
        sizeLerp = new Vector3(Mathf.Lerp(sizeLerp.x, newSize.x, speed), Mathf.Lerp(sizeLerp.y, newSize.y, speed),
            Mathf.Lerp(sizeLerp.z, newSize.z, speed));
        Vector3 newScale = transform.localScale;
        newScale.x = sizeLerp.x;
        newScale.y = sizeLerp.y;
        newScale.z = sizeLerp.z;
        transform.localScale = newScale;
    }

    private void LerpToPosition()
    {
        if (transform.position != lerpPos)
        {
            transform.localPosition = Vector3.Lerp(new Vector3(transform.localPosition.x ,transform.localPosition.y, 0), lerpPos, lerpSpeed);
        }
        else
        {
            lerpingPos = false;
        }
    }

    private IEnumerator SetColor()
    {
        if (mat)
        {
            while (mat.color != color)
            {
                float r = Mathf.Lerp(mat.color.r, color.r, colorSpeed);
                float g = Mathf.Lerp(mat.color.g, color.g, colorSpeed);
                float b = Mathf.Lerp(mat.color.b, color.b, colorSpeed);
                float a = Mathf.Lerp(mat.color.a, color.a, colorSpeed);
                Color lerpedColor = new Color(r, g, b, a);
                mat.SetColor("_Color", lerpedColor);
                yield return null;
            }
        }
        setColor = false;
    }

    private IEnumerator DestroyAfterDisappearing()
    {
        while (disappearing)
        {
            yield return null;
        }
        Destroy(this);
    }
}
