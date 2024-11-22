using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2OnSmiles : MonoBehaviour
{
    public void GetIndex()
    {
        FindObjectOfType<Level2>().GetIndexOfBtn(transform.GetSiblingIndex());
    }

    public void DestroyBanans()
    {
        FindObjectOfType<Level2>().DestroyBananas();
    }

    public void SetAnimals()
    {
        FindObjectOfType<Level2>().SetAnimals();
    }
}
