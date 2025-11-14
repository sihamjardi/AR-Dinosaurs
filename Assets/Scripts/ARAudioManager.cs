using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARAudioManager : MonoBehaviour
{
    public AudioSource dino1Audio;     //audio dino1
    public AudioSource dino2Audio;     // audio dino2
    public AudioSource velcoInfoAudio; // audio Velcoinfo

    private ObserverBehaviour observerBehaviour;

    void Start()
    {
        observerBehaviour = GetComponent<ObserverBehaviour>();
        if (observerBehaviour)
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        bool visible = status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED;

        if (dino1Audio)
        {
            if (visible && !dino1Audio.isPlaying) dino1Audio.Play();
            else if (!visible && dino1Audio.isPlaying) dino1Audio.Stop();
        }

        if (dino2Audio)
        {
            if (visible && !dino2Audio.isPlaying) dino2Audio.Play();
            else if (!visible && dino2Audio.isPlaying) dino2Audio.Stop();
        }

        if (velcoInfoAudio)
        {
            if (visible && !velcoInfoAudio.isPlaying) velcoInfoAudio.Play();
            else if (!visible && velcoInfoAudio.isPlaying) velcoInfoAudio.Stop();
        }
    }

    void OnDestroy()
    {
        if (observerBehaviour)
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
    }
}
