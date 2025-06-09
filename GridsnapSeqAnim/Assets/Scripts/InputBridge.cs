using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class InputBridge : MonoBehaviour
{
    public static InputBridge Instance;

    public static float forwardThrust;
    public static float sideThrust;
    public static float upThrust;
    public static float rollTorque;
    public static float spinTorque;
    public static float flipTorque;
    public static float boost;
    public static bool forceBrake;
    public static bool torqueBrake;
    public static bool aim;
    public static bool shoot;
    public static bool target;
    public static bool freelook;
    public static bool pause;

    private Gamepad theGamepad;
    private Mouse theMouse;
    private Keyboard theKeyboard;

    bool mouseKeyboard = false;
    bool gamepad = false;

    enum InputType { Gamepad, MouseKeyboard, Both }
    InputType inputType = InputType.MouseKeyboard;

    // Start is called before the first frame update
    void Start()
    {
        theGamepad = Gamepad.current;
        theMouse = Mouse.current;
        theKeyboard = Keyboard.current;

        if (theMouse != null && theKeyboard != null) 
        {
            mouseKeyboard = true;
        }
        if (theGamepad != null)
        {
            gamepad = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        GetForwardThrust();
        GetSideThrust();
        GetUpThrust();
        GetRollTorque();
        GetSpinTorque();
        GetFlipTorque();
        GetBoost();
        GetForceBrake();
        GetTorqueBrake();
        GetAim();
        GetShoot();
        GetTarget();
        GetToggleFreeLook();
        GetPause();
    }

    private void GetForwardThrust()
    {
        if (gamepad)
        {
            //if (theGamepad.)
        }
        if (mouseKeyboard)
        {
            forwardThrust = GetNormalizedKeyValue(theKeyboard.wKey, theKeyboard.sKey);
        }
    }

    private void GetSideThrust()
    {
        if (gamepad)
        {

        }
        if (mouseKeyboard)
        {
            sideThrust = GetNormalizedKeyValue(theKeyboard.aKey, theKeyboard.dKey);
        }
    }

    private void GetUpThrust()
    {
        if (gamepad)
        {

        }
        if (mouseKeyboard)
        {
            upThrust = GetNormalizedKeyValue(theKeyboard.spaceKey, theKeyboard.ctrlKey);
        }
    }

    private void GetRollTorque()
    {
        if (gamepad)
        {

        }
        if (mouseKeyboard)
        {
            rollTorque = GetNormalizedKeyValue(theKeyboard.qKey, theKeyboard.eKey);
        }
    }

    private void GetSpinTorque()
    {
        if (gamepad)
        {

        }
        if (mouseKeyboard)
        {
            spinTorque = GetNormalizedHorMouse();
        }
    }

    private void GetFlipTorque()
    {
        if (gamepad)
        {

        }
        if (mouseKeyboard)
        {
            flipTorque = GetNormalizedVertMouse();
        }
    }

    private void GetBoost()
    {
        if (gamepad)
        {

        }
        if (mouseKeyboard)
        {
            boost = theKeyboard.shiftKey.isPressed ? 1 : 0;
        }
    }

    private void GetForceBrake()
    {
        if (gamepad)
        {

        }
        if (mouseKeyboard)
        {
            forceBrake = theKeyboard.xKey.isPressed;
        }
    }

    private void GetTorqueBrake()
    {
        if (gamepad)
        {

        }
        if (mouseKeyboard)
        {
            torqueBrake = theKeyboard.xKey.isPressed;
        }
    }

    private void GetShoot()
    {
        if (gamepad)
        {

        }
        if (mouseKeyboard)
        {
            shoot = theMouse.leftButton.isPressed;
        }
    }

    private void GetAim()
    {
        if (gamepad)
        {

        }
        if (mouseKeyboard)
        {
            aim = theMouse.rightButton.isPressed;
        }
    }

    private void GetTarget()
    {
        if (gamepad)
        {

        }
        if (mouseKeyboard)
        {
            target = theKeyboard.tKey.isPressed;
        }
    }

    private void GetToggleFreeLook()
    {
        if (gamepad)
        {

        }
        if (mouseKeyboard)
        {
            freelook = theKeyboard.zKey.wasPressedThisFrame;
            if (freelook)
            {
                Debug.Log("Freelooking");
            }
        }
    }

    private void GetPause()
    {
        if (gamepad)
        {

        }
        if (mouseKeyboard)
        {
            pause = theKeyboard.escapeKey.wasPressedThisFrame;
        }
    }

    private float GetNormalizedKeyValue(ButtonControl posButton, ButtonControl negButton)
    {
        if (posButton.isPressed && negButton.isPressed)
        {
            return 0f;
        }
        else if (posButton.isPressed)
        {
            return 1;
        }
        else if (negButton.isPressed)
        {
            return -1;
        }

        return 0f;
    }

    private float GetNormalizedHorMouse()
    {
        if (theMouse.delta.right.magnitude > 0)
        {
            return theMouse.delta.right.magnitude;
        }
        else if (theMouse.delta.left.magnitude > 0)
        {
            return theMouse.delta.left.magnitude * -1;
        }
        else
        {
            return 0;
        }
    }

    private float GetNormalizedVertMouse()
    {
        if (theMouse.delta.up.magnitude > 0)
        {
            return theMouse.delta.up.magnitude;
        }
        else if (theMouse.delta.down.magnitude > 0)
        {
            return theMouse.delta.down.magnitude * -1;
        }
        else
        {
            return 0;
        }
    }
}
