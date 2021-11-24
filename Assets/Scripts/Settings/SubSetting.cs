using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SubSetting : MonoBehaviour
{
    public GameObject set;
    public GameObject subSet;

    // public void OnClickSave()
    // {
    //     Debug.Log("저장");
    // }

    public void OnClickOption()
    {
        set.SetActive(true);
        subSet.SetActive(false);
    }

    public void OnClickMain()
    {
        SceneManager.LoadScene(0);  // 메인화면으로 이동
    }

    // !! close 버튼은 GameObject.SetActive로 구현 가능하다. '계속하기'도 같은 방식으로 구현한다.
}