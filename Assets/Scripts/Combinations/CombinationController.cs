using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CombinationController : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI[] _scoreTXT;
    [SerializeField] private TMPro.TextMeshProUGUI _coinTXT;
    private GlobalProperties _globalProperties;
    private List<int> _squareIndexDestroy = new List<int>();
    private Grid _grid;
    private int _combinations;
    private int _score;

    public int Score { get => _score; }

    public void CheckGridCombinations()
    {
        List<GridSquare> gridSquares = _grid.GridSquares.GetRange(0, _grid.GridSquares.Count);
        CheckHorizontalLine(gridSquares);
        CheckVerticalLine(gridSquares);
        CheckRects(gridSquares);
        UpdateScore();
        DeleteSquare();
    }
    private void Awake()
    {
        _globalProperties = new GlobalProperties();
        _globalProperties.Set();
        _grid = FindObjectOfType<Grid>();
        _coinTXT.text = Coins.GetCoin().ToString();
    }
    private void CheckHorizontalLine(List<GridSquare> gridSquares)
    {
        for (int y = 0; y < 9; y++)
        {
            bool destroy = true;
            for (int x = 0; x < 9; x++)
            {
                if (gridSquares[Index(x, y)].Free)
                {
                    destroy = false;
                    break;
                }
            }
            if (destroy)
            {
                _combinations++;
                for (int x = 0; x < 9; x++)
                {
                    _squareIndexDestroy.Add(Index(x, y));
                }
            }
        }
    }
    private void CheckVerticalLine(List<GridSquare> gridSquares)
    {
        for (int x = 0; x < 9; x++)
        {
            bool destroy = true;
            for (int y = 0; y < 9; y++)
            {
                if (gridSquares[Index(x, y)].Free)
                {
                    destroy = false;
                    break;
                }
            }
            if (destroy)
            {
                _combinations++;
                for (int y = 0; y < 9; y++)
                {
                    _squareIndexDestroy.Add(Index(x, y));
                }
            }
        }
    }
    private void CheckRects(List<GridSquare> gridSquares)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                bool destroy = true;
                for (int x = 0; x < 3; x++)
                {
                    for (int y = 0; y < 3; y++)
                    {
                        if (gridSquares[Index(i * 3 + x, j * 3 + y)].Free)
                        {
                            destroy = false;
                            break;
                        }
                    }
                    if (!destroy) break;
                }
                if (destroy)
                {
                    _combinations++;
                    for (int x = 0; x < 3; x++)
                    {
                        for (int y = 0; y < 3; y++)
                        {
                            _squareIndexDestroy.Add(Index(i * 3 + x, j * 3 + y));
                        }
                    }
                }
            }

        }
    }
    private void UpdateScore()
    {
        if (_squareIndexDestroy.Count == 0) _combinations = 0;
        else
        {
            _score += _combinations;
            Coins.SaveCoin(3);
        }

        for (int i = 0; i < _scoreTXT.Length; i++) _scoreTXT[i].text = _score.ToString();
        Score score = new Score();
        score.SaveScore(_score);
        Coins.SaveCoin(1);
        _coinTXT.text = Coins.GetCoin().ToString();
    }
    private void DeleteSquare()
    {
        for (int i = 0; i < _squareIndexDestroy.Count; i++)
        {
            _grid.GridSquares[_squareIndexDestroy[i]].Free = true;
            _grid.GridSquares[_squareIndexDestroy[i]].GetComponentInChildren<Square>().AnimationDestroy(0.5f, 1);
        }
        _squareIndexDestroy.Clear();
    }
    private int Index(int x, int y)
    {
        return x + y * 9;
    }
}
