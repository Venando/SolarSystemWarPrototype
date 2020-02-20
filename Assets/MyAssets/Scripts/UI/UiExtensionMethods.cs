using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static class UiExtensionMethods
{
    public static void TextFade(this MonoBehaviour monoBehaviour, TMP_Text text, float time, float delay, float endAlpha)
    {
        monoBehaviour.StartCoroutine(TextFade(text, time, delay, endAlpha));
    }

    private static IEnumerator TextFade(this TMP_Text text, float time, float delay, float endAlpha)
    {
        yield return new WaitForSecondsRealtime(delay);
        var startTime = Time.realtimeSinceStartup;
        var startAlpha = text.alpha;

        while (true)
        {
            var t = (Time.realtimeSinceStartup - startTime) / (time);
            if (t > 1)
                break;
            text.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            yield return null;
        }
        text.alpha = endAlpha;
    }
}
