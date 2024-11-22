using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //private Player Player;
    private LevelsBar LevelsBar;

    [SerializeField] private Button startBtn;

    [SerializeField] private Sprite[] bubaSprite;
    [SerializeField] private Image buba;

    static public bool isPlay = false;
    private int gameNum;

    private void Awake()
    {

        LevelsBar = GetComponent<LevelsBar>();
    }

    void Start()
    {
        Player.LoadPlayer();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            startBtn.interactable = false;
            LevelsBar.CreateGamesBar(6, Player.GameNum);
        }

        SetBubaImage();
    }

    public void CanStartButtonPress(int indexBtn)
    {
        print(gameNum);
        gameNum = indexBtn + 1;
        startBtn.interactable = true;
    }

    public void StartGame()
    {
        FindObjectOfType<WebCam>().StopCam();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public void ReStartGame()
    {
        FindObjectOfType<WebCam>().StopCam();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextGame()
    {
        FindObjectOfType<WebCam>().StopCam();

        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings-1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    public void SetBubaImage()
    {
        buba.sprite = bubaSprite[Player.GameNum-1];
        Player.SavePlayer();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ExitToMenu()
    {
        FindObjectOfType<WebCam>().StopCam();
        SceneManager.LoadScene(0);
    }

}
