using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private int _columns = 0;
    [SerializeField] private int _rows = 0;
    [SerializeField] private float _squaresGap = 0.1f;
    [SerializeField] private GridSquare _gridSquare;
    [SerializeField] private Vector2 _startPosition = new Vector2(0.0f, 0.0f);
    private GlobalProperties _globalProperties;
    private Vector2 _offset = new Vector2(0.0f, 0.0f);
    private List<GridSquare> _gridSquares = new List<GridSquare>();
    public List<GridSquare> GridSquares { get => _gridSquares; }
    private void Awake()
    {
        _globalProperties = new GlobalProperties();
        _globalProperties.Set();
        CreateGrid();
    }
    private void CreateGrid()
    {
        SpawnGridSquares();
        SetGridSquaresPositions();
    }
    private void SpawnGridSquares()
    {
        int square_index = 0;

        for (var row = 0; row < _rows; row++)
        {
            for(var column = 0; column < _columns; column++)
            {
                if ((row >= 3 && row <= 5) && column == 0) square_index = 3;
                if ((row >= 3 && row <= 5) && column == 3) square_index = 0;
                if (row >= 6 && column == 0) square_index = 0;
                _gridSquares.Add(Instantiate(_gridSquare));
                _gridSquares[_gridSquares.Count - 1].Coordinates = new Vector2(column, row);
                _gridSquares[_gridSquares.Count - 1].transform.SetParent(transform);
                _gridSquares[_gridSquares.Count - 1].transform.localScale = new Vector3(_globalProperties.SquareScale, _globalProperties.SquareScale, _globalProperties.SquareScale);
                _gridSquares[_gridSquares.Count - 1].SetImage(true);
                if (square_index > 2 && square_index < 6) _gridSquares[_gridSquares.Count - 1].SetImage(false);
                square_index++;
                if (square_index >= 6) square_index = -3;
            }
        }
    }
    private void SetGridSquaresPositions()
    {
        int columnNumber = 0;
        int rowNumber = 0;
        Vector2 squareGapNumber = new Vector2(0.0f, 0.0f);
        bool rowMoved = false;

        var squareRect = _gridSquares[0].GetComponent<RectTransform>();
        _offset.x = squareRect.rect.width * squareRect.transform.localScale.x + _globalProperties.EverySquareOffset;
        _offset.y = squareRect.rect.height * squareRect.transform.localScale.y + _globalProperties.EverySquareOffset;

        foreach (GridSquare square in _gridSquares)
        {
            if (columnNumber + 1 > _columns)
            {
                squareGapNumber.x = 0;
                columnNumber = 0;
                rowNumber++;
                rowMoved = false;
            }

            var xOffset = _offset.x * columnNumber + (squareGapNumber.x * _squaresGap);
            var yOffset = _offset.y * rowNumber + (squareGapNumber.y * _squaresGap);

            if (columnNumber > 0 && columnNumber % 3 == 0)
            {
                squareGapNumber.x++;
                xOffset += _squaresGap;
            }

            if (rowNumber > 0 && rowNumber % 3== 0 && rowMoved == false)
            {
                rowMoved = true;
                squareGapNumber.y++;
                yOffset += _squaresGap;
            }

            square.GetComponent<RectTransform>().anchoredPosition = new Vector2(_startPosition.x + xOffset, _startPosition.y - yOffset);

            square.GetComponent<RectTransform>().localPosition = new Vector3(_startPosition.x + xOffset, _startPosition.y - yOffset, 0.0f);

            columnNumber++;
        }

    }

}
