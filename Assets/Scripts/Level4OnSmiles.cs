using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4OnSmiles : MonoBehaviour
{
    public void GetIndex()
    {
        FindObjectOfType<Level4>().GetIndexOfBtn(transform.GetSiblingIndex());
    }

}
