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

    [SerializeField] GameObject objPlayCracks;
    [SerializeField] GameObject objOptionCracks;
    [SerializeField] GameObject objCreditCracks;
    [SerializeField] GameObject objExitCracks;

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
        if (button == 0) objPlayDarkness.SetActive(true);
        else if (button == 1) objOptionDarkness.SetActive(true);
        else if (button == 2) objCreditDarkness.SetActive(true);
        else if (button == 3) objExitDarkness.SetActive(true);
    }
    public void ButtonClick(int button)
    {
        if (button == 0) PlayButtonCracked();
        else if (button == 1) OptionButtonCracked();
        else if (button == 2) CreditButtonCracked();
        else if (button == 3) ExitButtonCracked();
    }

    void PlayButtonCracked()
    {
        objPlayCracks.SetActive(true);
        objOptionCracks.SetActive(false);
        objCreditCracks.SetActive(false);
        objExitCracks.SetActive(false);
    }
    void OptionButtonCracked()
    {
        objPlayCracks.SetActive(false);
        objOptionCracks.SetActive(true);
        objCreditCracks.SetActive(false);
        objExitCracks.SetActive(false);
    }
    void CreditButtonCracked()
    {
        objPlayCracks.SetActive(false);
        objOptionCracks.SetActive(false);
        objCreditCracks.SetActive(true);
        objExitCracks.SetActive(false);
    }
    void ExitButtonCracked()
    {
        objPlayCracks.SetActive(false);
        objOptionCracks.SetActive(false);
        objCreditCracks.SetActive(false);
        objExitCracks.SetActive(true);
    }
}
