using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{

    [SerializeField] GameObject MainMenuCanvas;
    [SerializeField] GameObject OptionsCanvas;
    [SerializeField] GameObject CreditsCanvas;

    private void Start()
    {
        MainMenuCanvas.SetActive(true);
        OptionsCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
    }

    public void ClickedOptions()
    {
        MainMenuCanvas.SetActive(false);
        OptionsCanvas.SetActive(true);
        CreditsCanvas.SetActive(false);
    }
    public void ClickedCredits()
    {
        MainMenuCanvas.SetActive(false);
        OptionsCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }
    public void ClickedReturn()
    {
        MainMenuCanvas.SetActive(true);
        OptionsCanvas.SetActive(false);
        CreditsCanvas.SetActive(false);
    }
}
