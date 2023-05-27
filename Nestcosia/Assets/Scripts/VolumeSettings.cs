using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider sfxVolume;

    const string MusicKey = "MusicVolume";
    const string SFXKey = "SFXVolume";

    private void Awake()
    {
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxVolume.onValueChanged.AddListener(SetSFXVolume);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMusicVolume(float value)
    {
        mixer.SetFloat(MusicKey, value);

    }

    public void SetSFXVolume(float value)
    {
        mixer.SetFloat(SFXKey, value);
    }

}
