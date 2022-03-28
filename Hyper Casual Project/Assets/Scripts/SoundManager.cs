using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgm1;
    public AudioSource bgm2;
    public AudioSource bgm3;
    public AudioSource bgm4;

    public AudioSource clickSound;
    public AudioSource uiClickSound;
    public AudioSource appearSound;
    bool CoUIClicked;

    public AudioSource currentAudio;
    public Slider musicSlider;
    public Slider soundSlider;

    private void Start()
    {
        currentAudio = bgm1;
    }

    private void Update()
    {
        currentAudio.volume = musicSlider.value;
        clickSound.volume = soundSlider.value;
        uiClickSound.volume = soundSlider.value;
        appearSound.volume = soundSlider.value;
    }

    public void OnBgm1()
    {
        if (bgm2.isPlaying) bgm2.Stop();
        if (bgm3.isPlaying) bgm3.Stop();
        if (bgm4.isPlaying) bgm4.Stop();

        currentAudio = bgm1;
        bgm1.Play();
    }

    public void OnBgm2()
    {
        if (bgm1.isPlaying) bgm1.Stop();
        if (bgm3.isPlaying) bgm3.Stop();
        if (bgm4.isPlaying) bgm4.Stop();

        currentAudio = bgm2;
        bgm2.Play();
    }

    public void OnBgm3()
    {
        if (bgm1.isPlaying) bgm1.Stop();
        if (bgm2.isPlaying) bgm2.Stop();
        if (bgm4.isPlaying) bgm4.Stop();

        currentAudio = bgm3;
        bgm3.Play();
    }

    public void OnBgm4()
    {
        if (bgm1.isPlaying) bgm1.Stop();
        if (bgm2.isPlaying) bgm2.Stop();
        if (bgm3.isPlaying) bgm3.Stop();

        currentAudio = bgm4;
        bgm4.Play();
    }

    public void ClickUI()
    {
        if (uiClickSound.isPlaying)
        {
            uiClickSound.Stop();
            uiClickSound.Play();
        }
        else uiClickSound.Play();
    }

}
