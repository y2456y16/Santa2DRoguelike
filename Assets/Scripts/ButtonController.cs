using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject optionUI;
    [SerializeField] private GameObject statusUI;

    private StorySceneManager story;

    private void Awake()
    {
        story = GetComponentInChildren<StorySceneManager>();
    }

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
        //����UI�� active true �����̸�, ��ư�� ������ ������� �ʵ��� ����
        if (!optionUI.activeSelf)
        {
            story.storyUI_1.SetActive(true);
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
