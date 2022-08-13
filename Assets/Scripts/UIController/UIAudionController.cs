using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAudionController : MonoBehaviour
{
    [SerializeField] private Button _volumeOnOff;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Sprite _imageVolumeOn;
    [SerializeField] private Sprite _imageVolumeOff;
    private void OnOffVolume()
    {
        AudioManager.PlayButton();
        if (AudioSaveController.GetIsAudioPlay())
        {
            AudioSaveController.SetIsAudioPlay(0);
            _volumeOnOff.GetComponent<Image>().sprite = _imageVolumeOff;
        }
        else
        {
            AudioSaveController.SetIsAudioPlay(1);
            _volumeOnOff.GetComponent<Image>().sprite = _imageVolumeOn;
        }
        AudioManager.SetBgMUsic();
    }
    private void SetVolume(float volume)
    {
        AudioSaveController.SetAudioVolume(volume);
        AudioManager.SetVolume();
    }
    private void Start()
    {
        _volumeOnOff.onClick.AddListener(OnOffVolume);
        _volumeSlider.onValueChanged.AddListener(SetVolume);
        _volumeSlider.value = AudioSaveController.GetAudioVolume();
        ChangeSprite();
    }
    private void ChangeSprite()
    {
        if (!AudioSaveController.GetIsAudioPlay()) 
            _volumeOnOff.GetComponent<Image>().sprite = _imageVolumeOff;
        else
            _volumeOnOff.GetComponent<Image>().sprite = _imageVolumeOn;
    }
}
