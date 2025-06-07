using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    [Header("Entities")]
    [SerializeField]
    public GameObject camera;

    [SerializeField]
    FadeImage fadePanel;

    [SerializeField]
    GameObject Protag;
    [SerializeField]
    GameObject Villain;
    [SerializeField]
    GameObject BigBoss;
    [SerializeField]
    GameObject Planet;

    public enum CamTargets { Protag, Villain, BigBoss, Planet, Teammate }
    public enum CamAngles { UpperBackRight, ChaseView, LowerLeftDrop, /*FlyUp,*/DownTheStreet, FirstPerson, UpperFrontLeft, PassingBy, BirdsEye }
    List<CamAngles> DropCamAngles = new List<CamAngles>() { CamAngles.LowerLeftDrop, CamAngles.DownTheStreet, CamAngles.PassingBy };
    List<CamAngles> LookAtAngles = new List<CamAngles>() { CamAngles.DownTheStreet };

    [Header("Camera Control")]
    [SerializeField]
    public CamTargets target = CamTargets.Protag;
    [SerializeField]
    public CamAngles angle = CamAngles.UpperBackRight;
    public bool animInitialized = false;
    public bool animJustInit = false;

    CamAngles lastAngle = CamAngles.FirstPerson;
    CamAngles prevAngle = CamAngles.FirstPerson;
    CamTargets lastTarget = CamTargets.Planet;
    CamTargets currTarget = CamTargets.Protag;
    CamAngles currAngle = CamAngles.UpperBackRight;
    GameObject targetObj;
    CamAngle angleObj;
    CamTarget lookObj;
    CamSubTarget subLookObj;
    bool targetDirty = false;
    bool angleDirty = false;

    bool need2dropCam = false;

    // Shake Parameters
    private Vector3 originalCameraPos;
    public float shakeDuration = 2f;
    public float shakeAmount = 0.7f;

    private bool shaking = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //if (AnimationSequence.Instance.animate && !animInitialized)
        //{
        //    return;
        //}
        //if (animJustInit && AnimationSequence.Instance.animate)
        //{
        //    switch (AnimationSequence.Instance.currSection)
        //    {
        //        case AnimationSequence.LoadedSection.FullSong:
        //            ControlPanelFuncs.Luminaris1LLD();
        //            break;
        //        case AnimationSequence.LoadedSection.Intro:
        //            ControlPanelFuncs.PlanetGChase();
        //            break;
        //        case AnimationSequence.LoadedSection.Explosion:
        //            ControlPanelFuncs.Luminaris2FP();
        //            break;
        //    }
        //    animJustInit = false;
        //}
        //if ((!targetObj || target != currTarget) && !targetDirty)
        //{
        //    targetDirty = true;
        //}
        //if ((!angleObj || angle != currAngle) && !angleDirty)
        //{
        //    angleDirty = true;
        //}
        //if (targetDirty)
        //{
        //    UpdateTarget();
        //}
        //if (angleDirty)
        //{
        //    UpdateAngle();
        //}
        //if (Input.GetButtonDown("CameraSwap"))
        //{
        //    CameraSwap();
        //}
        //DoSpecialActions();
    }

    void CameraSwap()
    {
        if (angle == lastAngle)
        {
            angle = 0;
        }
        else
        {
            angle++;
        }

        angleDirty = true;
    }

    public void SetNewTargetObj(GameObject newTargetObj, CamTargets targetType)
    {
        switch (targetType)
        {
            case CamTargets.Protag:
                Protag = newTargetObj;
                break;
            case CamTargets.Teammate:
                Protag = newTargetObj;
                break;
            case CamTargets.Villain:
                Villain = newTargetObj;
                break;
            case CamTargets.BigBoss:
                BigBoss = newTargetObj;
                break;
            case CamTargets.Planet:
                Planet = newTargetObj;
                break;
        }
        targetDirty = true;
    }

    public void SetNewTarget(CamTargets newTarget)
    {
        target = newTarget;
        targetDirty = true;
    }

    public void SetNewAngle(CamAngles newAngle)
    {
        angle = newAngle;
        angleDirty = true;
    }

    public void ActivateFadePanel()
    {
        fadePanel.gameObject.SetActive(true);
    }

    public void FadeCamera()
    {
        fadePanel.StartFade();
    }

    public void FadeCamera(float fadeFrom, float fadeTo, float speed)
    {
        fadePanel.StartFade(fadeFrom, fadeTo, speed);
    }

    public void UpdateTarget()
    {
        UpdateTarget(target);
    }

    public void StartShaking()
    {
        if (!shaking)
        {
            originalCameraPos = transform.localPosition;
            shaking = true;
        }
    }
    public void StopShaking()
    {
        transform.localPosition = originalCameraPos;
    }

    private void DoShake()
    {
        transform.localPosition = originalCameraPos + Random.insideUnitSphere * shakeAmount;
    }


    void UpdateTarget(CamTargets newTarget)
    {
        //Set the gameObject that corresponds to the newTarget
        //update the flag to mark what the current target is
        switch (newTarget)
        {
            case CamTargets.Protag:
                targetObj = Protag;
                currTarget = CamTargets.Protag;
                break;
            case CamTargets.Villain:
                targetObj = Villain;
                currTarget = CamTargets.Villain;
                break;
            case CamTargets.BigBoss:
                targetObj = BigBoss;
                currTarget = CamTargets.BigBoss;
                break;
            case CamTargets.Planet:
                targetObj = Planet;
                currTarget = CamTargets.Planet;
                break;
        }
        //we want to update the angle now that the target is updated
        angleDirty = true;
        targetDirty = false;
    }

    void UpdateAngle()
    {
        UpdateAngle(angle);
    }

    void UpdateAngle(CamAngles newAngle)
    {
        //if we're changing from a "drop cam" angle, remove the old objects
        if (DropCamAngles.Contains(prevAngle))
        {
            camera.transform.parent = null;
            if (angleObj)
            {
                Destroy(angleObj.gameObject);
            }
        }
        prevAngle = angle;
        angleObj = GetAngleFromTarget(newAngle);
        if (!angleObj)
        {
            angleDirty = false;
            return;
        }
        currAngle = newAngle;
        if (DropCamAngles.Contains(currAngle))
        {
            need2dropCam = true;
        }
        ZeroOutCam();
        angleDirty = false;
    }

    void ZeroOutCam()
    {
        camera.transform.parent = angleObj.transform;
        camera.transform.localPosition = Vector3.zero;
        camera.transform.localRotation = Quaternion.identity;
        camera.transform.localScale = Vector3.one;
        angleDirty = false;
    }

    CamAngle GetAngleFromTarget(CamAngles desiredAngle)
    {
        CamAngle[] angles = targetObj.GetComponentsInChildren<CamAngle>();
        foreach (CamAngle angle in angles)
        {
            if (angle.angle == desiredAngle)
            {
                return angle;
            }
        }
        return null;
    }

    void DoSpecialActions()
    {
        if ((DropCamAngles.Contains(currAngle) && need2dropCam) || need2dropCam)
        {
            DropCam();
        }
        if (LookAtAngles.Contains(currAngle))
        {
            LookAtTarget();
        }
        else
        {
            lookObj = null;
        }
        if (shaking)
        {
            DoShake();
        }
    }

    void DropCam()
    {
        GameObject dupeAngleObj = Instantiate(angleObj.gameObject, angleObj.transform.parent);
        Destroy(dupeAngleObj.GetComponentInChildren<Camera>().gameObject);
        ZeroOutCam();
        angleObj.transform.parent = null;
        need2dropCam = false;
    }

    public void DropCamNow()
    {
        need2dropCam = true;
    }

    void LookAtTarget()
    {
        if (!lookObj)
        {
            lookObj = targetObj.GetComponentInChildren<CamTarget>();
        }
        if (lookObj && angleObj)
        {
            angleObj.transform.LookAt(lookObj.transform);
        }
    }

    void LookAtSubTarget()
    {
        if (!subLookObj)
        {
            subLookObj = targetObj.GetComponentInChildren<CamSubTarget>();
        }
        angleObj.transform.LookAt(subLookObj.transform);
    }
}
