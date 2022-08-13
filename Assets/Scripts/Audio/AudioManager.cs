using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    [SerializeField] private AudioSource _audioButtonPref;
    [SerializeField] private AudioSource _audioFigurDropPref;
    [SerializeField] public AudioSource _bgMusic;
    public static AudioSource audioFigurDrop;
    public static AudioSource audioButton;
    public static AudioSource bgMusic;
    public static void PlayButton()
    {
        if (!AudioSaveController.GetIsAudioPlay()) return;
        audioButton.Play();
    }

    public static void PlayFigurDrop()
    {
        if (!AudioSaveController.GetIsAudioPlay()) return;
        audioFigurDrop.Play();
    }
    public static void SetVolume()
    {
        audioButton.volume = AudioSaveController.GetAudioVolume();
        audioFigurDrop.volume = AudioSaveController.GetAudioVolume();
        bgMusic.volume = AudioSaveController.GetAudioVolume() / 2;
    }
    public static void SetBgMUsic()
    {
        if (!AudioSaveController.GetIsAudioPlay()) bgMusic.Stop();
        else bgMusic.Play();
    }
    private void Start()
    {
        audioButton = _audioButtonPref;
        audioFigurDrop = _audioFigurDropPref;
        bgMusic = _bgMusic;
        SetVolume();
        SetBgMUsic();
    }
}
