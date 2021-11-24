using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSetting : MonoBehaviour
{
    public GameObject set;

    public void OnClickNew()
    {
        SceneManager.LoadScene(1);  // 인게임으로 이동
    }

    // public void OnClickLoad()
    // {
    //     SceneManager.LoadScene(1);  // 인게임으로 이동
    // }

    public void OnClickOption()
    {
        set.SetActive(true);
    }

    public void OnClickEnd()
    {
//  유니티 에디터에서는 동작하지 않기 때문에 전처리기 지시어 사용
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}