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

    [SerializeField] GameObject objEmerge;
    [SerializeField] GameObject objTower;

    [SerializeField] Animator animatorWatchTower;
    [SerializeField] Animator animatorHatch;
    [SerializeField] Animator animatorEmerge;


    private void Start()
    {
        objEmerge.SetActive(false);
        objTower.SetActive(false);
        StartSeeingEyeAnimation();
    }
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
        if (button == 0) ClickedPlayButton();
        else if (button == 1) ClickedOptionButton();
        else if (button == 2) ClickedCreditButton();
        else if (button == 3) ClickedExitButton();
    }
    void ClickedPlayButton()
    {
        PlayButtonCracked();
        KillTheSeeingEyeAnimation();
    }
    void ClickedOptionButton()
    {
        OptionButtonCracked();
    }
    void ClickedCreditButton()
    {
        CreditButtonCracked();
    }
    void ClickedExitButton()
    {
        ExitButtonCracked();
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

    // ATTENTION ANIMATION STUFF -> do not change or delete please or else break and ethan sad
    #region Start Tower Animation
    void WatchForPreditors(bool isWatching)
    {
        animatorWatchTower.SetBool("isWatching", isWatching);
    }
    IEnumerator StartWatchTowerTimer()
    {
        float waitForSec = 10.0f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                OpenHatch();
            }

            yield return null;
        }
    }
    void StartSeeingEyeAnimation()
    {
        animatorHatch.SetBool("isOpen", false);
        animatorHatch.SetBool("isClose", false);
        animatorHatch.SetBool("isClosed", true);
        animatorHatch.SetBool("isOpened", false);
        StartCoroutine(StartWatchTowerTimer());
    }
    IEnumerator StartTowerTimer()
    {
        float waitForSec = 1.4f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                animatorEmerge.SetBool("isEmerge", false);
                animatorEmerge.SetBool("isEnd", true);
                StartCoroutine(WaitToWatch());
            }
            yield return null;
        }
    }
    IEnumerator WaitToWatch()
    {
        float waitForSec = 0.1f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                objEmerge.SetActive(false);
                objTower.SetActive(true);
                WatchForPreditors(true);
            }
            yield return null;
        }
    }
    IEnumerator EmergeTimer()
    {
        float waitForSec = 0.1f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                animatorEmerge.SetBool("isStart", false);
                animatorEmerge.SetBool("isEmerge", true);
                animatorEmerge.SetBool("isEnd", false);
                animatorEmerge.SetBool("isDemerge", false);
                StartCoroutine(StartTowerTimer());
            }
            yield return null;
        }
    }
    IEnumerator StartEmerge()
    {
        float waitForSec = 0.1f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                objEmerge.SetActive(true);
                animatorEmerge.SetBool("isStart", true);
                animatorEmerge.SetBool("isEmerge", false);
                animatorEmerge.SetBool("isEnd", false);
                animatorEmerge.SetBool("isDemerge", false);
                StartCoroutine(EmergeTimer());
            }
            yield return null;
        }
    }
    IEnumerator OpenHatchTimer()
    {
        float waitForSec = 0.5f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if(timer > 1f)
            {
                animatorHatch.SetBool("isOpen", false);
                animatorHatch.SetBool("isOpened", true);
                StartCoroutine(StartEmerge());
            }
            yield return null;
        }
    }
    void OpenHatch()
    {
        animatorHatch.SetBool("isOpen", true);
        animatorHatch.SetBool("isClose", false);
        animatorHatch.SetBool("isClosed", false);
        animatorHatch.SetBool("isOpened", false);
        StartCoroutine(OpenHatchTimer());
    }

    #endregion

    #region End Tower Animation
    void KillTheSeeingEyeAnimation()
    {
        animatorWatchTower.SetBool("isWatching", false);
        StartCoroutine(WaitToDemerge());
    }
    IEnumerator WaitToDemerge()
    {
        float waitForSec = 0.3f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                objTower.SetActive(false);
                objEmerge.SetActive(true);
                animatorEmerge.SetBool("isDemerge", false);
                animatorEmerge.SetBool("isStart", false);
                animatorEmerge.SetBool("isEmerge", false);
                animatorEmerge.SetBool("isEnd", true);
                StartCoroutine(StartDemerge());

            }
            yield return null;
        }
    }
    IEnumerator StartDemerge()
    {
        float waitForSec = 0.1f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                animatorEmerge.SetBool("isDemerge", true);
                animatorEmerge.SetBool("isEnd", false);
                StartCoroutine(StartCloseHatch());
            }
            yield return null;
        }
    }
    IEnumerator StartCloseHatch()
    {
        float waitForSec = 1.6f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                animatorHatch.SetBool("isOpened", true);
                animatorHatch.SetBool("isOpen", false);
                animatorHatch.SetBool("isClose", false);
                animatorHatch.SetBool("isClosed", false);
                objEmerge.SetActive(false);
                animatorHatch.SetBool("isClose", true);
                animatorHatch.SetBool("isOpened", false);
                StartCoroutine(WaitForClosedHatch());
            }
            yield return null;
        }
    }
    IEnumerator WaitForClosedHatch()
    {
        float waitForSec = 0.5f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                animatorHatch.SetBool("isClose", false);
                animatorHatch.SetBool("isClosed", true);
            }
            yield return null;
        }
    }
    #endregion
}
