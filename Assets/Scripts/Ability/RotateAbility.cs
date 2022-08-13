using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateAbility : Ability
{
    protected override void ControllOnOffAbility()
    {
        base.ControllOnOffAbility();
    }

    protected override void SetAbility()
    {
        FiguresAbility.RotateFigure(CheckClickOnFigure(), this);
        AbilityController.SetSaveAbility("RotateAbility", -1, false);
        CountTXT.text = AbilityController.GetSaveAbility("RotateAbility").ToString();
        ControllOnOffAbility();
        FigureSelectControll(false);
    }

    private void Start()
    {
        AbbilityButton.onClick.AddListener(ControllOnOffAbility);
        CountTXT.text = AbilityController.GetSaveAbility("RotateAbility").ToString();
    }
    private void Update()
    {
        if (CheckAbility("RotateAbility"))
            SetAbility();
    }
}
