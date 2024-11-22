using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level8 : MonoBehaviour
{
    [SerializeField] private GameObject winPanel, loosPanel, littleMansMain, daleeButton, planetaVody;
    [SerializeField] private GameObject[] littleMans;

    public Sprite[] trueFalse;
    public GameObject levelBar;
    public int currentStage = 0;  // Количество заполненных ячеек 

    [SerializeField] private AudioClip audioIntro;
    [SerializeField] private AudioClip audioWin;
    [SerializeField] private AudioClip audioLose;
    private AudioSource audioSource;

    private UIManager UIManager;

    private void Awake()
    {
        UIManager = FindObjectOfType<UIManager>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioIntro;
    }

    public void StartEmiter()
    {
        daleeButton.SetActive(true);
        planetaVody.SetActive(true);
    }

    public void ClickStartGame()   // Старт игры пр иклике
    {
        littleMansMain.SetActive(true);
        levelBar.SetActive(true);

        if (winPanel != null) winPanel.SetActive(false);
        if (loosPanel != null) loosPanel.SetActive(false);

        StartCoroutine(PlayAudioAndActivate());
    }

    private IEnumerator PlayAudioAndActivate()  // Сначала проигрываем её балабольство, потом начинается игра
    {
        audioSource.Play();

        yield return new WaitForSeconds(audioIntro.length);

        audioSource.clip = null;

        ShuffleArray(littleMans);         // Перемешиваем массив объектов
        littleMans[0].SetActive(true);
    }

    public void SpawnLittleMans()   // Метод для появления новых человечков по очереди (проверяет поставлен в ячейку ли предыдущий) 
    {
        for (int i = 1; i < littleMans.Length; i++)
        {
            if (i <= currentStage)
            {
                littleMans[i].SetActive(true);
            }
        }
    }

    private void ShuffleArray(GameObject[] array)    // Перемешиваем порядок человечков
    {
        for (int i = 0; i < array.Length; i++)
        {
            int rnd = Random.Range(0, array.Length);
            GameObject tempGO = array[rnd];
            array[rnd] = array[i];
            array[i] = tempGO;
        }
    }

    public void WinGame()   // Проверка на Победу или поражение
    {
        if (currentStage == 3)
        {
            bool allCorrect = true;

            for (int i = 0; i < levelBar.transform.childCount; i++)
            {
                Transform child = levelBar.transform.GetChild(i);
                Image imageComponent = child.GetComponent<Image>();

                if (imageComponent == null || imageComponent.sprite != trueFalse[1])
                {
                    allCorrect = false;
                    break;
                }
            }

            if (allCorrect)
            {
                winPanel.SetActive(true);
                audioSource.clip = audioWin;
                audioSource.Play();
            }
            else
            {
                loosPanel.SetActive(true);
                audioSource.clip = audioLose;
                audioSource.Play();
            }
        }
    }
}