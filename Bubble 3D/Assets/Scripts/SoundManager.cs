using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] float minRandomPitch;
    [SerializeField] float maxRandomPitch;

    [SerializeField] AudioSource scorePoint;
    [SerializeField] AudioSource directHit;
    [SerializeField] AudioSource punch;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void PlaySound(AudioSource soundToPlay)
    {
        soundToPlay.pitch = Random.Range(minRandomPitch, maxRandomPitch);
        soundToPlay.Play();
    }

    public void PlayScorePointSound()
    {
        PlaySound(scorePoint);
    }

    public void PlayDirectHitSound()
    {
        PlaySound(directHit);
    }

    public void PlayPunchSound()
    {
        PlaySound(punch);
    }
}