using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject optionUI;
    [SerializeField] private GameObject statusUI;
    [SerializeField] private GameObject status_optionUI;

    private StorySceneManager story;

    private void Awake()
    {
        story = GetComponentInChildren<StorySceneManager>();
    }

    //IntroScene ------------------------------------------------------------------------
    //UI
    public void OnOptionUIButton()
    {
        statusUI.SetActive(false);
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
            story.storyUI_1.SetActive(true);//UI setactive보다는 생성이 더 좋을것같음
        }
    }

    //MainScene ------------------------------------------------------------------------
    //UI
    public void OnStatusUIButton()
    {
        status_optionUI.SetActive(true);
        statusUI.SetActive(true);
        optionUI.SetActive(false);
    }

    public void OffStatusUIButton()
    {
        status_optionUI.SetActive(false);
        statusUI.SetActive(true);
        optionUI.SetActive(false);
    }
}
