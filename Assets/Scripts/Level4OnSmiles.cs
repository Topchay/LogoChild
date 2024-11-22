using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level4OnSmiles : MonoBehaviour
{
    Level4 Level4;

    public int tagValue; // ѕуста€ переменна€ дл€ хранени€ значени€ тега
    private Image imageComponent;

    private void Awake()
    {
        Level4 = FindObjectOfType<Level4>();
        imageComponent = GetComponent<Image>();
    }

    private void OnMouseDown()
    {
        if (Level4.isGame)
        {
            imageComponent.enabled = true;
            string tag = gameObject.tag;
            int.TryParse(tag, out int parsedValue);
            tagValue = parsedValue;

            Level4.CheckClicked(tagValue);
        }
    }
}
