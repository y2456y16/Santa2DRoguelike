using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject optionUI;
    [SerializeField] private GameObject statusUI;



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
        SceneManager.LoadScene("TestMainScene");
    }
}
