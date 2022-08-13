using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected FigureSpawner FigureSpawner;
    [SerializeField] protected TMPro.TextMeshProUGUI CountTXT;
    [SerializeField] protected Button AbbilityButton;
    [SerializeField] private Image _imageBg;
    [SerializeField] private Ability[] _abilities;
    [SerializeField] protected string AbilityName;
    [SerializeField] protected FiguresAbility FiguresAbility;
    private bool _isAbility;
    public bool IsAbility { get => _isAbility; set => _isAbility = value; }
    protected Image ImageBg { get => _imageBg; }

    public void FigureSelectControll(bool select)
    {
        foreach (Figure figure in FigureSpawner.Figures)
        {
            figure.FigureSelected = select;
        }
    }

    protected abstract void SetAbility();
    virtual protected void ControllOnOffAbility()
    {
        AudioManager.PlayButton();
        if (AbilityController.GetSaveAbility(AbilityName) > 0 || IsAbility)
        {
            IsAbility = !IsAbility;
            HighAbility();
            foreach (Figure figure in FigureSpawner.Figures)
            {
                if (IsAbility)  figure.FigureSelected = false;
                if (!IsAbility) figure.FigureSelected = true;
            }
        }
    }
    protected void HighAbility()
    {
        bool isAbility = _isAbility;
        foreach (Ability ability in _abilities)
        {
            ability.IsAbility = false;
            _isAbility = isAbility;
            if (!ability.IsAbility) ability.ImageBg.color = new Color(ImageBg.color.r, ImageBg.color.g, ImageBg.color.b, 0);
            else if (ability.IsAbility) _imageBg.color = new Color(ImageBg.color.r, ImageBg.color.g, ImageBg.color.b, 0.03f);
        }
    }
    protected bool CheckAbility(string nameAbility)
    {
        if (IsAbility)
        {
            if (AbilityController.GetSaveAbility(AbilityName) <= 0) return false;
            else if (CheckClickOnFigure() != null)
            {
                return true;
            }
        }
        return false;
    }
    protected Figure CheckClickOnFigure()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit2D = Physics2D.Raycast(mousePosition, Vector2.up);

        if (hit2D != null && hit2D.transform != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Transform objectHit = hit2D.transform;
                if (objectHit.TryGetComponent(out Figure figure))
                {
                    return figure;
                }
            }
        }
        return null;
    }
}
