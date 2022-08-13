using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _coinsTXT;
    public static void SaveCoin(int coins)
    {
        if (!PlayerPrefs.HasKey("Coins")) PlayerPrefs.SetInt("Coins", 0);
        else PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + coins);
    }
    public static int GetCoin()
    {
        if (!PlayerPrefs.HasKey("Coins")) PlayerPrefs.SetInt("Coins", 0);
        return PlayerPrefs.GetInt("Coins");
    }
    public void CoinsChange()
    {
        _coinsTXT.text = GetCoin().ToString();
    }
    private void Start()
    {
        CoinsChange();
    }
}
