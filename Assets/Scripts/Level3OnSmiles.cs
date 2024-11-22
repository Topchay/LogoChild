using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level3OnSmiles : MonoBehaviour
{
    private Level3 level3;

    private Image imageComponent;
    private Sprite smile;

    private void Awake()
    {
        level3 = FindObjectOfType<Level3>();
        imageComponent = GetComponent<Image>();
    }

    private void Start()
    {
        StartCoroutine(SwitchSpriteCoroutine());
    }

    private IEnumerator SwitchSpriteCoroutine()
    {
        while (true) // ����������� ���� ����� ���� �������
        {
            if (level3 != null && level3.isGameActive)
            {
                // ��������� ��������� ������ �� 1 �� 2
                float timer = Random.Range(1f, 2f);
                yield return new WaitForSeconds(timer);

                if (level3.smilesSprite != null && level3.smilesSprite.Length > 0)
                {
                    // �������� �������� ������ �� �������
                    int randomIndex = Random.Range(0, level3.smilesSprite.Length);
                    imageComponent.sprite = level3.smilesSprite[randomIndex];

                    Color currentColor = imageComponent.color;
                    currentColor.a = 1f;
                    imageComponent.color = currentColor;

                    // ��������� ��������� ������ �� 2 �� 3 ��� ������� �������
                    float hideTimer = Random.Range(2f, 3f);
                    yield return new WaitForSeconds(hideTimer);

                    // ������� ������ (������������� ��� � ������)
                    imageComponent.sprite = null;

                    currentColor.a = 0f;
                    imageComponent.color = currentColor;
                }
                else
                {
                    Debug.LogWarning("Smiles sprite array is empty or null.");
                }
            }
            else
            {             
                yield return null; // ����� �� ���� ����
            }
        }
    }

    private void OnMouseDown()
    {
        if (imageComponent.sprite != null)
        {
            smile = imageComponent.sprite;
            level3.CheckClicked(smile);

            imageComponent.sprite = null;
            Color currentColor = imageComponent.color;
            currentColor.a = 0f;
            imageComponent.color = currentColor;
        }
    }
}