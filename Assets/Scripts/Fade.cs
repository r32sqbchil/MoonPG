using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image fadeImage;

    float time = 0f;
    float f_Time = 1f;

    bool fadeCheck = false;

    public void FadeIn()
    {
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        fadeCheck = true;
        fadeImage.gameObject.SetActive(true);
        time = 0f;
        Color alpha = fadeImage.color;

        while (alpha.a < 1f)
        {
            time += Time.deltaTime / f_Time;
            alpha.a = Mathf.Lerp(0, 1, time);
            fadeImage.color = alpha;
            
            yield return null;
        }

        time = 0f;

        yield return new WaitForSeconds(1f);

        while (alpha.a > 0f)
        {
            time += Time.deltaTime / f_Time;
            alpha.a = Mathf.Lerp(1, 0, time);
            fadeImage.color = alpha;

            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
        fadeCheck = false;

        yield return null;
    }
}
