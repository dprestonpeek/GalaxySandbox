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
        ship.fBrakeMultiplier = PlayerPrefs.GetFloat("fbrake", .1f);
        fBrake.text = ship.fBrakeMultiplier.ToString();
        ship.tBrakeMultiplier = PlayerPrefs.GetFloat("tbrake", .1f);
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
    }

    public void UpdateFwd()
    {
        ship.forwardMultiplier = float.Parse(fwd.text);
        PlayerPrefs.SetFloat("fwd", ship.forwardMultiplier);
    }

    public void UpdateSide()
    {
        ship.sideMultiplier = float.Parse(side.text);
        PlayerPrefs.SetFloat("side", ship.sideMultiplier);
    }

    public void UpdateUp()
    {
        ship.upMultiplier = float.Parse(up.text);
        PlayerPrefs.SetFloat("up", ship.upMultiplier);
    }

    public void UpdateRoll()
    {
        ship.rollMultiplier = float.Parse(roll.text);
        PlayerPrefs.SetFloat("roll", ship.rollMultiplier);
    }

    public void UpdateSpin()
    {
        ship.spinMultiplier = float.Parse(spin.text);
        PlayerPrefs.SetFloat("spin", ship.spinMultiplier);
    }

    public void UpdateFlip()
    {
        ship.flipMultiplier = float.Parse(flip.text);
        PlayerPrefs.SetFloat("flip", ship.flipMultiplier);
    }

    public void UpdateFBrake()
    {
        ship.fBrakeMultiplier = float.Parse(fBrake.text);
        PlayerPrefs.SetFloat("fBrake", ship.fBrakeMultiplier);
    }

    public void UpdateTBrake()
    {
        ship.tBrakeMultiplier = float.Parse(tBrake.text);
        PlayerPrefs.SetFloat("tBrake", ship.tBrakeMultiplier);
    }

    public void UpdateBoost()
    {
        ship.boostMultiplier = float.Parse(boost.text);
        PlayerPrefs.SetFloat("boost", ship.boostMultiplier);
    }

    public void UpdateUI()
    {

    }
}
