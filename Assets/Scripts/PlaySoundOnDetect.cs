using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class PlaySoundOnDetect : MonoBehaviour
{
    private AudioSource audioSource;
    private ObserverBehaviour observerBehaviour;
    private Animator animator;

    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        observerBehaviour = GetComponent<ObserverBehaviour>();

        if (observerBehaviour)
        {
            observerBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }

        // Disable animator and audio at start (no sound or animation yet)
        if (animator) animator.enabled = false;
        if (audioSource) audioSource.enabled = false;
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        bool isVisible = targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED;

        if (isVisible)
        {
            if (animator) animator.enabled = true;
            if (audioSource)
            {
                audioSource.enabled = true;
                if (!audioSource.isPlaying)
                    audioSource.Play();
            }
        }
        else
        {
            if (animator) animator.enabled = false;
            if (audioSource)
            {
                audioSource.Stop();
                audioSource.enabled = false;
            }
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
