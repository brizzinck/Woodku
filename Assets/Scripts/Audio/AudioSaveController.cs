using UnityEngine;

public class AudioSaveController
{
    public static float GetAudioVolume()
    {
        if (!PlayerPrefs.HasKey("AudioVolume")) PlayerPrefs.SetFloat("AudioVolume", 0.5f);
        return PlayerPrefs.GetFloat("AudioVolume");
    }
    public static void SetAudioVolume(float volume)
    {
        PlayerPrefs.SetFloat("AudioVolume", volume);
    }
    public static bool GetIsAudioPlay()
    {
        if (!PlayerPrefs.HasKey("IsAudioPlay")) PlayerPrefs.SetInt("IsAudioPlay", 1);
        if (PlayerPrefs.GetInt("IsAudioPlay") == 1) return true;
        else return false;
    }
    public static void SetIsAudioPlay(int isPlay)
    {
        PlayerPrefs.SetInt("IsAudioPlay", isPlay);
    }
}
