using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource BallJump, Score, GameOver, Gamewin;
    public Toggle SoundToggle;
    public AudioMixer MainMixer;
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ToggleChanged(bool val)
    {
        if(val)
        {
            MainMixer.SetFloat("Vol", -80);
        }
        else
        {
            MainMixer.SetFloat("Vol", 0);

        }
    }

    public void PlayBallJump()
    {
        BallJump.Play();
    }

    public void PlayScoreSound()
    {
        Score.Play();
    }

    public void PlayGameOverSound()
    {
        GameOver.Play();
    }

    public void PlayGamewinSound()
    {
        Gamewin.Play();
    }
}
