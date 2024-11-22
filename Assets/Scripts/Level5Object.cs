using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5Object : MonoBehaviour
{
    Level5 Level5;

    public int tagValue; // ѕуста€ переменна€ дл€ хранени€ значени€ тега

    private void Awake()
    {
        Level5 = FindObjectOfType<Level5>();
    }

    private void OnMouseDown()
    {
        string tag = gameObject.tag;
        int.TryParse(tag, out int parsedValue);
        tagValue = parsedValue;

        Level5.CheckClicked(tagValue);
    }
}
