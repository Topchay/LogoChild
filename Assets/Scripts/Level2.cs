using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level2 : MonoBehaviour
{
    private UIManager UIManager;

    [SerializeField] private Sprite[] animals;
    [SerializeField] private Sprite[] trueFalse;
    [SerializeField] private GameObject winPanel,loosPanel, daleeButton, planetaSlonov;

    [SerializeField] private Text timerText;
    [SerializeField] private float cTime = 20;
    private bool timer;

    [SerializeField] private Transform bananas;

    [SerializeField] private Image[] img;


    private int countBanans = 0;

    public void StartEmiter()
    {
        daleeButton.SetActive(true);
        planetaSlonov.SetActive(true);

    }

    void Start()
    {
        timer = true;
        winPanel.SetActive(false);
        loosPanel.SetActive(false);
        UIManager = FindObjectOfType<UIManager>();
        
        SetAnimals();
    }

    IEnumerator Timer(float time)
    {
        for (int i = (int)time; i >-1 ; i--)
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

    public void SetAnimals()
    {
        ShuffleSmile();

        for (int i = 0; i < img.Length; i++)
        {
            img[i].sprite = animals[i];
        }
    }

    void ShuffleSmile()
    {
        for (int i = 0; i < animals.Length - 1; i++)
        {
            int j = Random.Range(i, img.Length);

            Sprite temp = animals[i];
            animals[i] = animals[j];
            animals[j] = temp;
        }
    }

    public void GetIndexOfBtn(int indexBtn)
    {
        if (img[indexBtn].sprite.name.Equals("slon"))
        {
            img[indexBtn].GetComponent<Animation>().Play();
            countBanans++;
        }
    }

    public void DestroyBananas()
    {
        Destroy(bananas.GetChild(0).gameObject);
        WinGame();
    }

    void WinGame()
    {
        if (countBanans == 10)
        {
            winPanel.SetActive(true);

            if (Player.GameNum < SceneManager.GetActiveScene().buildIndex+1)
            {
                Player.GameNum++;
                timer = false;
                UIManager.SetBubaImage();
            }
        }
    }
}
