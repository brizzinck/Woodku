using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnAbility : Ability
{
    protected override void ControllOnOffAbility()
    {
        base.ControllOnOffAbility();
        if (AbilityController.GetSaveAbility("RespawnAbility") >= 1) SetAbility();
    }

    protected override void SetAbility()
    {
        FiguresAbility.RespawnFigure(FigureSpawner.Figures);
        AbilityController.SetSaveAbility("RespawnAbility", -1, false);
        CountTXT.text = AbilityController.GetSaveAbility("RespawnAbility").ToString();
        base.ControllOnOffAbility();
        FigureSelectControll(true);
    }

    private void Start()
    {
        AbbilityButton.onClick.AddListener(ControllOnOffAbility);
        CountTXT.text = AbilityController.GetSaveAbility("RespawnAbility").ToString();
    }
    private void Update()
    {
        if (CheckAbility("RespawnAbility"))
        {
            SetAbility();
            HighAbility();
        }
    }
}
