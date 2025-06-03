using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GroupAnimation : MonoBehaviour
{
    [Header("Rotation Control")]
    [SerializeField]
    bool overrideRotation = true;

    [SerializeField]
    bool dontFaceAwayFromCamera = false;

    [SerializeField]
    [Range(-10, 10)]
    float xShapeRotationSpeed = 1;

    [SerializeField]
    [Range(-10, 10)]
    float yShapeRotationSpeed = 0;

    [SerializeField]
    [Range(-10, 10)]
    float zShapeRotationSpeed = 0;

    [SerializeField]
    [Range(.01f, 1)]
    float shapeRotationSmoothing = .5f;

    [SerializeField]
    [Range(-10, 10)]
    public float groupRotationSpeed = 0;

    [SerializeField]
    [Range(.01f, 1)]
    public float groupRotationSmoothing = .5f;

    public Vector3 shapeRotationSpeed { get { return new Vector3(xShapeRotationSpeed, yShapeRotationSpeed, zShapeRotationSpeed); } }
    float groupRot = 0;

    [Header("Size Control")]
    [SerializeField]
    bool overrideSize = true;

    [SerializeField]
    public bool resetSize = false;

    [SerializeField]
    public bool pulseIncremental = false;

    [SerializeField]
    public bool pulseGroup = false;

    [SerializeField]
    bool disappear = false;

    [SerializeField]
    bool reappear = false;

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
    [Range(0, 10)]
    public float pulseAmount = 0;

    [SerializeField]
    [Range(0, .1f)]
    public float pulseSpeed = .05f;

    [SerializeField]
    [Range(.01f, 1)]
    public float resizeSpeed = .1f;

    [Header("Position Control")]
    [SerializeField]
    public bool setPosition = false;

    [SerializeField]
    public bool overridePosition = false;

    [SerializeField]
    public Vector3 newPosition = Vector3.zero;

    [SerializeField]
    [Range(.01f, 1)]
    public float positionSpeed = .5f;

    [SerializeField]
    public bool freezePositionIfBelowThreshold = false;

    [SerializeField]
    public bool hideIfBelowThreshold = false;

    [SerializeField]
    public float threshold;

    [Header("Arrangement Control")]
    [SerializeField]
    public bool arrangeInCircle = false;

    [SerializeField]
    public bool arrangeInUnison = false;

    [SerializeField]
    public bool toggleArrangement = false;

    [SerializeField]
    public bool waitAndArrange = false;

    [SerializeField]
    private float timeToWait = 0;

    [SerializeField]
    [Range(0, 750)]
    public float circleRadius = 1;

    [SerializeField]
    [Range(.01f, 1)]
    public float arrangeSpeed = .1f;

    [Header("Velocity Control")]
    [SerializeField]
    public bool tossUp = false;

    [SerializeField]
    public bool explode = false;

    [SerializeField]
    [Range(0, 500)]
    public float force = 0f;


    [Header("Stagger Control")]
    [SerializeField]
    [Range(0, 1)]
    public float staggerLength = 1;

    [Header("Color Control")]
    [SerializeField]
    List<Color> colors;

    [SerializeField]
    public bool setColor = false;

    [SerializeField]
    public Material material;

    [SerializeField]
    public bool altMaterial = false;

    [SerializeField]
    public bool setMaterial = false;
    
    [Header("Video Control")]
    [SerializeField]
    public bool fadeToVideo = false;

    [SerializeField]
    public bool fadeToBG = false;

    [SerializeField]
    [Range(0.00001f, 1)]
    public float fadeSpeed = .4f;

    [SerializeField]
    [Range(0.00001f, 1)]
    public float fadeAmount = 1f;

    [Header("Glitch Control")]
    [SerializeField]
    public bool glitch = false;

    [SerializeField]
    public bool glitchWithColor = false;

    [SerializeField]
    private SpriteRenderer image;

    [SerializeField]
    [Range(.001f, 1)]
    public float glitchSpeed = 1;

    [SerializeField]
    [Range(0, 10)]
    public int glitchAmount = 0;

    [SerializeField]
    Sprite noglitch;

    [SerializeField]
    Sprite glitch1;

    [SerializeField]
    Sprite glitch2;

    int arrangeCounter = 0;
    [HideInInspector]
    public bool arrangingInward = false;
    [HideInInspector]
    public bool rotatingBackwards = false;
    [HideInInspector]
    public bool shrinking = false;
    [HideInInspector]
    public bool switchPos = false;
    [HideInInspector]
    public int switchPath = 0;
    [HideInInspector]
    public bool switchCircleStatus = false;
    [HideInInspector]
    public bool randomStep = false;

    [HideInInspector]
    public VideoPlayer video;

    [HideInInspector]
    public Vector3 originalPos;

    private Vector3 lerpStartPos;
    private bool removingShapes;

    private Vector3 initScale = Vector3.one;
    private Vector3 pulseScale = Vector3.one;
    private bool pulseStatus = false;

    [HideInInspector]
    public int increment = 0;

    [HideInInspector]
    public List<ShapeAnimation> shapes;
    List<int> shapesToCount;
    Dictionary<int, float> getShapeByKeyRange;
    // Start is called before the first frame update
    void Start()
    {
        initScale = transform.localScale;
        if (overrideSize)
        {
            SetStartSize();
        }
    }

    private void Awake()
    {
        shapes = new List<ShapeAnimation>();
        shapes.AddRange(GetComponentsInChildren<ShapeAnimation>());
        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //shapes = GetComponentsInChildren<ShapeAnimation>();
        SetThreshold();
        if (overrideRotation)
        {
            StartCoroutine(SetRotations(new Vector3(xShapeRotationSpeed, yShapeRotationSpeed, zShapeRotationSpeed), shapeRotationSmoothing, staggerLength));
        }
        if (dontFaceAwayFromCamera)
        {
            EnsureFacingCamera();
        }
        if (overrideSize)
        {
            StartCoroutine(SetSizes());
        }
        if (resetSize)
        {
            StartCoroutine(ResetSizes());
        }
        if (overridePosition)
        {
            StartCoroutine(SetShapePositions(newPosition, positionSpeed));
        }
        if (arrangeInCircle)
        {
            StartCoroutine(ArrangeInCircle(circleRadius, arrangeSpeed));
        }
        if (waitAndArrange)
        {
            StartCoroutine(WaitAndArrangeInCircle(circleRadius, arrangeSpeed, timeToWait));
        }
        if (arrangeInUnison)
        {
            StartCoroutine(ArrangeInCircle(0, arrangeSpeed));
        }
        if (pulseGroup)
        {
            StartCoroutine(PulseGroupItem(pulseAmount, pulseSpeed));
        }
        if (pulseIncremental)
        {
            StartCoroutine(PulseIncremental(pulseAmount, pulseSpeed));
        }
        if (disappear)
        {
            StartCoroutine(Disappear(resizeSpeed));
        }
        if (reappear)
        {
            StartCoroutine(Reappear(resizeSpeed));
        }
        if (glitch)
        {
            StartCoroutine(ShowGlitch(glitchSpeed, glitchAmount));
        }
        if (glitchWithColor)
        {
            StartCoroutine(ShowGlitchWithColor(glitchSpeed, glitchAmount));
        }
        if (fadeToVideo)
        {
            StartCoroutine(FadeVideo(true, fadeSpeed));
        }
        if (fadeToBG)
        {
            StartCoroutine(FadeVideo(false, fadeSpeed));
        }
        if (setColor)
        {
            SetColor();
        }
        if (setMaterial)
        {
            SetMaterial();
        }
        RotateGroup();
    }

    private void Update()
    {
        if (tossUp)
        {
            StartCoroutine(TossUp(force));
        }
        if (explode)
        {
            Explode(force);
        }
        if (toggleArrangement)
        {
            ToggleCircle(circleRadius, arrangeSpeed);
        }
        if (setPosition)
        {
            StartCoroutine(LerpToPosition(newPosition, positionSpeed));
        }
        if (freezePositionIfBelowThreshold)
        {
            foreach (ShapeAnimation shape in shapes)
            {
                shape.freezePositionIfBelowThreshold = true;
            }
        }
        if (hideIfBelowThreshold)
        {
            foreach (ShapeAnimation shape in shapes)
            {
                shape.hideIfBelowThreshold = true;
            }
        }
        
    }

    public void RemoveShapes(int shapesToRemove)
    {
        for (int i = 0; i < shapesToRemove; i++)
        {
            shapes.RemoveAt(shapes.Count - 1 - i);
        }
    }

    public IEnumerator SetRotations(Vector3 rotations, float smoothing, float staggerAmount)
    {
        foreach (ShapeAnimation shape in shapes)
        {
            shape.xRotationSpeed = rotations.x;
            shape.yRotationSpeed = rotations.y;
            shape.zRotationSpeed = rotations.z;
            shape.rotationSmoothing = smoothing;
            if (staggerAmount > 0)
            {
                yield return new WaitForSeconds(this.staggerLength);
            }
        }
    }

    public IEnumerator FadeVideo(bool fadein, float speed)
    {
        if (video == null)
        {
            video = GetComponent<VideoPlayer>();
        }
        if (fadein)
        {
            while (video.targetCameraAlpha < fadeAmount)
            {
                video.targetCameraAlpha += speed;
                yield return null;
            }
        }
        else
        {
            while (video.targetCameraAlpha > 0.02f)
            {
                video.targetCameraAlpha -= speed;
                yield return null;
            }
            video.targetCameraAlpha = 0.02f;
        }
        fadeToVideo = false;
        fadeToBG = false;
    }

    public void EnsureFacingCamera()
    {
        foreach (ShapeAnimation shape in shapes)
        {
            Vector3 newAngles = shape.transform.localEulerAngles;
            if (shape.transform.localEulerAngles.x > 90
                || shape.transform.localEulerAngles.x < -90)
            {
                newAngles.x *= -1;
            }
            if (shape.transform.localEulerAngles.y > 90
                || shape.transform.localEulerAngles.y < -90)
            {
                newAngles.y *= -1;
            }
            shape.transform.localEulerAngles = newAngles;
        }

    }

    IEnumerator SetSizes()
    {
        if (removingShapes)
            yield return null;
        foreach (ShapeAnimation shape in shapes)
        {
            shape.xSize = xSize;
            shape.ySize = ySize;
            shape.zSize = zSize;
            shape.resizeSpeed = resizeSpeed;
            yield return new WaitForSeconds(staggerLength);
        }
    }

    IEnumerator ResetSizes()
    {
        foreach (ShapeAnimation shape in shapes)
        {
            shape.xSize = shape.startSize.x;
            shape.ySize = shape.startSize.y;
            shape.zSize = shape.startSize.z;
            shape.resizeSpeed = resizeSpeed;
            yield return new WaitForSeconds(staggerLength);
        }
        resetSize = false;
    }

    private void RotateGroup()
    {
        groupRot = Mathf.Lerp(groupRot, groupRotationSpeed, groupRotationSmoothing);
        transform.Rotate(Vector3.forward, groupRot);
    }

    public void RotateGroup(float speed)
    {
        groupRot = Mathf.Lerp(groupRot, speed, groupRotationSmoothing);
        transform.Rotate(Vector3.forward, groupRot);
    }

    private IEnumerator LerpToPosition(Vector3 newPos, float speed)
    {
        lerpStartPos = transform.localPosition;
        bool move = true;
        while (move)
        {
            transform.localPosition = Vector3.Lerp(lerpStartPos, newPos, speed);
            if (Mathf.Abs(transform.localPosition.x - newPos.x) < .01f
                && Mathf.Abs(transform.localPosition.y - newPos.y) < .01f
                && Mathf.Abs(transform.localPosition.z - newPos.z) < .01f)
            {
                SetShapePositions(Vector3.zero, speed);
                setPosition = false;
                move = false;
            }
            yield return null;
        }
    }
    
    public IEnumerator SetShapePositions(Vector3 newPos, float speed)
    {
        foreach (ShapeAnimation shape in shapes)
        {
            shape.StartPositionLerp(newPos, speed);
            yield return new WaitForSeconds(staggerLength);
        }
        overridePosition = false;
    }

    public void ResetShapePositions()
    {
        foreach (ShapeAnimation shape in shapes)
        {
            shape.ResetPosition();
        }
    }

    public void ToggleCircle(float radius, float speed)
    {
        if (toggleArrangement)
        {
            if (switchCircleStatus)
            {
                StartCoroutine(ArrangeInCircle(0, speed));
                switchCircleStatus = false;
            }
            else
            {
                StartCoroutine(ArrangeInCircle(radius, speed));
                switchCircleStatus = true;
            }
        }
        toggleArrangement = false;
    }

    public IEnumerator ArrangeInCircle(float radius, float speed)
    {
        arrangeInUnison = false;
        for (int i = 0; i < shapes.Count; i++)
        {
            float angle = i * Mathf.PI * 2f / shapes.Count;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            shapes[i].StartPositionLerp(newPos, speed);
            yield return new WaitForSeconds(staggerLength);
        }
        arrangeInCircle = false;
    }

    public IEnumerator WaitAndArrangeInCircle(float radius, float speed, float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        StartCoroutine(ArrangeInCircle(radius, speed));
        waitAndArrange = false;
    }

    public bool ArrangeInCircleStep(float radius, float speed)
    {
        float angle = arrangeCounter * Mathf.PI * 2f / shapes.Count;
        Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
        shapes[arrangeCounter].StartPositionLerp(newPos, speed);
        if (arrangeCounter == shapes.Count - 1)
        {
            arrangeCounter = 0;
            return false;
        }
        else
        {
            arrangeCounter++;
        }
        return true;
    }

    public bool ArrangeInCircleStepRandom(float radius, float speed)
    {
        if (shapesToCount == null || shapesToCount.Count == 0)
        {
            shapesToCount = new List<int>();
            for (int i = 0; i < shapes.Count; i++)
            {
                shapesToCount.Add(i);
            }
        }
        int random;
        do
        {
            random = Random.Range(0, shapesToCount.Count);
        } while (!shapesToCount.Contains(random));


        if (shapesToCount.Count > 0)
        {
            float angle = random * Mathf.PI * 2f / shapes.Count;
            Vector3 newPos = new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            shapes[random].StartPositionLerp(newPos, speed);
            shapesToCount.Remove(random);
            return true;
        }
        return false;
    }

    public void ToggleCircleStepRandom(float radius, float speed)
    {
        if (switchCircleStatus)
        {
            if (!ArrangeInCircleStepRandom(0, speed))
            {
                switchCircleStatus = false;
            }
        }
        else
        {
            if (!ArrangeInCircleStepRandom(radius, speed))
            {
                switchCircleStatus = true;
            }            
        }
    }

    public IEnumerator ArrangeInUnison(float speed)
    {
        arrangeInCircle = false;
        for (int i = 0; i < shapes.Count; i++)
        {
            shapes[i].StartPositionLerp(new Vector3(transform.localPosition.x, transform.localPosition.y, 0), speed);
            yield return new WaitForSeconds(staggerLength);
        }
        arrangeInUnison = false;
    }

    public bool ArrangeInUnisonStep(float speed)
    {
        Vector3 newPos = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        shapes[arrangeCounter].StartPositionLerp(newPos, speed);
        if (arrangeCounter == shapes.Count - 1)
        {
            arrangeCounter = 0;
            return false;
        }
        else
        {
            arrangeCounter++;
        }
        return true;
    }

    public IEnumerator TossUp(float speed)
    {
        foreach(ShapeAnimation shape in shapes)
        {
            shape.TossUp(speed);
            yield return new WaitForSeconds(staggerLength);
        }
        tossUp = false;
    }

    public void TossUpSingleIncremental(float speed)
    {
        shapes[increment].TossUp(speed);
        increment++;
    }

    public void ResetIncremental()
    {
        increment = 0;
    }

    public void TossUpSingle(float speed, int keyMin, int keyMax, int currKey)
    {
        float range = (float)(keyMax - keyMin) / (float)shapes.Count;
        if (getShapeByKeyRange == null)
        {
            getShapeByKeyRange = new Dictionary<int, float>();
            for (int i = 0; i < shapes.Count; i++)
            {
                getShapeByKeyRange.Add(i, range * i);
            }
        }
        currKey -= keyMin;
        for (int i = 0; i < getShapeByKeyRange.Count; i++)
        {
            if (i == getShapeByKeyRange.Count - 1)
            {
                if (shapes[i].shouldFreeze)
                {
                    shapes[i].ResetPosition();
                    shapes[i].SetUseGravity(true);
                }
                shapes[i].TossUp(speed);
                break;
            }
            if (currKey >= getShapeByKeyRange[i] && currKey < getShapeByKeyRange[++i])
            {
                if (shapes[i].shouldFreeze)
                {
                    shapes[i].ResetPosition();
                    shapes[i].SetUseGravity(true);
                }
                shapes[i].TossUp(speed);
                break;
            }
        }
    }

    public void Explode(float forceToApply)
    {
        foreach (ShapeAnimation shape in shapes)
        {
            Vector3 force = new Vector3(Random.Range(forceToApply * -1, forceToApply), Random.Range(forceToApply * -1, forceToApply),
                Random.Range(forceToApply * -1, forceToApply));
            shape.Toss(force);
        }
        explode = false;
    }

    public IEnumerator ShowGlitch(float speed, int amount)
    {
        if (image != null && glitch1 != null && glitch2 != null)
        {
            for (int i = 0; i < amount; i++)
            {
                image.sprite = glitch1;
                yield return new WaitForSeconds(speed);
                image.sprite = glitch2;
                yield return new WaitForSeconds(speed);
            }
            image.sprite = noglitch;
            glitch = false;
        }
    }

    public IEnumerator ShowGlitchWithColor(float speed, int amount)
    {
        if (image != null && glitch1 != null && glitch2 != null && colors.Count >= 2)
        {
            for (int i = 0; i < amount; i++)
            {
                image.sprite = glitch1;
                image.color = colors[0];
                yield return new WaitForSeconds(speed);
                image.sprite = glitch2;
                image.color = colors[1];
                yield return new WaitForSeconds(speed);
            }
            image.sprite = noglitch;
            image.color = Color.white;
            glitch = false;
        }
        glitchWithColor = false;
    }

    public void Pulse(float pulseAmount)
    {
        foreach (ShapeAnimation shape in shapes)
        {
            shape.pulseAmount = pulseAmount;
            shape.pulseShape = true;
        }
    }

    public IEnumerator PulseIncremental(float amount, float speed)
    {
        Vector3 pulseScale = initScale * amount;
        pulseStatus = true;

        while (pulseStatus)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, pulseScale, speed);
            if (IsCloseEnough(transform.localScale, pulseScale))
            {
                pulseStatus = false;
            }
            else
            {
                yield return null;
            }
        }

        while (!pulseStatus)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, initScale, speed);
            if (IsCloseEnough(transform.localScale, initScale))
            {
                break;
            }
        }
        pulseGroup = false;
    }

    public IEnumerator PulseGroupItem(float amount, float speed)
    {
        Vector3 pulseScale = initScale * amount;
        pulseStatus = true;

        while (pulseStatus)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, pulseScale, speed);
            if (IsCloseEnough(transform.localScale, pulseScale))
            {
                pulseStatus = false;
            }
            else
            {
                yield return null;
            }
        }

        while (!pulseStatus)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, initScale, speed);
            if (IsCloseEnough(transform.localScale, initScale))
            {
                break;
            }
        }
        pulseGroup = false;
    }

    public IEnumerator Disappear(float speed)
    {
        overrideSize = false;
        foreach (ShapeAnimation shape in shapes)
        {
            shape.Disappear(speed);
            yield return new WaitForSeconds(staggerLength);
        }
        disappear = false;
    }

    public IEnumerator Reappear(float speed)
    {
        overrideSize = false;
        foreach (ShapeAnimation shape in shapes)
        {
            shape.Reappear(speed);
            yield return new WaitForSeconds(staggerLength);
        }
        reappear = false;
    }

    public void ToggleUseGravity()
    {
        foreach (ShapeAnimation shape in shapes) 
        {
            if (shape.rb.useGravity)
            {
                shape.rb.isKinematic = true;
                shape.rb.useGravity = false;
            }
            else
            {
                shape.rb.isKinematic = false;
                shape.rb.useGravity = true;
            }
        }
    }

    public void SetUseGravity(bool useGravity)
    {
        foreach (ShapeAnimation shape in shapes)
        {
            if (useGravity)
            {
                shape.rb.isKinematic = false;
                shape.rb.useGravity = true;
            }
            else
            {
                shape.rb.isKinematic = true;
                shape.rb.useGravity = false;
            }
        }
    }

    public void Delete(float speed)
    {
        StartCoroutine(Disappear(speed));
        foreach(ShapeAnimation shape in shapes)
        {
            shape.Delete();
        }
    }

    private void SetThreshold()
    {
        foreach (ShapeAnimation shape in shapes)
        {
            shape.threshold = threshold;
        }
    }

    private void SetMaterial()
    {
        bool alternate = true;
        foreach (ShapeAnimation shape in shapes)
        {
            if (alternate)
            {
                shape.renderer.material = material;
                //shape.mat.SetTexture("_MainText", material);
            }
            if (altMaterial)
            {
                alternate = !alternate;
            }
        }
        setMaterial = false;
    }

    private void SetColor()
    {
        SetColor(colors);
    }

    private void SetColor(Color color)
    {
        List<Color> colors = new List<Color>();
        colors.Add(color);
        SetColor(colors);
    }

    private void SetColor(List<Color> colors)
    {
        if (colors == null)
        {
            colors = new List<Color>();
        }

        int colorNum = 0;
        foreach (ShapeAnimation shape in shapes)
        {
            if (colors.Count > 0)
            {
                shape.color = colors[colorNum];
                shape.setColor = true;
            }
            if (colorNum < colors.Count - 1)
            {
                colorNum++;
            }
            else
            {
                colorNum = 0;
            }
        }
    }

    private void SetStartSize()
    {
        foreach (ShapeAnimation shape in shapes)
        {
            shape.startSize = new Vector3(xSize, ySize, zSize);
        }
    }

    private bool IsCloseEnough(Vector3 toCompare, Vector3 comparison)
    {
        if (Mathf.Abs(toCompare.x - comparison.x) < .01f
                   && Mathf.Abs(toCompare.y - comparison.y) < .01f
                   && Mathf.Abs(toCompare.z - comparison.z) < .01f)
        {
            return true;
        }
        return false;
    }
}
