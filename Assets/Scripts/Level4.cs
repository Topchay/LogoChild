using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level4 : MonoBehaviour
{
    private UIManager UIManager;

    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource aud;

    [SerializeField] private Sprite[] blin;
    [SerializeField] private Sprite[] trueFalse;
    [SerializeField] private GameObject winPanel, loosPanel;

    [SerializeField] private Image[] img;

    private int countClip = 0;

    void Start()
    {
        winPanel.SetActive(false);
        loosPanel.SetActive(false);
        UIManager = FindObjectOfType<UIManager>();

        SetAnimals();
    }

    public void PlayClip()
    {
        aud.clip = clips[countClip];
        aud.Play();
    }

    private void Update()
    {
    }

    public void SetAnimals()
    {
        ShuffleSmile();

        for (int i = 0; i < img.Length; i++)
        {
            img[i].sprite = blin[i];
        }
    }

    void ShuffleSmile()
    {
        for (int i = 0; i < blin.Length - 1; i++)
        {
            int j = Random.Range(i, img.Length);

            Sprite temp = blin[i];
            blin[i] = blin[j];
            blin[j] = temp;
        }
    }

    public void GetIndexOfBtn(int indexBtn)
    {
        Destroy(img[indexBtn]);
        countClip++;

        if (countClip == 4)
        {
            WinGame();
        }

    }


    void WinGame()
    {
        bool win = true;


        if (win)
        {
            winPanel.SetActive(true);

            if (Player.GameNum < SceneManager.GetActiveScene().buildIndex + 1)
            {
                Player.GameNum++;
                UIManager.SetBubaImage();
            }
        }
        else
        {
            loosPanel.SetActive(true);
        }


    print(win);
    }
}
