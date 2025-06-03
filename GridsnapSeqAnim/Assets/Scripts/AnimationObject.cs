using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationObject : MonoBehaviour
{
    [HideInInspector]
    public static CannonBehavior cannon;
    [HideInInspector]
    public static GlowUp planetGlowUp;
    [HideInInspector]
    public static ExplosionManager explosionManager;
    [HideInInspector]
    public static ShockwaveManager shockwaveManager;
    [HideInInspector]
    public static Transform protag2;
    [HideInInspector]
    public static Move protag2Move;
    [HideInInspector]
    public static Ship protag2Ship;
    [HideInInspector]
    public static GameObject planet;
    [HideInInspector]
    public static GameObject protagHUD;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Cannon
    public void ShootCannon(bool continuous)
    {
        if (!cannon)
        {
            cannon = GetComponent<CannonBehavior>();
        }
        if (continuous)
        {
            cannon.StartContinuousFire();
        }
        else
        {
            cannon.Fire();
        }
    }

    public void StopContinuousFire()
    {
        if (!cannon)
        {
            cannon = GetComponent<CannonBehavior>();
        }
        cannon.StopContinuousFire();
    }
    #endregion
    #region GlowFX
    public void GlowUp()
    {
        if (!planetGlowUp)
        {
            planetGlowUp = GetComponent<GlowUp>();
        }
        planetGlowUp.StartGlowUp();
    }
    #endregion
    #region ExplosionFX
    public void SpawnExplosion(bool shockwave = true)
    {
        if (!explosionManager)
        {
            explosionManager = GetComponent<ExplosionManager>();
        }
        explosionManager.SpawnExplosion();
        if (shockwave)
        {
            ProduceShockwave();
        }
    }

    public void ProduceShockwave()
    {
        if (!shockwaveManager)
        {
            shockwaveManager = gameObject.GetComponent<ShockwaveManager>();
        }
        shockwaveManager.ProduceShockwave();
    }
    #endregion
    #region Movement
    public void StartMoving(float speed)
    {
        Move move = transform.GetComponentInChildren<Move>();
        if (move)
        {
            move.autoTravel = true;
            move.releaseControl = false;
            move.speed = speed;
        }
    }

    public void StopMoving()
    {
        Move move = transform.GetComponentInChildren<Move>();
        if (move)
        {
            move.autoTravel = false;
            move.releaseControl = true;
        }
    }

    public void PointTowards(Transform objToLookAt)
    {
        transform.LookAt(objToLookAt);
    }



    #endregion
    #region Protag
    public void ManualControl(bool doManualControl = true)
    {
        Move move = transform.GetComponentInChildren<Move>();
        if (move)
        {
            move.autoTravel = !doManualControl;
            move.releaseControl = !doManualControl;
        }
    }

    public void LogManualMovement()
    {

    }

    public void ShowHUD()
    {
        if (!protagHUD)
        {
            protagHUD = AnimationSequence.Instance.protag3.transform.Find("HUDDisplay").gameObject;
            protagHUD.SetActive(true);
        }
    }

    public void HideHUD()
    {
        if (!protagHUD)
        {
            protagHUD = AnimationSequence.Instance.protag3.transform.Find("HUDDisplay").gameObject;
            protagHUD.SetActive(false);
        }
    }

    public float GetSpeed()
    {
        Move move = transform.GetComponentInChildren<Move>();
        if (move)
        {
            return move.speed;
        }
        return 0;
    }

    public void SetSpeed(float speed)
    {
        Move move = transform.GetComponentInChildren<Move>();
        if (move)
        {
            move.speed = speed;
        }
    }

    //why does this exist
    public void AddRigidbody()
    {
        if (!protag2)
        {
            protag2 = transform;
        }
        if (!protag2Ship)
        {
            protag2Ship = protag2.GetComponentInChildren<Ship>();
        }
        Rigidbody rb = protag2.GetComponent<Rigidbody>();
        if (!rb)
        {
            rb = protag2.gameObject.AddComponent<Rigidbody>();
        }
        rb.useGravity = false;
        rb.mass = 1000;
    }
    #endregion
    #region Planet
    public void Disappear()
    {
        if (!planet)
        {
            planet = gameObject;
        }
        foreach (MeshRenderer renderer in planet.GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = false;
        }
    }
    #endregion
}
