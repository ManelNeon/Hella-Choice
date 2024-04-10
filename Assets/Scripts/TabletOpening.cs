using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TabletOpening : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] bool isDown;

    [SerializeField] Animator cameraAnimator;

    [SerializeField] Transform secondPosition;

    public void OnPointerEnter(PointerEventData eventData)
    {

        if (!GameManager.Instance.down && isDown)
        {
            cameraAnimator.Play("GoingDown");

            GameManager.Instance.down = true;
        }
        else if (GameManager.Instance.down && !isDown)
        {
            cameraAnimator.Play("GoingBack");

            GameManager.Instance.down = false;
        }
    }
}
