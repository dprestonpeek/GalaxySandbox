using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField]
    public AudioClip FullSong;
    [SerializeField]
    public AudioClip Intro;
    [SerializeField]
    public AudioClip Explosion;

    AudioSource audioSource;
    bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        //switch (Section.currentSection)
        //{
        //    case Section.LoadedSection.FullSong:
        //        audioSource.clip = FullSong;
        //        break;
        //    case Section.LoadedSection.Intro:
        //        audioSource.clip = Intro;
        //        break;
        //    case Section.LoadedSection.Explosion:
        //        audioSource.clip = Explosion;
        //        break;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOrUnpause()
    {
        if (paused)
        {
            audioSource.UnPause();
        }
        else
        {
            audioSource.Play();
        }
    }

    public void Pause()
    {
        audioSource.Pause();
        paused = true;
    }
}
