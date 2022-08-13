using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconDrag : MonoBehaviour
{
    [SerializeField] private Canvas canvasUI;

    public void DragIcon(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        Vector2 iconPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvasUI.transform, pointerData.position, canvasUI.worldCamera, out iconPosition);
        if (iconPosition.x < -890)
            iconPosition.x = -890;
        else if (iconPosition.x > 890)
            iconPosition.x = 890;

        if (iconPosition.y < -402)
            iconPosition.y = -402;
        else if (iconPosition.y > 462)
            iconPosition.y = 462;
        transform.position = canvasUI.transform.TransformPoint(iconPosition);
    }
}
