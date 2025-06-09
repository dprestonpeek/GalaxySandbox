using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
    public GameManager.Ships shipType = GameManager.Ships.Luminaris;

    [SerializeField]
    public Rigidbody rb;

    [SerializeField]
    MenuController menu;

    public float forwardMultiplier     = 1.0f;
    public float sideMultiplier        = 1.0f;
    public float upMultiplier          = 1.0f;
    public float rollMultiplier        = 1.0f;
    public float spinMultiplier        = 1.0f;
    public float flipMultiplier        = 1.0f;
    public float fBrakeMultiplier      = 1.0f;
    public float tBrakeMultiplier      = 1.0f;
    public float boostMultiplier       = 1.0f;

    public bool freeLook = false;

    public Vector3 angVel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
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

        if (InputBridge.freelook)
        {
            if (freeLook)
            {
                freeLook = false;
                menu.ToggleFreeLook(false);
            }
            else
            {
                freeLook = true;
                menu.ToggleFreeLook(true);
            }

        }

        rb.AddRelativeForce(new Vector3(CalculateForwardThrust(InputBridge.forwardThrust, InputBridge.boost), 0));
        rb.AddRelativeForce(new Vector3(0, 0, CalculateSideThrust(InputBridge.sideThrust, InputBridge.boost)));
        rb.AddRelativeForce(new Vector3(0, CalculateUpThrust(InputBridge.upThrust, InputBridge.boost)));
        rb.AddRelativeTorque(new Vector3(CalculateRollTorque(InputBridge.rollTorque), 0));
        if (freeLook)
        {
            rb.AddRelativeTorque(new Vector3(0, CalculateSpinTorque(InputBridge.spinTorque)));
            rb.AddRelativeTorque(new Vector3(0, 0, CalculateFlipTorque(InputBridge.flipTorque)));
        }
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
        if (Mathf.Abs(_rollTorque) == 0) 
        {
            rb.angularVelocity = new Vector3(DecreaseUntilZero(rb.angularVelocity.x, 0, .005f ), rb.angularVelocity.y, rb.angularVelocity.z);
        }
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
            return fBrakeMultiplier;
        }
        return 0;
    }

    public virtual float CalculateTBrakeForce(bool _tBrakeOn)
    {
        if (_tBrakeOn)
        {
            return tBrakeMultiplier;
        }
        else if (Mathf.Abs(rb.angularVelocity.x) < .5f)
        {
            rb.angularVelocity = new Vector3(DecreaseUntilZero(rb.angularVelocity.x, .1f, tBrakeMultiplier), rb.angularVelocity.y, rb.angularVelocity.z);
        }
        return 0;
    }

    public virtual void ApplyFBrakes(float _calculatedBrakeForce)
    {
        float x = 0, y = 0, z = 0;
        if (Mathf.Abs(rb.velocity.x) > 0)
        {
            x = DecreaseUntilZero(rb.velocity.x, .1f, fBrakeMultiplier);
        }
        if (Mathf.Abs(rb.velocity.y) > 0)
        {
            y = DecreaseUntilZero(rb.velocity.y, .1f, fBrakeMultiplier);
        }
        if (Mathf.Abs(rb.velocity.z) > 0)
        {
            z = DecreaseUntilZero(rb.velocity.z, .1f, fBrakeMultiplier);
        }
        rb.velocity = new Vector3(x, y, z);
    }

    public virtual void ApplyTBrakes(float _calculatedBrakeForce)
    {
        float x = 0, y = 0, z = 0;
        if (Mathf.Abs(rb.angularVelocity.x) > 0)
        {
            x = DecreaseUntilZero(rb.angularVelocity.x, .1f, tBrakeMultiplier);
        }
        if (Mathf.Abs(rb.angularVelocity.y) > 0)
        {
            y = DecreaseUntilZero(rb.angularVelocity.y, .1f, tBrakeMultiplier);
        }
        if (Mathf.Abs(rb.angularVelocity.z) > 0)
        {
            z = DecreaseUntilZero(rb.angularVelocity.z, .1f, tBrakeMultiplier);
        }
        rb.angularVelocity = new Vector3(x, y, z);
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
        if (value == 0)
        {
            return value;
        }
        // if value is negative, flip the operations
        if (value < 0)
        {
            tolerance *= -1;
            increment *= -1;
        }
        // increment must not be higher than tolerance
        if (increment > tolerance)
        {
            tolerance = increment;
        }

        // is the value still above the tolerance?
        if (Mathf.Abs(value) > tolerance)
        {
            // is the next increment going to bring value below the tolerance?
            if (Mathf.Abs(value - increment) < tolerance)
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
