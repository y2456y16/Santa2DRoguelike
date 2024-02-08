using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private GameObject optionUI;

    public void OnOptionUIButton()
    {
        optionUI.SetActive(true);
    }

    public void OffOptionUIButton()
    {
        optionUI.SetActive(false);
    }
}
