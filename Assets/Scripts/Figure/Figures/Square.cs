using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Square : MonoBehaviour
{
    private GlobalProperties _globalProperties;
    private Vector2 _coordinates;
    private Grid _grid;
    public void HighLightCell()
    {
        GridSquare gridSquare = _grid.GridSquares[(int)(_coordinates.x - _coordinates.y * 9)];
        gridSquare.ControllHighLight(true);
    }

    public static void StopHighLightCell(Grid grid)
    {
        foreach (GridSquare gridSq in grid.GridSquares)
        {
            gridSq.ControllHighLight(false);
        }
    }

    public bool CheckForPlace()
    {
        SetCoordinates();
        if ((_coordinates.x < 0 || _coordinates.x >= 9) || (_coordinates.y <= -9 || _coordinates.y > 0)) return false;
        GridSquare gridSquare = _grid.GridSquares[(int)(_coordinates.x - _coordinates.y * 9)];
        if (gridSquare.Free)
            return true;
        return false;
    }
    public void MoveToGrid()
    {
        GridSquare grid = _grid.GridSquares[(int)(_coordinates.x - _coordinates.y * 9)];
        transform.parent = grid.transform;
        transform.localPosition = Vector3.zero + Vector3.back;
        grid.Free = false;
    }
    public void AnimationDestroy(float timeAnimation, float timeDestroy)
    {
        transform.DOScale(0, timeAnimation);
        Destroy(gameObject, timeDestroy);
    }
    private void Start()
    {
        SetValue();
        transform.rotation = Quaternion.identity;
    }
    private void SetValue()
    {
        _grid = FindObjectOfType<Grid>();
        _globalProperties = new GlobalProperties();
        _globalProperties.Set();
    }
    private void SetCoordinates()
    {
        Vector2 currentPosition = transform.localPosition;
        currentPosition = Quaternion.AngleAxis(transform.parent.eulerAngles.z, Vector3.forward) * currentPosition;     
        currentPosition += (Vector2)transform.parent.localPosition + (Vector2 )transform.parent.transform.parent.localPosition;
        currentPosition -= _globalProperties.GridOffset;
        currentPosition.x = Mathf.RoundToInt(currentPosition.x / _globalProperties.Step.x);
        currentPosition.y = Mathf.RoundToInt(currentPosition.y / _globalProperties.Step.y);
        _coordinates = currentPosition;
    }
}
