using System;
using System.Collections;
using System.Collections.Generic;
using MidiPlayerTK;
using MPTK.NAudio.Midi;
using UnityEngine;

public class MidiManager : MonoBehaviour
{
    public static MidiManager Instance;

    public double pulseLength { get; private set; }
    public double deltaTicksPerQuarterNote { get; private set; }
    public int ticks { get; private set; }

    [SerializeField]
    public MidiFilePlayer player;

    List<long> prevTicks;
    List<MPTKEvent> mptkEvents;
    List<TrackMidiEvent> midiEvents;

    AnimationSequence sequence;

    float timer = 0;
    double msPerQuarter;
    float delay = 0;
    float startDelay = 150;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        player = FindObjectOfType<MidiFilePlayer>();
        player.OnEventNotesMidi.AddListener(NotesToPlay);
        pulseLength = player.MPTK_PulseLength;
        deltaTicksPerQuarterNote = player.MPTK_DeltaTicksPerQuarterNote;
        mptkEvents = new List<MPTKEvent>();
        midiEvents = new List<TrackMidiEvent>();
        sequence = FindObjectOfType<AnimationSequence>();
        prevTicks = new List<long>();
        pulseLength = player.MPTK_PulseLength;

        switch (AnimationSequence.Instance.currSection)
        {
            case AnimationSequence.LoadedSection.FullSong:
                ticks = 0;
                player.MPTK_MidiName = "WrongDogMidiLineup3";
                break;
            case AnimationSequence.LoadedSection.Intro:
                ticks = Section.Intro.start;
                MidiManager.Instance.player.MPTK_MidiName = "WD-1-Intro";
                break;
            case AnimationSequence.LoadedSection.Explosion:
                ticks = Section.Intro.explosion;
                MidiManager.Instance.player.MPTK_MidiName = "WD-2-Explosion";
                break;
        }
        if (AnimationSequence.Instance.animate)
        {
            CameraManager.Instance.ActivateFadePanel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (delay >= startDelay)
        {
            player.MPTK_Play();
            if (player.MPTK_PulseLength > 0)
            {
                msPerQuarter = (player.MPTK_DeltaTicksPerQuarterNote / 2) * player.MPTK_PulseLength;
                timer += Time.deltaTime;
                if (timer * 1000 >= msPerQuarter)
                {
                    if (!sequence.hasPaused)
                    {
                        sequence.Tick();
                    }
                    ticks++;
                    timer = 0;
                }
            }
        }
        else
        {
            delay++;
        }
    }

    private void NotesToPlay(List<MPTKEvent> events)
    {
        foreach (MPTKEvent ev in events)
        {
            if (ev.Command == MPTKCommand.NoteOn)
            {
                mptkEvents.Add(ev);
                InstrumentEvent info = new InstrumentEvent(ev.Channel, ev.Value);
                if (!sequence.hasPaused)
                {
                    sequence.MidiNoteEvent(info);
                }
            }
        }
    }

    public void NoteEvent()
    {
        foreach (MPTKEvent ev in mptkEvents)
        {
            if (ev.Command == MPTKCommand.NoteOn)
            {
            }
        }

        //need to do regex here to get the note name from the TrackMidiEvent
        //NoteOnEvent noteon = (NoteOnEvent)midiEvent.Event;
        //Debug.Log(noteon.NoteName);
    }
}

public struct InstrumentEvent
{
    public int note;
    public int channel;

    public InstrumentEvent(int channel, int note)
    {
        this.note = note;
        this.channel = channel;
    }
}
