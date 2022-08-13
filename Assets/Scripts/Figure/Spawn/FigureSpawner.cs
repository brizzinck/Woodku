using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureSpawner : MonoBehaviour
{
    [SerializeField] private SpawnPanel[] _positionFigures;
    [SerializeField] private Figure[] _figuresPrefabs;
    private List<Figure> _figures = new List<Figure>();
    public List<Figure> Figures { get => _figures; }

    public void Spawn(bool ignore)
    {
        StartCoroutine(GameOver.CheckAllPlace());
        foreach (SpawnPanel spawnPanel in _positionFigures)
        {
            if (!spawnPanel.Free && !ignore)
                return;
        }
        _figures = new List<Figure>();
        foreach (SpawnPanel spawnPanel in _positionFigures)
        {
            Figure figure = Instantiate(_figuresPrefabs[Random.Range(0, _figuresPrefabs.Length)], spawnPanel.transform);
            figure.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 4) * 90);
            figure.transform.localScale = Vector3.zero;
            figure.SetSpawnPanel(spawnPanel);
            spawnPanel.Free = false;
            _figures.Add(figure);
        }
        StartCoroutine(GameOver.CheckAllPlace());
    }
    private void Start()
    {
        Spawn(false);
    }
}
