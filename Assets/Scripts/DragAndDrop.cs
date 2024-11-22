using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    Level8 Level8;

    private Vector3 startPos;
    private GameObject hitObject;  // Пустой объект для записи столкновения

    private void Awake()
    {
        Level8 = FindObjectOfType<Level8>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        hitObject = collision.collider.gameObject;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 screenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPos = transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (hitObject != null) transform.position = hitObject.transform.position;
        else transform.position = startPos;

        CheckResult(hitObject);
    }

    public void CheckResult(GameObject hitObject)
    {
        if (hitObject != null && Level8.currentStage < Level8.levelBar.transform.childCount)
        {
            Image currentImageComponent = Level8.levelBar.transform.GetChild(Level8.currentStage).GetComponent<Image>();

            if (gameObject.CompareTag(hitObject.tag))
            {
                print("Правильно!");
                currentImageComponent.sprite = Level8.trueFalse[1];
            }
            else
            {
                print("Не правильно :(");
                currentImageComponent.sprite = Level8.trueFalse[0];
            }
        }
        Level8.currentStage++;
        Level8.SpawnLittleMans();
        Level8.WinGame();
    }
}
