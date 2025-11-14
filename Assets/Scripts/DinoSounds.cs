using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSounds : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip roarClip;
    public AudioClip barkClip;
    public AudioClip call1Clip;
    public AudioClip call2Clip;
    public AudioClip call3Clip;

    public void Roar()
    {
        PlaySound(roarClip);
    }

    public void Bark()
    {
        PlaySound(barkClip);
    }

    public void Call1()
    {
        PlaySound(call1Clip);
    }

    public void Call2()
    {
        PlaySound(call2Clip);
    }

    public void Call3()
    {
        PlaySound(call3Clip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
