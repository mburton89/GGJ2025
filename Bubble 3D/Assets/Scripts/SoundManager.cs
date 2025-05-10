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
    [SerializeField] AudioSource jump;

    public AudioSource motorSound;
    public float startingMotorVolume;
    public float startingMotorPitch;

    public float maxMotorVolume;
    public float maxMotorPitch;

    [HideInInspector] public bool isPlayingMotorSound;

    public float motorSoundIncreaseSpeed;

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

    private void Update()
    {
        if (isPlayingMotorSound)
        {
            if (motorSound.volume < maxMotorVolume)
            {
                motorSound.volume += motorSoundIncreaseSpeed;
            }

            if (motorSound.pitch < maxMotorPitch)
            {
                motorSound.pitch += motorSoundIncreaseSpeed;
            }
        }
        else 
        {
            if (motorSound.volume > startingMotorVolume)
            {
                motorSound.volume -= motorSoundIncreaseSpeed * 2;
            }

            if (motorSound.pitch > startingMotorPitch)
            {
                motorSound.pitch -= motorSoundIncreaseSpeed * 2;
            }
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

    public void PlayJumpSound()
    {
        PlaySound(jump);
    }

    public void StartMotorSound()
    {
        print("StartMotorSound");
        motorSound.enabled = true;
        motorSound.Play();
        isPlayingMotorSound = true;
    }

    public void StopMotorSound()
    {
        isPlayingMotorSound = false;
    }
}