using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject optionUI;
    [SerializeField] private GameObject statusUI;


    //IntroScene ------------------------------------------------------------------------
    //UI
    public void OnOptionUIButton()
    {
        optionUI.SetActive(true);
    }

    public void OffOptionUIButton()
    {
        optionUI.SetActive(false);
    }

    //SceneLoad
    public void MainSceneLoad()
    {
        //설정UI가 active true 상태이면, 버튼을 눌러도 실행되지 않도록 설정
        if (!optionUI.activeSelf)
        {
            SceneManager.LoadScene("TestMainScene");
        }
    }

    //MainScene ------------------------------------------------------------------------
    //UI
    public void OnStatusUIButton()
    {
        statusUI.SetActive(true);
    }

    public void OffStatusUIButton()
    {
        statusUI.SetActive(false);
    }
}
