using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3OnSmiles : MonoBehaviour
{
    public void GetIndex()
    {
        FindObjectOfType<Level3>().GetIndexOfBtn(transform.GetSiblingIndex());
    }

}
