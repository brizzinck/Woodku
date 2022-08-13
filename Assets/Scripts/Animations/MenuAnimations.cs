using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class MenuAnimations : MonoBehaviour
{
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private GameObject shopButton;
    private Animator _startAnim;
    private Animator _shopAnim;
    
    private void Start()
    {
        _startAnim = startButton.GetComponent<Animator>();
        _shopAnim = shopButton.GetComponent<Animator>(); 
    }
    public void LogoEnd()
    {
        _startAnim.SetTrigger("Start");
    }
    public void LogoEndOff()
    {
        _shopAnim.SetTrigger("Shop");
    }
}
