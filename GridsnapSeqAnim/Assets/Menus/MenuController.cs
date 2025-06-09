using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    Luminaris ship;

    [SerializeField]
    GameObject menu;

    [SerializeField]
    TMP_InputField fwd;

    [SerializeField]
    TMP_InputField side;

    [SerializeField]
    TMP_InputField up;

    [SerializeField]
    TMP_InputField roll;

    [SerializeField]
    TMP_InputField spin;

    [SerializeField]
    TMP_InputField flip;

    [SerializeField]
    TMP_InputField boost;

    [SerializeField]
    TMP_InputField fBrake;

    [SerializeField]
    TMP_InputField tBrake;

    [SerializeField]
    TMP_Text linearVelocity;

    [SerializeField]
    TMP_Text angularVelocity;

    [SerializeField]
    TMP_Text freeLookEnabled;

    // Start is called before the first frame update
    void Start()
    {
        ship.forwardMultiplier = PlayerPrefs.GetFloat("fwd", 5);
        fwd.text = ship.forwardMultiplier.ToString();
        ship.sideMultiplier = PlayerPrefs.GetFloat("side", 5);
        side.text = ship.sideMultiplier.ToString();
        ship.upMultiplier = PlayerPrefs.GetFloat("up", 5);
        up.text = ship.upMultiplier.ToString();
        ship.rollMultiplier = PlayerPrefs.GetFloat("roll", 5);
        roll.text = ship.rollMultiplier.ToString();
        ship.spinMultiplier = PlayerPrefs.GetFloat("spin", 10);
        spin.text = ship.spinMultiplier.ToString();
        ship.flipMultiplier = PlayerPrefs.GetFloat("flip", 10);
        flip.text = ship.flipMultiplier.ToString();
        ship.fBrakeMultiplier = PlayerPrefs.GetFloat("fBrake", .1f);
        fBrake.text = ship.fBrakeMultiplier.ToString();
        ship.tBrakeMultiplier = PlayerPrefs.GetFloat("tBrake", .1f);
        tBrake.text = ship.tBrakeMultiplier.ToString();
        ship.boostMultiplier = PlayerPrefs.GetFloat("boost", 5);
        boost.text = ship.boostMultiplier.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputBridge.pause) 
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
            }
            else
            {
                menu.SetActive(true);
            }
        }
        linearVelocity.text = ship.rb.velocity.ToString();
        angularVelocity.text = ship.rb.angularVelocity.ToString();
    }

    public void UpdateFwd()
    {
        try
        {
            ship.forwardMultiplier = float.Parse(fwd.text);
            PlayerPrefs.SetFloat("fwd", ship.forwardMultiplier);
        }
        catch(Exception ex)
        {

        }
    }

    public void UpdateSide()
    {
        try
        {
            ship.sideMultiplier = float.Parse(side.text);
            PlayerPrefs.SetFloat("side", ship.sideMultiplier);
        }
        catch (Exception ex)
        {

        }
    }

    public void UpdateUp()
    {
        try
        {
            ship.upMultiplier = float.Parse(up.text);
            PlayerPrefs.SetFloat("up", ship.upMultiplier);
        }
        catch (Exception ex)
        {

        }
    }

    public void UpdateRoll()
    {
        try
        {
            ship.rollMultiplier = float.Parse(roll.text);
            PlayerPrefs.SetFloat("roll", ship.rollMultiplier);
        }
        catch (Exception ex)
        {

        }
    }

    public void UpdateSpin()
    {
        try
        {
            ship.spinMultiplier = float.Parse(spin.text);
            PlayerPrefs.SetFloat("spin", ship.spinMultiplier);
        }
        catch (Exception ex)
        {

        }
    }

    public void UpdateFlip()
    {
        try
        {
            ship.flipMultiplier = float.Parse(flip.text);
            PlayerPrefs.SetFloat("flip", ship.flipMultiplier);
        }
        catch (Exception ex)
        {

        }
    }

    public void UpdateFBrake()
    {
        try
        {
            ship.fBrakeMultiplier = float.Parse(fBrake.text);
            PlayerPrefs.SetFloat("fBrake", ship.fBrakeMultiplier);
        }
        catch (Exception ex)
        {

        }
    }

    public void UpdateTBrake()
    {
        try
        {
            ship.tBrakeMultiplier = float.Parse(tBrake.text);
            PlayerPrefs.SetFloat("tBrake", ship.tBrakeMultiplier);
        }
        catch (Exception ex)
        {

        }
    }

    public void UpdateBoost()
    {
        try
        {
            ship.boostMultiplier = float.Parse(boost.text);
            PlayerPrefs.SetFloat("boost", ship.boostMultiplier);
        }
        catch (Exception ex)
        {

        }
    }

    public void ToggleFreeLook(bool enable)
    {
        if (enable)
        {
            freeLookEnabled.text = "on";
        }
        else
        {
            freeLookEnabled.text = "off";
        }
    }
}
