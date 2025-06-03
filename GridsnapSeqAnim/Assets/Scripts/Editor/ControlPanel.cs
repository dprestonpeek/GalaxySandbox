using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using MidiPlayerTK;

//[ExecuteAlways]
public class ControlPanel : EditorWindow
{
    public Rect windowRect = new Rect(20, 20, 120, 50);
    static MidiFilePlayer midiPlayer;
    static AudioSource audioPlayer;
    static int ticks = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    //void OnGUI()
    //{
    //    //Get references to our players
    //    midiPlayer = GetMidiPlayer(midiPlayer);
    //    audioPlayer = GetAudioPlayer(audioPlayer);

    //    #region Timeline Control
    //    //GUILayout.Label("Timeline Control");
    //    //GUILayout.BeginHorizontal();
    //    //GUILayout.BeginHorizontal();
    //    //GUILayoutOption[] opts = new GUILayoutOption[2] { GUILayout.Width(100), GUILayout.Height(50) };

    //    //GUILayout.EndHorizontal();
    //    //GUILayout.EndHorizontal();
    //    #endregion

    //    #region Camera Positions

    //    //Protags
    //    GUILayout.BeginHorizontal();
    //    GUILayout.Label("Luminaris 1 Camera");
    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("Lower Left Drop Cam"))
    //    {
    //        ControlPanelFuncs.Luminaris1LLD();
    //    }
    //    if (GUILayout.Button("Down the Street"))
    //    {
    //        ControlPanelFuncs.Luminaris1DTS();
    //    }
    //    GUILayout.EndHorizontal();
    //    GUILayout.EndHorizontal();
    //    //
    //    GUILayout.BeginHorizontal();
    //    GUILayout.Label("Luminaris 2 Camera");
    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("First Person"))
    //    {
    //        ControlPanelFuncs.Luminaris2FP();
    //    }
    //    GUILayout.EndHorizontal();
    //    GUILayout.EndHorizontal();
    //    //
    //    GUILayout.BeginHorizontal();
    //    GUILayout.Label("Luminaris 3 Camera");
    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("Passing By"))
    //    {
    //        ControlPanelFuncs.Luminaris3PB();
    //        AnimationSequence.Instance.Protag3StartMoving();
    //    }
    //    if (GUILayout.Button("Down the Street"))
    //    {
    //        ControlPanelFuncs.Luminaris3DTS();
    //    }
    //    if (GUILayout.Button("Start Moving"))
    //    {
    //        AnimationSequence.Instance.Protag3StartMoving();
    //    }
    //    GUILayout.EndHorizontal();
    //    GUILayout.EndHorizontal();

    //    //Space Objs
    //    GUILayout.BeginHorizontal();
    //    GUILayout.Label("Planet G Camera");
    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("Upper Back Right"))
    //    {
    //        ControlPanelFuncs.PlanetGUBR();
    //    }
    //    if (GUILayout.Button("Chase Cam"))
    //    {
    //        ControlPanelFuncs.PlanetGChase();
    //    }
    //    GUILayout.EndHorizontal();
    //    GUILayout.EndHorizontal();

    //    //Villains
    //    GUILayout.BeginHorizontal();
    //    GUILayout.Label("F3 Camera");
    //    GUILayout.BeginHorizontal();
    //    if (GUILayout.Button("Upper Front Left"))
    //    {
    //        ControlPanelFuncs.F3_1UFL();
    //    }
    //    GUILayout.EndHorizontal();
    //    GUILayout.EndHorizontal();
    //    #endregion

    //    GUILayout.Space(25);

    //    #region Actions
    //    GUILayout.BeginHorizontal();
    //    GUILayout.Label("Cinematic FX");
    //    if (GUILayout.Button("Do Explosion 1"))
    //    {
    //        AnimationSequence.Instance.ForceExplosion1();
    //    }
    //    if (GUILayout.Button("Do Shockwave 1"))
    //    {
    //        AnimationSequence.Instance.ForceExplosion1();
    //    }
    //    GUILayout.EndHorizontal();
    //    #endregion
    //}

    static MidiFilePlayer GetMidiPlayer(MidiFilePlayer nullCheck)
    {
        if (!nullCheck)
        {
            return FindObjectOfType<MidiFilePlayer>();
        }
        else
        {
            return nullCheck;
        }
    }

    static AudioSource GetAudioPlayer(AudioSource nullCheck)
    {
        if (!nullCheck)
        {
            return FindObjectOfType<MusicManager>().GetComponent<AudioSource>();
        }
        else
        {
            return nullCheck;
        }
    }

    [MenuItem("GRIDSNAP/Control Panel")]
    static void OpenControlPanel()
    {
        GetWindow<ControlPanel>("Control Panel");
    }
}
