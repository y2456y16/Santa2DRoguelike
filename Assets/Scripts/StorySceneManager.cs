using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.SceneManagement;
using TMPro;

public class StorySceneManager : MonoBehaviour
{
    [SerializeField] private GameObject character1;
    [SerializeField] private GameObject character2;
    [SerializeField] private GameObject storyUI_1;
    [SerializeField] private GameObject storyUI_2;
    [SerializeField] private GameObject storyUI_3;
    [SerializeField] private GameObject santaText;
    [SerializeField] private GameObject anyKeyText;

    [SerializeField] private Animator character1_ani;
    [SerializeField] private Animator character2_ani;

    private Vector3 character1_move = new Vector3(1.5f, 0f, 0f);
    private Vector3 character2_move = new Vector3(1.3f, 0f, 0f);

    private void Start()
    {
        StartCoroutine("CharacterAnimation");
    }

    IEnumerator CharacterAnimation()
    {
        SkipAnimation();
        while (true)
        {
            if (character2.transform.position.x < 1413)
            {
                character1.transform.position += character1_move;
                character2.transform.position += character2_move;
                yield return null;
            }
            else
            {
                character2_ani.SetBool("IsFaint", true);
                character1.transform.position += character1_move;
                yield return new WaitForSeconds(0.8f);
                character1_ani.SetBool("IsOO", true);
                break;
            }
        }
        yield return new WaitForSeconds(1f);
        storyUI_2.SetActive(true);
        yield return new WaitForSeconds(1f);
        storyUI_3.SetActive(true);
        yield return null;
        yield return new WaitForSeconds(2f);
        santaText.SetActive(true);
        yield return new WaitForSeconds(2f);
        anyKeyText.SetActive(true);
    }

    private void SkipAnimation()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("TestMainScene");
        }
    }
}
