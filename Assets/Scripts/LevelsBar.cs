using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelsBar : MonoBehaviour
{

    [SerializeField] private GameObject levelBTN;
    [SerializeField] private Transform LevelBar;

    public void CreateGamesBar(int count, int levelUnlock)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject btn = Instantiate(levelBTN, LevelBar);
            btn.name = $"{i + 1}";
            btn.transform.GetChild(0).GetComponent<Text>().text = (i + 1).ToString();

            if (i < levelUnlock) btn.GetComponent<Button>().interactable = true;
            else btn.GetComponent<Button>().interactable = false;
        }
    }
}
