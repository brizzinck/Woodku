using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;
public class Figure : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool _figureSelected = true;
    private Vector2 _mousePosition;
    private GlobalProperties _globalProperties;
    private List<Square> _squares = new List<Square>();
    private Grid _grid;
    private FigureSpawner _figureSpawn;
    private CombinationController _combinationController;
    private SpawnPanel _spawnPanel;
    private bool _selected;
    private bool _highCell;
    private Rect _clampRect;
    private Vector3 _rotate;
    public SpawnPanel SpawnPanel { get => _spawnPanel; }
    public bool FigureSelected { get => _figureSelected; set => _figureSelected = value; }
    public FigureSpawner FigureSpawn { get => _figureSpawn; }
    public List<Square> Squares { get => _squares; }
    public Vector3 Rotate { get => _rotate; set => _rotate = value; }

    public void SetSpawnPanel(SpawnPanel spawnPanel)
    {
        _spawnPanel = spawnPanel;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_figureSelected) return;
        _selected = true;
        _highCell = true;
        transform.parent = _grid.transform;
        _spawnPanel.Free = true;
        DOTween.Kill(this);
        AnimationFigure.AnimationScaleFigure(this, 1, 0.2f);
        SetSelectedFigure(false);
        _figureSelected = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_figureSelected) return;
        _highCell = false;
        _selected = false;
        Place();
        StartCoroutine(GameOver.CheckAllPlace());
        SetSelectedFigure(true);
    }
    public void ControllFadeFigure(Color spawnPanelColor, Color squareColor)
    {
        _spawnPanel.GetComponent<Image>().color = spawnPanelColor;
        foreach (Square square in _squares)
        {
            square.GetComponent<SpriteRenderer>().color = squareColor;
        }
    }
    public bool CheckForPlace()
    {
        if (_squares == null) SetSquares();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (!_squares[i].CheckForPlace())
            {
                return false;
            }
        }
        return true;
    }
    public void ClampPostion()
    {
        transform.localPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x, -715 - _clampRect.xMin, 730 - _clampRect.xMax),
            Mathf.Clamp(transform.localPosition.y, -1400, 1100 - _clampRect.yMax), -2);
    }
    public void SetClampRect()
    {
        _clampRect = Rect.zero;
        foreach (Square square in _squares)
        {
            Vector2 currentPostion = Quaternion.AngleAxis(transform.eulerAngles.z, Vector3.forward) * square.transform.localPosition;
            if (currentPostion.x > _clampRect.xMax) _clampRect.xMax = currentPostion.x;
            if (currentPostion.x < _clampRect.xMin) _clampRect.xMin = currentPostion.x;

            if (currentPostion.y > _clampRect.yMax) _clampRect.yMax = currentPostion.y;
            if (currentPostion.y < _clampRect.yMin) _clampRect.yMin = currentPostion.y;
        }
    }
    private void Start()
    {
        SetValues();
        AnimationFigure.AnimationScaleFigure(this, 0.5f, 0.2f);
    }
    private void Update()
    {
        Move();
    }
    private void SetValues()
    {
        _globalProperties = new GlobalProperties();
        _globalProperties.Set();
        _figureSpawn = FindObjectOfType<FigureSpawner>();
        _grid = FindObjectOfType<Grid>();
        _combinationController = FindObjectOfType<CombinationController>();
        _rotate = transform.eulerAngles;
        SetSquares();
        SetClampRect();
    }
    private void SetSquares()
    {
        _squares = new List<Square>();
        for (int i = 0; i < transform.childCount; i++)
            _squares.Add(GetComponentsInChildren<Square>()[i]);
    }
    private void Move()
    {
        if (_selected && _figureSelected)
        {
            Square.StopHighLightCell(_grid);
            _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mousePosition += Vector2.up;
            transform.position = _mousePosition;
            ClampPostion();
            SetHighLight();
        }
    }
    private void Place()
    {
        transform.localScale = new Vector3(1, 1, 1);
        if (CheckForPlace())
        {
            Square.StopHighLightCell(_grid);
            foreach (Square square in _squares)
            {
                square.MoveToGrid();
            }
            _combinationController.CheckGridCombinations();
            _figureSpawn.Spawn(false);
            AudioManager.PlayFigurDrop();
            Destroy(gameObject);
        }
        else
        {
            AnimationFigure.AnamationResetFigure(this, _spawnPanel);
        }      
    }
    private void SetHighLight()
    {
        if (CheckForPlace())
        {
            foreach (Square square in _squares)
            {
                if (_highCell) square.HighLightCell();
            }
        }
    }
    private void SetSelectedFigure(bool selected)
    {
        foreach (Figure figure in _figureSpawn.Figures)
        {
            figure.FigureSelected = selected;
        }
    }
}
