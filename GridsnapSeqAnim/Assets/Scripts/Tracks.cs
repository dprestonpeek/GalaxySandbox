using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracks : MonoBehaviour
{

    #region Intro

    public struct General
    {
        public static int firstNote { get { return 1; } }
    }

    public struct Vocals
    {
    }

    public struct HeavyGtr
    {
    }

    public struct CleanGtr
    {
    }

    public struct Bass
    {
        public static int startIntro { get { return 1; } }
        public static int endIntro { get { return 140; } }
    }

    public struct Keys
    {
    }

    public struct Snare
    {
    }

    public struct Kick
    {
    }

    public struct eKick
    {
    }

    public struct Click
    {
    }
 
    #endregion
}

public class Inst
{
    #region Instruments

    //Billy configuration
    //public const int Kick = 0;
    //public const int Snare = 1;
    //public const int RackTom = 2;
    //public const int FloorTom = 3;
    //public const int CleanGtr = 4;
    //public const int HeavyGtr = 5;
    //public const int Vocals = 6;
    //public const int Bass = 7;
    //public const int eKick = 8;
    //public const int eHats = 10;
    //public const int eSnaps = 11;
    //public const int eSnare = 12;
    //public const int Click = 13;
    //public const int Keys = 14;

    //Preston configuration
    public const int Vocals1 = 0;
    public const int Vocals2 = 1;
    public const int Strings = 2;
    public const int LeadGuitar = 3;
    public const int RhythmGuitar = 4;
    public const int Bass = 5;
    public const int Drums = 9;

    #endregion
}
