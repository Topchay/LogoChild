using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    private UIManager UIManager;

    [SerializeField] private Sprite[] smiles;
    [SerializeField] private Sprite[] trueFalse;
    [SerializeField] private Sprite[] levels;
    [SerializeField] private GameObject winPanel,loosPanel;


    [SerializeField] private Transform levelBar;

    [SerializeField] private Image[] img;
    [SerializeField] private Text timerText;
    [SerializeField] private float cTime = 20;


    private int countTrue = 0;
    private int countFalse = 0;

    void Start()
    {
        winPanel.SetActive(false);
        loosPanel.SetActive(false);
        UIManager = FindObjectOfType<UIManager>();
        SetLevelsSprite();
        SetSmiles();


    }

    IEnumerator Timer(float time)
    {
        for (int i = (int)time; i > -1; i--)
        {

            timerText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        loosPanel.SetActive(true);
    }

    public void StartGame()
    {
        StartCoroutine(Timer(cTime));
    }


    void SetSmiles()
    {
        ShuffleSmile();

        for (int i = 0; i < img.Length; i++)
        {
            img[i].sprite = smiles[i];
        }
    }

    void ShuffleSmile()
    {
        for (int i = 0; i < smiles.Length - 1; i++)
        {
            int j = Random.Range(i, img.Length);

            Sprite temp = smiles[i];
            smiles[i] = smiles[j];
            smiles[j] = temp;
        }
    }

    void SetLevelsSprite()
    {
        for(int i = 0;i < levelBar.childCount;i++)
        {
            levelBar.GetChild(i).GetComponentInChildren<Text>().text = (i+1).ToString();

            if(i== countTrue + countFalse) levelBar.GetChild(i).GetComponent<Image>().sprite = levels[0];
            else levelBar.GetChild(i).GetComponent<Image>().sprite = levels[1];
        }
    }

    public void GetIndexOfBtn(int indexBtn)
    {
        levelBar.GetChild(countTrue + countFalse).GetComponentInChildren<Text>().enabled = false;

        if (img[indexBtn].sprite.name.Equals("1"))
        {
            levelBar.GetChild(countTrue + countFalse).GetComponent<Image>().sprite = trueFalse[0];
            countTrue++;
            SetSmiles();
        }
        else
        {
            levelBar.GetChild(countTrue + countFalse).GetComponent<Image>().sprite = trueFalse[1];
            countFalse++;
        }

        if (countTrue + countFalse < 10) levelBar.GetChild(countTrue + countFalse).GetComponent<Image>().sprite = levels[0];

        if (countTrue+countFalse == 10) WinGame();
    }

    void WinGame()
    {

        if (countTrue >= countFalse)
        {
            winPanel.SetActive(true);

            if (Player.GameNum < SceneManager.GetActiveScene().buildIndex+1)
            {
                Player.GameNum++;
                UIManager.SetBubaImage();
            }

        }
        else
        {
            loosPanel.SetActive(true);
        }
    }
}
