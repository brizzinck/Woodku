using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _countRotateAbilityTXT;
    [SerializeField] private TMPro.TextMeshProUGUI _countRespawnAbilityTXT;
    [SerializeField] private TMPro.TextMeshProUGUI _countDeleteAbilityTXT;

    [SerializeField] private TMPro.TextMeshProUGUI _costRotateAbilityTXT;
    [SerializeField] private TMPro.TextMeshProUGUI _costRespawnAbilityTXT;
    [SerializeField] private TMPro.TextMeshProUGUI _costDeleteAbilityTXT;

    [SerializeField] private int _costRotateAbility;
    [SerializeField] private int _costRespawnAbility;
    [SerializeField] private int _costDeleteAbility;

    private static int _costRotateAbilityStatic;
    private static int _costRespawnAbilityStatic;
    private static int _costDeleteAbilityStatic;

    public void BuyItems(string ability)
    {
        AudioManager.PlayButton();
        SetSaveAbility(ability, 1, true);
        ChangeAbilityTXT();
    }
    public static int GetSaveAbility(string ability)
    {
        if (ability == "RotateAbility")
            return PlayerPrefs.GetInt("RotateAbility");
        else if (ability == "RespawnAbility")
            return PlayerPrefs.GetInt("RespawnAbility");
        else if (ability == "DeleteAbility")
            return PlayerPrefs.GetInt("DeleteAbility");
        return 0;
    }
    public static void SetSaveAbility(string ability, int value, bool isBuy)
    {
        if (ability == "RotateAbility" && (Coins.GetCoin() >= _costRotateAbilityStatic || !isBuy))
        {
            PlayerPrefs.SetInt("RotateAbility", PlayerPrefs.GetInt("RotateAbility") + value);
            if (isBuy) Coins.SaveCoin(-_costRotateAbilityStatic);
        }
        else if (ability == "RespawnAbility" && (Coins.GetCoin() >= _costRespawnAbilityStatic || !isBuy))
        {
            PlayerPrefs.SetInt("RespawnAbility", PlayerPrefs.GetInt("RespawnAbility") + value);
            if (isBuy) Coins.SaveCoin(-_costRespawnAbilityStatic);
        }
        else if (ability == "DeleteAbility" && (Coins.GetCoin() >= _costDeleteAbilityStatic || !isBuy))
        {
            PlayerPrefs.SetInt("DeleteAbility", PlayerPrefs.GetInt("DeleteAbility") + value);
            if (isBuy) Coins.SaveCoin(-_costDeleteAbilityStatic);
        }
    }
    private void Start()
    {
        CheckSaveItems();
        SetCountAbilityTXT();
    }
    private void CheckSaveItems()
    {
        if (!PlayerPrefs.HasKey("RotateAbility")) PlayerPrefs.SetInt("RotateAbility", 3);
        if (!PlayerPrefs.HasKey("RespawnAbility")) PlayerPrefs.SetInt("RespawnAbility", 2);
        if (!PlayerPrefs.HasKey("DeleteAbility")) PlayerPrefs.SetInt("DeleteAbility", 1);
    }
    private void SetCountAbilityTXT()
    {
        ChangeAbilityTXT();

        _costRotateAbilityTXT.text = _costRotateAbility.ToString();
        _costRespawnAbilityTXT.text = _costRespawnAbility.ToString();
        _costDeleteAbilityTXT.text = _costDeleteAbility.ToString();

        _costRotateAbilityStatic = _costRotateAbility;
        _costRespawnAbilityStatic = _costRespawnAbility;
        _costDeleteAbilityStatic = _costDeleteAbility;
    }
    private void ChangeAbilityTXT()
    {
        _countRotateAbilityTXT.text = PlayerPrefs.GetInt("RotateAbility").ToString();
        _countRespawnAbilityTXT.text = PlayerPrefs.GetInt("RespawnAbility").ToString();
        _countDeleteAbilityTXT.text = PlayerPrefs.GetInt("DeleteAbility").ToString();
    }
}
