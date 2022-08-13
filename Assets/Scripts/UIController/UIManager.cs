using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [SerializeField] 
    private Button _openShop;
    [SerializeField] 
    private Button _exitShop;
    [SerializeField] 
    private Button _startGame;
    [SerializeField]
    private Button _exitGame;
    [SerializeField]
    private Button _openSettings;    
    [SerializeField]
    private CanvasGroup _settings;
    [SerializeField]
    private CanvasGroup _shop;
    [SerializeField] 
    private TMPro.TextMeshProUGUI _scoreTXT;
    private void Awake()
    {    
        _openShop.onClick.AddListener(OpenCloseShop);
        _exitShop.onClick.AddListener(OpenCloseShop);
        _startGame.onClick.AddListener(PlayMode);
        _exitGame.onClick.AddListener(ExitGame);
        _openSettings.onClick.AddListener(OpenSettings);
        if (new Score().GetScore() <= 0) return;
        _scoreTXT.text = new Score().GetScore().ToString();
    }
    public void OpenCloseShop()
    {
        AudioManager.PlayButton();
        GameAnimation.ControllUIFadeObj(_shop, false);
        GameAnimation.ControllUIFadeObj(_settings, true);
    }
    public void PlayMode()
    {
        AudioManager.PlayButton();
        SceneManager.LoadScene("GameScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void OpenSettings()
    {
        AudioManager.PlayButton();
        GameAnimation.ControllUIFadeObj(_settings, false);
        GameAnimation.ControllUIFadeObj(_shop, true);
    }
}
