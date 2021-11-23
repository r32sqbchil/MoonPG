using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setManager : MonoBehaviour
{
    public GameObject displaySet;
    public GameObject soundSet;
    public GameObject settings;
    public GameObject subSet;

    public void OnClickDisplay()
    {
        displaySet.SetActive(true);
        soundSet.SetActive(false);
    }

    public void OnClickSound()
    {
        displaySet.SetActive(false);
        soundSet.SetActive(true);
    }

    public void OnClickEnd()
    {
        subSet.SetActive(false);
        settings.SetActive(false);
    }

    public void OnClickTitleEnd()
    {
        settings.SetActive(false);
    }



    // public void OnClickResolution()
    // {
    //     Debug.Log("해상도 목록 펼치기");
    // }

    // public void OnClickRaito()
    // {
    //     Debug.Log("화면비율 목록 펼치기");
    // }

    // public void OnClickBrightness()
    // {
    //     Debug.Log("화면밝기 fill로 조절하는 내용");
    // }
}
