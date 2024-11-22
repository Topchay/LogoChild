using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnLevelButtons : MonoBehaviour
{
    private UIManager UIManager;

    private void Awake()
    {
        UIManager = FindObjectOfType<UIManager>();
    }

    public void SetLevel()
    {
        UIManager.CanStartButtonPress(transform.GetSiblingIndex());
    }
}
