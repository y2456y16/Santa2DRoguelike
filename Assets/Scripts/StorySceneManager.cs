using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.SceneManagement;

public class StorySceneManager : MonoBehaviour
{
    [SerializeField] private GameObject character1;
    [SerializeField] private GameObject character2;
    public GameObject storyUI_1;

    public Animator character1_ani;
    public Animator character2_ani;

    private void Start()
    {
    }


    private void Update()
    {
        if (storyUI_1.activeSelf)
        {
            if (character2.transform.position.x < 1400)
            {
                character1.transform.position += new Vector3(1.5f, 0f, 0f);
                character2.transform.position += new Vector3(1.3f, 0f, 0f);
            }
            else
            {
                character2_ani.SetBool("IsFaint", true);
                character1_ani.SetBool("IsOO", true);
            }
        }
    }
}
