using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject objDarkness;
    [SerializeField] GameObject objPlayDarkness;
    [SerializeField] GameObject objOptionDarkness;
    [SerializeField] GameObject objCreditDarkness;
    [SerializeField] GameObject objExitDarkness;

    [SerializeField] GameObject objPlayButton;
    [SerializeField] GameObject objOptionButton;
    [SerializeField] GameObject objCreditButton;
    [SerializeField] GameObject objExitButton;

    public void ButtonUnHover()
    {
        objDarkness.SetActive(true);
        objPlayDarkness.SetActive(false);
        objOptionDarkness.SetActive(false);
        objCreditDarkness.SetActive(false);
        objExitDarkness.SetActive(false);
    }
    public void ButtonHover(int button)
    {
        objDarkness.SetActive(false);
        if(button == 0) objPlayDarkness.SetActive(true);
        else if(button == 1) objOptionDarkness.SetActive(true);
        else if(button == 2) objCreditDarkness.SetActive(true);
        else if(button == 3) objExitDarkness.SetActive(true);
    }
}
