using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableMultiTouch : MonoBehaviour
{
    private void Awake()
    {
        Input.multiTouchEnabled = false;
    }
}
