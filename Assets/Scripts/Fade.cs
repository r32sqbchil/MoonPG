using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image fadeImage;

    float time = 0f;
    float f_Time = 1f;

    // bool fadeCheck = false;

    public void FadeIn()
    {
        StartCoroutine(FadeFlow());
    }

    IEnumerator FadeFlow()
    {
        fadeImage.gameObject.SetActive(true);

        fadeImage.color = new Color(0,0,0,0);

        float fadeAlpha = 0;
        while (fadeAlpha <= 1f)
        {
            fadeAlpha += Time.deltaTime / f_Time;
            fadeImage.color = new Color(0,0,0,fadeAlpha);
            
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        while (fadeAlpha > 0f)
        {
            fadeAlpha -= Time.deltaTime / f_Time;
            fadeImage.color = new Color(0,0,0,fadeAlpha);

            yield return null;
        }

        fadeImage.color = new Color(0,0,0,0);

        fadeImage.gameObject.SetActive(false);
    }
}