using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationSequence : MonoBehaviour
{
    public static AnimationSequence Instance;
    [SerializeField]
    public bool animate;

    [SerializeField]
    bool epilepsySafe;

    [SerializeField]
    List<AnimationObject> animObjs;
    public enum LoadedSection { FullSong, Intro, Explosion };

    [SerializeField]
    public LoadedSection currSection;

    #region AnimObjs
    AnimationObject cannon { get { return animObjs[0]; } set { cannon = value; } }
    AnimationObject planetGlowUp { get { return animObjs[1]; } set { planetGlowUp = value; } }
    AnimationObject protag2 { get { return animObjs[2]; } set { protag2 = value; } }
    AnimationObject explosion1 { get { return animObjs[3]; } set { explosion1 = value; } }
    AnimationObject shockwave1 { get { return animObjs[3]; } set { planet = value; } }
    AnimationObject planet { get { return animObjs[4]; } set { planet = value; } }
    AnimationObject protag1 { get { return animObjs[5]; } set { protag1 = value; } }
    public AnimationObject protag3 { get { return animObjs[6]; } set { protag3 = value; } }
    AnimationObject f3_1 { get { return animObjs[7]; } set { f3_1 = value; } }
    AnimationObject f3_2 { get { return animObjs[8]; } set { f3_2 = value; } }
    AnimationObject friendly1 { get { return animObjs[9]; } set { friendly1 = value; } }
    AnimationObject friendly2 { get { return animObjs[10]; } set { friendly2 = value; } }
    AnimationObject friendly3 { get { return animObjs[11]; } set { friendly3 = value; } }
    AnimationObject friendly4 { get { return animObjs[12]; } set { friendly4 = value; } }

    [SerializeField]
    Transform[] npcTargets = new Transform[4];

    public delegate void ManualTick();
    List<KeyValuePair<int, ManualTick>> manualTicks = new List<KeyValuePair<int, ManualTick>>();

    public delegate void ManualEvent(InstrumentEvent instEvent);
    List<ManualEventData> manualEvents = new List<ManualEventData>();
    private class ManualEventData
    {
        public ManualEvent theEvent;
        public float timestamp;
        public InstrumentEvent instEvent;
    }
    public float manualEventTimer = 0;

    AnimationObject[] protags;
    public object ControlPanel { get; private set; }
    #endregion

    Dictionary<int, int> instCounter;

    AnimationEvents events;
    public int ticks = 0;
    public int notes = 0;
    private int startTicks = 0;

    private int pauseTicks = 0;
    public bool paused = false;
    public bool hasPaused = false;

    private delegate void FXAction();
    private FXAction forceFxAction;
    private bool forcingFX = false;
    private int forceFxCounter = 0;
    private int forceFxMax = 10;
    private bool doForceFX = true;

    float timer = 0;
    double msPerQuarter;
    float delay = 0;
    float startDelay = 150;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        events = GetComponent<AnimationEvents>();
        InitInstCounter();
        protags = new AnimationObject[3] { protag1, protag2, protag3 };

        if (animate)
        {
            switch (currSection)
            {
                case LoadedSection.FullSong:
                    ticks = 0;
                    break;
                case LoadedSection.Intro:
                    ticks = Section.Intro.start;
                    f3_1.gameObject.SetActive(true);
                    break;
                case LoadedSection.Explosion:
                    ticks = Section.Intro.explosion;
                    f3_1.gameObject.SetActive(true);
                    instCounter[Inst.Bass] = 130;
                    break;
            }
            startTicks = ticks;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Reset"))
        {
            SceneManager.LoadScene(0);
        }
        //If we're playing after we've paused
        if (!paused && hasPaused)
        {
            //DoTickAfterUnpaused();
            DoEventAfterUnpaused();
        }
        //Start this timer after the first pause
        if (hasPaused)
        {
            if (paused)
            {
                manualEventTimer = GlobalTimer.Instance.timer1;
            }
            else
            {
                manualEventTimer += Time.deltaTime;
            }
        }
    }

    private void InitInstCounter()
    {
        instCounter = new Dictionary<int, int>();
        for (int i = 0; i < 16; i++)
        {
            instCounter.Add(i, 0);
        }
    }

    public void TogglePause()
    {
        manualEventTimer = 0;

        //If we're paused
        if (paused)
        {
            //Then Play
            GlobalTimer.Instance.timer1 = 0;
            MusicManager.Instance.PlayOrUnpause();
            paused = false;
            Time.timeScale = 1;
        }
        else
        {
            //Otherwise Pause
            MusicManager.Instance.Pause();
            paused = true;
            hasPaused = true;
            Time.timeScale = 0;
        }
    }

    private void LogTickWhilePaused()
    {
        manualTicks.Add(new KeyValuePair<int, ManualTick>(ticks, Tick));
    }

    private void DoTickAfterUnpaused()
    {
        if (delay >= startDelay)
        {
            if (MidiManager.Instance.pulseLength > 0)
            {
                msPerQuarter = (MidiManager.Instance.deltaTicksPerQuarterNote / 2) * MidiManager.Instance.pulseLength;
                timer += Time.deltaTime;
                if (timer * 1000 >= msPerQuarter)
                {
                    if (manualTicks.Count > 0)
                    {
                        manualTicks[0].Value();
                        manualTicks.RemoveAt(0);
                    }
                    timer = 0;
                }
            }
        }
        else
        {
            delay++;
        }
    }

    private void LogEventWhilePaused(InstrumentEvent instEvent, float timestamp)
    {
        ManualEventData data = new ManualEventData
        {
            theEvent = MidiNoteEvent(instEvent),
            timestamp = timestamp,
            instEvent = instEvent
        };
        manualEvents.Add(data);
    }

    private void DoEventAfterUnpaused()
    {
        if (manualEvents.Count > 0)
        {
            //If the event's timestamp matches current timer
            if (manualEvents[0].timestamp >= manualEventTimer)
            {
                //Run the event
                manualEvents[0].theEvent(manualEvents[0].instEvent);
                manualEvents.RemoveAt(0);
            }
        }
    }

    public void Tick()
    {
        if (animate && !paused)
        {
            //at 4 beats
            if (ticks % 4 == 0)
            {

            }
            //at 8 beats
            if (ticks % 8 == 0)
            {
            }

            //intro with no midi
            if (ticks == 0)
            {
                //CameraManager.Instance.SetNewTarget(CameraManager.CamTargets.Protag);
                //CameraManager.Instance.SetNewAngle(CameraManager.CamAngles.LowerLeftDrop);
                CameraManager.Instance.animInitialized = true;
                CameraManager.Instance.animJustInit = true;
                CameraManager.Instance.camera.GetComponent<CameraMovement>().panY = true;
                CameraManager.Instance.FadeCamera();
                ControlPanelFuncs.Luminaris1LLD();
                
            }
            if (ticks == startTicks)
            {
                CameraManager.Instance.animInitialized = true;
                CameraManager.Instance.animJustInit = true;
                CameraManager.Instance.FadeCamera();
            }
            //Have to change camera angles on ticks because there are no notes yet
            if (ticks == 32)
            {
                ControlPanelFuncs.Luminaris1DTS();
            }
            if (ticks == 84)
            {
                ControlPanelFuncs.Luminaris1FP();
            }
            if (ticks == 130)
            {
                f3_1.gameObject.SetActive(true);
                //f3.StartMoving(5);
            }
            if (ticks == 132)
            {
                ControlPanelFuncs.PlanetGChase();
            }
            //F3 blow up planet
            if (instCounter[Inst.Bass] >= 140 && ticks < 300)
            {
                protag2.StopMoving();
                cannon.StopContinuousFire();
                //ForceExplosion1();
                explosion1.SpawnExplosion();
                planet.Disappear();
            }
            //Get ready for new protag obj
            if (ticks == 290)
            {
                protag3.gameObject.SetActive(true);
                ControlPanelFuncs.SwitchShipCam(ShipManager.Ships.Luminaris, CameraManager.CamTargets.Protag, "ProtagShipHolder2", 2, CameraManager.CamAngles.DownTheStreet, false);
                f3_1.gameObject.SetActive(false);
                f3_2.gameObject.SetActive(true);
            }
            //Protag start moving back in
            if (ticks == 313)
            {
                ControlPanelFuncs.Luminaris3PB();
            }
            if (ticks == 315)
            {
                protag3.StartMoving(25);
                f3_2.StartMoving(100);
            }
            if (instCounter[Inst.Bass] == 150)
            {
                //protag3.ManualControl();
            }


            //Debug.Log("Tick= " + ticks);
            ticks++;
        }
        else if (paused)
        {
            pauseTicks++;
            LogTickWhilePaused();
            hasPaused = true;
        }
        else
        {
            CameraManager.Instance.animInitialized = true;
            CameraManager.Instance.animJustInit = true;
        }
        if (CheckForceFX())
        {
            DoForceFX();
        }
    }

    public ManualEvent MidiNoteEvent(InstrumentEvent instEvent)
    {
        notes++;

        if (animate && !paused)
        {
            switch (instEvent.channel)
            {
                //Song Map per instrument
                #region Vocals1
                case Inst.Vocals1:

                    instCounter[Inst.Vocals1]++;
                    break;
                #endregion
                #region Vocals2
                case Inst.Vocals2:

                    instCounter[Inst.Vocals2]++;
                    break;
                #endregion
                #region Strings
                case Inst.Strings:

                    instCounter[Inst.Strings]++;
                    break;
                #endregion
                #region LeadGuitar
                case Inst.LeadGuitar:

                    instCounter[Inst.LeadGuitar]++;
                    break;
                #endregion
                #region RhythmGuitar
                case Inst.RhythmGuitar:

                    instCounter[Inst.RhythmGuitar]++;
                    break;
                #endregion
                #region Bass
                case Inst.Bass:
                    //entire intro w/ drums
                    if (instCounter[Inst.Bass] == 30)
                    {
                        protag2.transform.gameObject.SetActive(true);
                    }
                    if (instCounter[Inst.Bass] == 35)
                    {
                        ControlPanelFuncs.F3_1UFL();
                    }
                    if (instCounter[Inst.Bass] == 70)
                    {
                        ControlPanelFuncs.PlanetGUBR();
                    }
                    if (instCounter[Inst.Bass] < 140)
                    {
                        cannon.ShootCannon(true);
                        planetGlowUp.GlowUp();
                    }
                    if (instCounter[Inst.Bass] >= 105 && instCounter[Inst.Bass] < 140)
                    {
                        ControlPanelFuncs.Luminaris2FP();
                    }
                    if (instCounter[Inst.Bass] == 140)
                    {
                    }
                    if (instCounter[Inst.Bass] == 148)
                    {
                        CameraManager.Instance.StartShaking();
                        Move pt3Move = protag3.GetComponentInChildren<Move>();
                        MoveTowards mt = pt3Move.gameObject.AddComponent<MoveTowards>();
                        mt.objToMove = pt3Move.gameObject;
                        mt.objToMoveTowards = f3_2.GetComponentInChildren<Move>().transform;
                        mt.speed = protag3.GetSpeed();
                        //PointTowards pt = pt3Move.gameObject.AddComponent<PointTowards>();
                        //pt.objToRotate = pt3Move.gameObject;
                        //pt.objToLookAt = f3_2.GetComponentInChildren<Move>().transform;
                        ControlPanelFuncs.SwitchShipCam(ShipManager.Ships.Luminaris, CameraManager.CamTargets.Protag, "ProtagShipHolder3", 3, CameraManager.CamAngles.FirstPerson, false);
                    }
                    Ship ship = protag3.GetComponentInChildren<Ship>();
                    if (instCounter[Inst.Bass] == 151)
                    {
                        ship.reqObjs[0].gameObject.SetActive(true);
                    }
                    if (instCounter[Inst.Bass] == 153)
                    {
                        ship.reqObjs[1].gameObject.SetActive(false);
                        ship.reqObjs[2].gameObject.SetActive(true);
                    }
                    if (instCounter[Inst.Bass] == 154)
                    {
                        ship.reqObjs[2].gameObject.SetActive(false);
                        ship.reqObjs[3].gameObject.SetActive(true);
                    }
                    if (instCounter[Inst.Bass] == 155)
                    {
                        ship.reqObjs[3].gameObject.SetActive(false);
                        ship.reqObjs[4].gameObject.SetActive(true);
                    }
                    if (instCounter[Inst.Bass] == 156)
                    {
                        ship.reqObjs[5].gameObject.SetActive(false);
                        ship.reqObjs[6].gameObject.SetActive(true);
                    }
                    if (instCounter[Inst.Bass] == 160)
                    {
                        ship.reqObjs[0].gameObject.SetActive(false);
                        ControlPanelFuncs.SwitchShipCam(ShipManager.Ships.Luminaris, CameraManager.CamTargets.Protag, "ProtagShipHolder3",
                            3, CameraManager.CamAngles.BirdsEye, false);
                        friendly1.gameObject.SetActive(true);
                        friendly2.gameObject.SetActive(true);
                        friendly3.gameObject.SetActive(true);
                        friendly4.gameObject.SetActive(true);
                        Ship pt3Ship = protag3.GetComponentInChildren<Ship>();
                        friendly1.GetComponentInChildren<MoveTowards>().objToMoveTowards = pt3Ship.reqObjs[7].gameObject.transform;
                        friendly2.GetComponentInChildren<MoveTowards>().objToMoveTowards = pt3Ship.reqObjs[8].gameObject.transform;
                        friendly3.GetComponentInChildren<MoveTowards>().objToMoveTowards = pt3Ship.reqObjs[9].gameObject.transform;
                        friendly4.GetComponentInChildren<MoveTowards>().objToMoveTowards = pt3Ship.reqObjs[10].gameObject.transform;
                    }
                    if (instCounter[Inst.Bass] == 170)
                    {
                        CameraManager.Instance.DropCamNow();
                        protag3.SetSpeed(500);
                        friendly1.GetComponentInChildren<MoveTowards>().speed = 600;
                        friendly2.GetComponentInChildren<MoveTowards>().speed = 600;
                        friendly3.GetComponentInChildren<MoveTowards>().speed = 600;
                        friendly4.GetComponentInChildren<MoveTowards>().speed = 600;
                        f3_2.SetSpeed(20);
                        GameObject camHolder = CameraManager.Instance.camera.transform.parent.gameObject;
                        Rigidbody camrb = camHolder.AddComponent<Rigidbody>();
                        camrb.useGravity = false;
                        ZeroGravitySpin zgs = camHolder.AddComponent<ZeroGravitySpin>();
                        zgs.SpinVelocity = Vector3.forward * .25f;
                    }
                    if (instCounter[Inst.Bass] == 175)
                    {
                        //ControlPanelFuncs.SwitchShipCam(ShipManager.Ships.F3, CameraManager.CamTargets.BigBoss, "BigBossShipHolder2", 2,
                        //    CameraManager.CamAngles.ChaseView, false);
                        ControlPanelFuncs.F3_2Chase();
                        CamAngle[] angles = f3_2.GetComponentsInChildren<CamAngle>();
                        foreach (CamAngle angle in angles)
                        {
                            if (angle.angle == CameraManager.CamAngles.ChaseView)
                            {
                                PointTowards pt = angle.gameObject.AddComponent<PointTowards>();
                                pt.objToRotate = angle.gameObject;
                                pt.objToLookAt = protag3.GetComponentInChildren<Move>().transform;
                            }
                        }
                    }
                    Debug.Log("Bass notes = " + instCounter[Inst.Bass]);
                    instCounter[Inst.Bass]++;
                    break;
                #endregion
                #region Drums
                case Inst.Drums:

                    instCounter[Inst.Drums]++;
                    break;
                    #endregion
            }
        }
        else if (paused)
        {
            LogEventWhilePaused(instEvent, manualEventTimer);
        }
        //Debug.Log("Note " + notes);
        return null;
    }

    /// <summary>
    /// Call this when you want to trigger the explosion
    /// </summary>
    public void ForceExplosion1()
    {
        doForceFX = true;
        protag2.transform.gameObject.SetActive(true);
        cannon.StopContinuousFire();
        forceFxAction = DoForceExplosion1;
    }

    /// <summary>
    /// Call this when you want to trigger the shockwave
    /// </summary>
    public void ForceShockwave1()
    {
        doForceFX = true;
        protag2.transform.gameObject.SetActive(true);
        cannon.StopContinuousFire();
        forceFxAction = DoForceShockwave1;
    }

    /// <summary>
    /// The function that actually does the work frame-by-frame
    /// </summary>
    private void DoForceExplosion1()
    {
        DoForceExplosion(shockwave1, explosion1);
    }

    /// <summary>
    /// The function that actually does the work frame-by-frame
    /// </summary>
    private void DoForceShockwave1()
    {
        DoForceExplosion(shockwave1, null);
    }

    public void DoForceExplosion(AnimationObject shockwave, AnimationObject explosion)
    {
        if (shockwave)
        {
            foreach (AnimationObject p in protags)
            {
                p.StopMoving();
            }
            shockwave.ProduceShockwave();
        }
        if (explosion)
        {
            explosion.SpawnExplosion();
            planet.Disappear();
        }
    }

    public void FireLaserCannon1()
    {

    }

    public void Protag3StartMoving()
    {
        protag3.StartMoving(18);
    }

    private bool CheckForceFX()
    {
        if (doForceFX)
        {
            forcingFX = true;
        }
        return forcingFX;
    }


    private void DoForceFX()
    {
        try
        {
            forceFxAction();
        }
        catch(Exception ex)
        {

        }
        if (forceFxCounter < forceFxMax)
        {
            forceFxCounter++;
        }
        else
        {
            forceFxCounter = 0;
            doForceFX = false;
            forcingFX = false;
        }
    }
}
