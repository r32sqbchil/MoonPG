using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image fadeImage;

    float f_Time = 1f;


    public void FadeIn()
    {
        StartCoroutine(_FadeIn());
    }
    public void FadeOut()
    {
        StartCoroutine(_FadeOut());
    }

    IEnumerator FadeAll()
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
    IEnumerator _FadeIn()
    {
        fadeImage.gameObject.SetActive(true);

        fadeImage.color = new Color(255,255,255,0);

        float fadeAlpha = 0;
        while (fadeAlpha <= 1f)
        {
            fadeAlpha += Time.deltaTime / f_Time;
            fadeImage.color = new Color(255,255,255,fadeAlpha);
            
            yield return null;
        }
    }
    IEnumerator _FadeOut()
    {
        fadeImage.gameObject.SetActive(true);

        fadeImage.color = new Color(0,0,0,1);

        float fadeAlpha = 1;

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