using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSquare : MonoBehaviour
{
    [SerializeField] private Image[] _highLights;
    private Image _highLight;
    public Vector2 Coordinates;
    public bool Free = true;
    [SerializeField] private Image _normalImage;
    [SerializeField] private List<Sprite> _normalImages;

    public void SetImage(bool setFirstImage)
    {
        _normalImage.GetComponent<Image>().sprite = setFirstImage ? _normalImages[0] : _normalImages[1];
        _highLight = setFirstImage ? _highLights[0] : _highLights[1];
    }
    public void ControllHighLight(bool active)
    {
        if (_highLight == null) return;
        _highLight.gameObject.SetActive(active);
    }
}
