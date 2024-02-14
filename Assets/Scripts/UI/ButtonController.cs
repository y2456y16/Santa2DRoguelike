using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject optionUI;
    [SerializeField] private GameObject statusUI;
    [SerializeField] private GameObject status_optionUI;
    private GameObject storyUI;

    private StorySceneManager story;

    private void Awake()
    {
        story = GetComponentInChildren<StorySceneManager>();
    }

    private void Start()
    {
        status_optionUI.SetActive(false);
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
        storyUI = Resources.Load<GameObject>("Prifabs/Story_Canvas");
        //설정UI가 active true 상태이면, 버튼을 눌러도 실행되지 않도록 설정
        if (!optionUI.activeInHierarchy)
        {
            gameObject.SetActive(false);
            Instantiate(storyUI);

        }
    }

    //MainScene ------------------------------------------------------------------------
    //UI
    public void OnStatusUIButton()
    {
        Time.timeScale = 0f;
        status_optionUI.SetActive(true);
        statusUI.SetActive(true);
        optionUI.SetActive(false);
    }

    public void OffStatusUIButton()
    {
        status_optionUI.SetActive(false);
        statusUI.SetActive(true);
        optionUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
