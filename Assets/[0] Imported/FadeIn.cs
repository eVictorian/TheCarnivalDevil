using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using NaughtyAttributes;

public class FadeIn : MonoBehaviour
{
    public Image target;
    public float duration = 0.5f;

    Coroutine currentRoutine;

    public soDATA_GameEvent fadeInTrigger;
    public soDATA_GameEvent fadeOutTrigger;

    void Awake()
    {
        if (fadeInTrigger != null) fadeInTrigger.RegisterListener(FadeInFully);
        if (fadeOutTrigger != null) fadeOutTrigger.RegisterListener(FadeOutFully);
    }

    [Button] public void FadeInFully()
    {
        StartFade(0f);
    }

    [Button] public void FadeOutFully()
    {
        StartFade(1f);
    }

    void StartFade(float targetAlpha)
    {
        if (target == null) return;

        // Stop any previous fade to avoid overlap
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(FadeRoutine(targetAlpha));
    }

    IEnumerator FadeRoutine(float targetAlpha)
    {
        float startAlpha = target.color.a;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            Color c = target.color;
            c.a = Mathf.Lerp(startAlpha, targetAlpha, t);
            target.color = c;

            yield return null;
        }

        // Ensure exact final alpha
        Color final = target.color;
        final.a = targetAlpha;
        target.color = final;

        currentRoutine = null;
    }
}