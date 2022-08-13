using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FiguresAbility : MonoBehaviour
{
    [SerializeField] private AnimationFigure _animationFigure;
    public void RotateFigure(Figure figure, RotateAbility rotateAbility)
    {
        Vector3 rotate = figure.transform.eulerAngles;
        rotate = figure.Rotate;
        rotate = new Vector3(rotate.x, rotate.y, rotate.z + 90);
        figure.Rotate += Vector3.forward * 90;
        _animationFigure.AnimationRotateFigure(figure, rotateAbility, rotate, 0.2f);
    }
    public void RespawnFigure(List<Figure> figures)
    {
        foreach (Figure figure in figures)
        {
            if (figure == null) continue;
            StartCoroutine(DestroyFigure(figure, null));
        }
        StartCoroutine(GameOver.CheckAllPlace());
    }
    public IEnumerator DestroyFigure(Figure figure, DeleteAbility deleteAbility)
    {
        figure.SpawnPanel.Free = true;
        figure.FigureSpawn.Spawn(false);
        GameObject figureObj = figure.gameObject;
        AnimationFigure.AnimationScaleFigure(figure, 0, 0.2f);
        AnimationFigure.AnimationScaleFigure(figure, 0, 0.2f);
        figure.SpawnPanel.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        Destroy(figure);
        yield return new WaitForSeconds(0.3f);
        if (deleteAbility != null) deleteAbility.FigureSelectControll(true);
        Destroy(figureObj);
    }
}
