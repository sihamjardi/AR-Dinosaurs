using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PlaySoundOnDetect : MonoBehaviour
{
    private AudioSource audioSource;
    private ObserverBehaviour observerBehaviour;

    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        observerBehaviour = GetComponent<ObserverBehaviour>();

        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        // When the image target is detected
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        // When the image target is lost
        else
        {
            audioSource.Stop();
        }
    }

    void OnDestroy()
    {
        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
}

