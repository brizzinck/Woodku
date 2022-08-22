using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimationFigure : MonoBehaviour
{
    public static void AnamationResetFigure(Figure figure, SpawnPanel spawnPanel)
    {
        DOTween.Kill(figure);
        spawnPanel.Free = false;
        figure.transform.parent = spawnPanel.transform;
        AnimationScaleFigure(figure, 0.5f, 0.2f);
        AnimationResetPostion(figure, spawnPanel.transform, 0.2f);
    }
    public static void AnimationResetPostion(Figure figure, Transform transform, float timeResetPostion)
    {
        figure.transform.DOMove(transform.position, timeResetPostion);
    }
    public static void AnimationScaleFigure(Figure figure, float scale, float timeScale)
    {
        figure.transform.DOScale(scale, timeScale);
    }
    public void AnimationRotateFigure(Figure figure, RotateAbility rotateAbility,  Vector3 rotate, float timeRotate)
    {
        DOTween.Kill(figure);
        figure.transform.DORotate(rotate, timeRotate).SetEase(Ease.Linear).OnComplete(() =>
        {
            foreach (Square square in figure.Squares) square.transform.rotation = Quaternion.identity;
            figure.SetClampRect();
            rotateAbility.FigureSelectControll(true);
            StartCoroutine(GameOver.CheckAllPlace());
        });
    }
}
