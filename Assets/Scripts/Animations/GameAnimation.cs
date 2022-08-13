using UnityEngine;
using DG.Tweening;
public class GameAnimation
{
    public static void DoFade(CanvasGroup image, int fade)
    {
        image.DOFade(fade, 0.6f);
    }
    public static void DoRotate(Transform transform, Vector3 rotate)
    {
        transform.DORotate(rotate, 0.6f);
    }
    public static void ControllUIFadeObj(CanvasGroup obj, bool ignore)
    {
        if (ignore)
        {
            obj.blocksRaycasts = false;
            DoFade(obj, 0);
            return;
        }
        else if (obj.blocksRaycasts)
        {
            obj.blocksRaycasts = false;
            DoFade(obj, 0);
        }
        else
        {
            obj.blocksRaycasts = true;
            DoFade(obj, 1);
        }
    }
}
