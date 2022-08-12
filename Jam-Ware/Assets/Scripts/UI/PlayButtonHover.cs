using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class PlayButtonHover : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI myText;

    public void Hovering() // when you hover over the button
    {
        myText.text = "Install\nVirus";
    }

    public void Idle() // when you unhover the button
    {
        myText.text = "Install\nAnti-Virus";
    }
}