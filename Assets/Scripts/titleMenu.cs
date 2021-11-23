using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class titleMenu : MonoBehaviour
{
    public GameObject settings;

    public void OnClickNewStart()
    {
        SceneManager.LoadScene(1);  // 인게임으로 이동
    }

    // public void OnClickContinue()
    // {
    //     SceneManager.LoadScene(1);  // 인게임으로 이동
    // }

    public void OnClickOption()
    {
        settings.SetActive(true);
    }

    // public void OnClickSetting()
    // {
    //     Debug.Log("설정창 불러오기");
    // }

    public void OnClickExit()
    {
//  유니티 에디터에서는 동작하지 않기 때문에 전처리기 지시어 사용
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}