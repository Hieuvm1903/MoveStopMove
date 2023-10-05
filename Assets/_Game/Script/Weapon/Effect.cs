using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] ParticleSystem[] PSs;
    [SerializeField] AudioSource audioSource;
    public void Play()
    {
        foreach(var pas in PSs)
        {
            pas.Play();
        }
        audioSource.Play();
    }
}
