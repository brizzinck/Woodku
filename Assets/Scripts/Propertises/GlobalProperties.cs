using UnityEngine;

public struct GlobalProperties
{
    public Vector2 GridOffset;
    public Vector2 Step;

    public float SquareScale;
    public float EverySquareOffset;
    public void Set()
    {
         GridOffset = new Vector2(-715, 1100);
         Step = new Vector2(180.5f, 180.5f);
         SquareScale = 1.8f;
         EverySquareOffset = 0.5f;
    }
}
