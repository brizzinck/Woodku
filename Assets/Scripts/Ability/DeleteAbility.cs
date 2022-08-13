using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteAbility : Ability
{
    protected override void ControllOnOffAbility()
    {
        base.ControllOnOffAbility();
    }
    protected override void SetAbility()
    {
        StartCoroutine(FiguresAbility.DestroyFigure(CheckClickOnFigure(), this));
        AbilityController.SetSaveAbility("DeleteAbility", -1, false);
        CountTXT.text = AbilityController.GetSaveAbility("DeleteAbility").ToString();
        ControllOnOffAbility();
        FigureSelectControll(false);
    }
    private void Start()
    {
        AbbilityButton.onClick.AddListener(ControllOnOffAbility);
        CountTXT.text = AbilityController.GetSaveAbility("DeleteAbility").ToString();
    }
    private void Update()
    {
        if (CheckAbility("DeleteAbility"))
            SetAbility();
    }
}
