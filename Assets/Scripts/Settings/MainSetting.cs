using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSetting : MonoBehaviour
{
    public GameObject displaySet;
    public GameObject soundSet;
    public GameObject set;
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
        set.SetActive(false);
    }

    public void OnClickTitleEnd()
    {
        set.SetActive(false);
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
