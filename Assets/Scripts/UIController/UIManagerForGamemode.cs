using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
public class UIManagerForGamemode : MonoBehaviour
{
    [SerializeField]
    private Button[] _openMenu;
    [SerializeField]
    private Button[] _resetGame;
    [SerializeField]
    private Button _openSettings;
    [SerializeField]
    private CanvasGroup _settings;

    private void Awake()
    {
        for (int i = 0; i < _openMenu.Length; i++) _openMenu[i].onClick.AddListener(OpenMenu);
        for (int i = 0; i < _resetGame.Length; i ++) _resetGame[i].onClick.AddListener(ResetGame);
        _openSettings.onClick.AddListener(OpenSettings); 
    }
    public void OpenMenu()
    {
        AudioManager.PlayButton();
        SceneManager.LoadScene("MenuScene");
    }
    public void ResetGame()
    {
        AudioManager.PlayButton();
        SceneManager.LoadScene("GameScene");
    }
    public void OpenSettings()
    {
        AudioManager.PlayButton();
        GameAnimation.ControllUIFadeObj(_settings, false);
    }
}
