using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{
    [SerializeField] private Grid _grid;
    [SerializeField] private CanvasGroup _gameOverPanel;
    [SerializeField] private FigureSpawner _figureSpawner;
    [SerializeField] private Button _closeGameOverButton;
    [SerializeField] private Button _settingsButton;
    private static GlobalProperties _globalProperties;
    private static Grid _gridStatic;
    private static CanvasGroup _gameOverPanelStatic;
    private static FigureSpawner _figureSpawnerStatic;
    private static Button _settingsButtonStatic;

    public static IEnumerator CheckAllPlace()
    {
        yield return null;
        if (FindObjectsOfType<Figure>() != null)
        {
            Figure[] figures = FindObjectsOfType<Figure>();
            if (figures.Length >= 1)
            {
                List<GridSquare> gridSquares = _gridStatic.GridSquares;
                bool isPlace = false;
                foreach (Figure figure in figures)
                {
                    bool isLocalPLace = false;
                    Vector3 currentTransformFigure = figure.transform.localPosition;
                    figure.transform.parent = _gridStatic.transform;
                    for (int i = 0; i < gridSquares.Count; i++)
                    {
                        figure.transform.localPosition = new Vector2(
                            Mathf.RoundToInt(gridSquares[i].transform.localPosition.x / _globalProperties.Step.x),
                            Mathf.RoundToInt(gridSquares[i].transform.localPosition.y / _globalProperties.Step.y)) * _globalProperties.Step.x;
                        figure.ClampPostion();
                        if (figure.CheckForPlace())
                        {
                            isLocalPLace = true;
                            isPlace = true;
                        }
                    }
                    Image spawnPanleImg = figure.SpawnPanel.GetComponent<Image>();
                    if (!isLocalPLace) figure.ControllFadeFigure(Color.red, new Color(1, 1, 1, 0.5f));
                    else figure.ControllFadeFigure(Color.white, new Color(1, 1, 1, 1f));
                    spawnPanleImg.color = new Color(spawnPanleImg.color.r, spawnPanleImg.color.g, spawnPanleImg.color.b, 0.5f);
                    figure.transform.parent = figure.SpawnPanel.transform;
                    figure.transform.localPosition = new Vector3(currentTransformFigure.x, currentTransformFigure.y, 0);
                }
                if (!isPlace)
                {
                    GameAnimation.DoFade(_gameOverPanelStatic, 1);
                    FigureSelected(figures, false);
                    _gameOverPanelStatic.blocksRaycasts = true;
                    _settingsButtonStatic.interactable = false;
                }
                else
                {
                    FigureSelected(figures, true);
                    CloseGameOver();
                }
            }
            else _figureSpawnerStatic.Spawn(true);        
        }
        Square.StopHighLightCell(_gridStatic);

        static void FigureSelected(Figure[] figures, bool selected)
        {
            foreach (Figure figure in figures)
            {
                figure.FigureSelected = selected;
            }
        }
    }

    private void Start()
    {
        _globalProperties = new GlobalProperties();
        _globalProperties.Set();
        _closeGameOverButton.onClick.AddListener(CloseGameOver);
        _gridStatic = _grid;
        _gameOverPanelStatic = _gameOverPanel;
        _figureSpawnerStatic = _figureSpawner;
        _settingsButtonStatic = _settingsButton;
    }

    private static void CloseGameOver()
    {
        GameAnimation.DoFade(_gameOverPanelStatic, 0);
        _gameOverPanelStatic.blocksRaycasts = false;
        _settingsButtonStatic.interactable = true;
    }
}
