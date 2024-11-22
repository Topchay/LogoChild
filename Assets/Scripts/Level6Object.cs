using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6Object : MonoBehaviour
{
    Level6 Level6;

    public int tagValue; // ѕуста€ переменна€ дл€ хранени€ значени€ тега

    private void Awake()
    {
        Level6 = FindObjectOfType<Level6>();
    }

    private void OnMouseDown()
    {
        string tag = gameObject.tag;
        int.TryParse(tag, out int parsedValue);
        tagValue = parsedValue;

        Level6.CheckClicked(tagValue);
    }
}
