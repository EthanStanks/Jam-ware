using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IconHover : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image desktopIcon;
    private Color transparentIconBackground;
    private Color visableIconBackground;
    [SerializeField] LevelLoader loader;
    [SerializeField] int loadSceneInt;
    private void Start()
    {
        transparentIconBackground = new Color(desktopIcon.color.r, desktopIcon.color.g, desktopIcon.color.b, 0f);
        visableIconBackground = new Color(desktopIcon.color.r, desktopIcon.color.g, desktopIcon.color.b, 0.38f);
    }
    public void Hovering() // when you hover over the button
    {
        desktopIcon.color = visableIconBackground;
    }

    public void Idle() // when you unhover the button
    {
        desktopIcon.color = transparentIconBackground;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;
        if (clickCount == 2)
        {
            loader.LoadLevel(loadSceneInt);
            Debug.Log("Double Clicked");
        }
    }
}
