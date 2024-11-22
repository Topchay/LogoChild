using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level3 : MonoBehaviour
{
    private UIManager UIManager;

    [SerializeField] private Sprite[] worms;
    [SerializeField] private Sprite[] trueFalse;
    [SerializeField] private GameObject winPanel, loosPanel;

    [SerializeField] private Text timerText;
    [SerializeField] private float cTime = 10;
    private bool timer;

    [SerializeField] private Image[] img,apple;

    private string[] takesWorm;

    private int countWorms = 0;

    void Start()
    {
        takesWorm = new string[4];
        timer = true;
        winPanel.SetActive(false);
        loosPanel.SetActive(false);
        UIManager = FindObjectOfType<UIManager>();

        SetWormsPosition();
        SetAnimals();
    }

    void SetWormsPosition()
    {
        for (int i = 0; i < img.Length; i++)
        {
            img[i].transform.localPosition = apple[i].transform.localPosition;
        }
    }

    private void Update()
    {
        if (timer)
        {
            cTime -= Time.deltaTime;
            timerText.text = Mathf.Ceil(cTime).ToString();

            if (cTime <= 0)
            {
                loosPanel.SetActive(true);
                timer = false;
            }
        }
    }

    public void SetAnimals()
    {
        ShuffleSmile();

        for (int i = 0; i < img.Length; i++)
        {
            img[i].sprite = worms[i];
        }
    }

    void ShuffleSmile()
    {
        for (int i = 0; i < worms.Length - 1; i++)
        {
            int j = Random.Range(i, img.Length);

            Sprite temp = worms[i];
            worms[i] = worms[j];
            worms[j] = temp;
        }
    }

    public void GetIndexOfBtn(int indexBtn)
    {
        takesWorm[countWorms] = img[indexBtn].sprite.name;
        Destroy(img[indexBtn]);
        countWorms++;

        if (countWorms == 4)
        {
            timer=false;
            WinGame();
        }

    }


    void WinGame()
    {
        bool win = true;

        for (int i = 0; i < takesWorm.Length - 1; i++)
        {
            for (int j = i+1; j < takesWorm.Length; j++)
            {
                if (takesWorm[i] == takesWorm[j])
                {
                    win = false; break;
                }
            }
        }

        if (win)
        {
            winPanel.SetActive(true);

            if (Player.GameNum < SceneManager.GetActiveScene().buildIndex + 1)
            {
                Player.GameNum++;
                timer = false;
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
