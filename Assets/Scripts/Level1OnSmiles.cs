using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1OnSmiles : MonoBehaviour
{
    public void GetIndex()
    {
        FindObjectOfType<Level1>().GetIndexOfBtn(transform.GetSiblingIndex());
    }

}
