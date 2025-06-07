using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    public GameManager.Ships shipType = GameManager.Ships.Luminaris;

    [SerializeField]
    public Rigidbody rb;

    public float forwardMultiplier     = 1.0f;
    public float sideMultiplier        = 1.0f;
    public float upMultiplier          = 1.0f;
    public float rollMultiplier        = 1.0f;
    public float spinMultiplier        = 1.0f;
    public float flipMultiplier        = 1.0f;
    public float fBrakeMultiplier      = 1.0f;
    public float tBrakeMultiplier      = 1.0f;
    public float boostMultiplier       = 1.0f;

    public Vector3 angVel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        angVel = rb.angularVelocity;
        if (InputBridge.forceBrake)
        {
            ApplyFBrakes(CalculateFBrakeForce(InputBridge.forceBrake));
        }
        if (InputBridge.torqueBrake)
        {
            ApplyTBrakes(CalculateTBrakeForce(InputBridge.torqueBrake));
        }
        if (InputBridge.forceBrake || InputBridge.torqueBrake)
        {
            return;
        }

        rb.AddRelativeForce(new Vector3(CalculateForwardThrust(InputBridge.forwardThrust, InputBridge.boost), 0));
        rb.AddRelativeForce(new Vector3(0, 0, CalculateSideThrust(InputBridge.sideThrust, InputBridge.boost)));
        rb.AddRelativeForce(new Vector3(0, CalculateUpThrust(InputBridge.upThrust, InputBridge.boost)));
        rb.AddRelativeTorque(new Vector3(CalculateRollTorque(InputBridge.rollTorque), 0));
        rb.AddRelativeTorque(new Vector3(0, CalculateSpinTorque(InputBridge.spinTorque)));
        rb.AddRelativeTorque(new Vector3(0, 0, CalculateFlipTorque(InputBridge.flipTorque)));
    }

    public virtual float CalculateForwardThrust(float _forwardThrust, float _boost)
    {
        float result = _forwardThrust * forwardMultiplier;
        if (Mathf.Abs(result) > 0)
        {
            result += (_boost * boostMultiplier);
        }
        return result;
    }

    public virtual float CalculateSideThrust(float _sideThrust, float _boost)
    {
        float result = _sideThrust * sideMultiplier;
        if (Mathf.Abs(result) > 0)
        {
            result += (_boost * boostMultiplier);
        }
        return result;
    }

    public virtual float CalculateUpThrust(float _upThrust, float _boost)
    {
        float result = _upThrust * upMultiplier;
        if (Mathf.Abs(result) > 0)
        {
            result += (_boost * boostMultiplier);
        }
        return result;
    }

    public virtual float CalculateRollTorque(float _rollTorque)
    {
        return (_rollTorque * rollMultiplier);
    }

    public virtual float CalculateSpinTorque(float _spinTorque)
    {
        return (_spinTorque * spinMultiplier);
    }

    public virtual float CalculateFlipTorque(float _flipTorque)
    {
        return (_flipTorque * flipMultiplier);
    }

    public virtual float CalculateFBrakeForce(bool _fBrakeOn)
    {
        if (_fBrakeOn)
        {
            return fBrakeMultiplier * -1;
        }
        return 0;
    }

    public virtual float CalculateTBrakeForce(bool _tBrakeOn)
    {
        if (_tBrakeOn)
        {
            return tBrakeMultiplier * -1;
        }
        else if (Mathf.Abs(rb.angularVelocity.x) < .5f)
        {
            rb.angularVelocity = new Vector3(DecreaseUntilZero(rb.angularVelocity.x, 0, .005f), DecreaseUntilZero(rb.angularVelocity.y, 0, .005f), DecreaseUntilZero(rb.angularVelocity.z, 0, .005f ));
        }
        return 0;
    }

    public virtual void ApplyFBrakes(float _calculatedBrakeForce)
    {
        rb.velocity = new Vector3(DecreaseUntilZero(rb.velocity.x, .05f, .1f), DecreaseUntilZero(rb.velocity.y, .05f, .1f), DecreaseUntilZero(rb.velocity.z, .05f, .1f));
    }

    public virtual void ApplyTBrakes(float _calculatedBrakeForce)
    {
        rb.angularVelocity = new Vector3(DecreaseUntilZero(rb.angularVelocity.x, .05f, .001f), DecreaseUntilZero(rb.angularVelocity.y, .05f, .001f), DecreaseUntilZero(rb.angularVelocity.z, .05f, .001f));
    }

    // Determines whether a float is close enough to 0.
    private bool IsItZero(float value, float tolerance)
    {
        if (Mathf.Abs(value) < tolerance)
        {
            return true;
        }
        return false;
    }

    private float DecreaseUntilZero(float value, float tolerance, float increment)
    {
        Debug.Log("Decrease until zero: value = " + value);
        // if value is negative, flip the operations
        if (value < 0)
        {
            tolerance *= -1;
            increment *= -1;
        }

        // is the value still above the tolerance?
        if (Mathf.Abs(value) > tolerance)
        {
            // is the next increment going to bring value below 0?
            if (Mathf.Abs(value - tolerance) < 0)
            {
                value = 0;
            }
            else
            {
                value -= increment;
            }
        }
        else
        {
            value = 0;
            return value;
        }
        return value;
    }
}
