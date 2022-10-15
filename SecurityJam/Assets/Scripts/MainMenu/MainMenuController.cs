using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] float fltAnimationStartDelay;
    [SerializeField] GameObject objDarkness;
    [SerializeField] GameObject objPlayLight;
    [SerializeField] GameObject objOptionLight;
    [SerializeField] GameObject objCreditLight;
    [SerializeField] GameObject objExitLight;

    [SerializeField] GameObject objPlayCracks;
    [SerializeField] GameObject objOptionCracks;
    [SerializeField] GameObject objCreditCracks;
    [SerializeField] GameObject objExitCracks;

    [SerializeField] GameObject objEmerge;
    [SerializeField] GameObject objTower;

    [SerializeField] Animator animatorWatchTower;
    [SerializeField] Animator animatorHatch;
    [SerializeField] Animator animatorEmerge;
    [SerializeField] Animator animatorGarage;

    [SerializeField] Animator animatorOfficeLights;
    float fltFlickRate = 0.5f;
    float fltNextFlick;

    private void Start()
    {
        objEmerge.SetActive(false);
        objTower.SetActive(false);
        StartSeeingEyeAnimation();
    }
    private void Update()
    {
        FlickerLights();
    }
    public void ButtonUnHover()
    {
        objDarkness.SetActive(true);
        objPlayLight.SetActive(false);
        objOptionLight.SetActive(false);
        objCreditLight.SetActive(false);
        objExitLight.SetActive(false);
    }
    public void ButtonHover(int button)
    {
        objDarkness.SetActive(false);
        if (button == 0) objPlayLight.SetActive(true);
        else if (button == 1) objOptionLight.SetActive(true);
        else if (button == 2) objCreditLight.SetActive(true);
        else if (button == 3) objExitLight.SetActive(true);
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

    void FlickerLights()
    {
        if (Time.time > fltNextFlick)
        {
            fltNextFlick = Time.time + fltFlickRate;
            int rng = (int)Random.Range(0, 3);
            RandomLight(rng);
        }
    }
    void RandomLight(float rng)
    {
        if (rng <= 0.99f)
        {
            animatorOfficeLights.SetBool("isDark", true);
            animatorOfficeLights.SetBool("isLeft", false);
            animatorOfficeLights.SetBool("isRight", false);
        }
        else if (rng <= 1.99f)
        {
            animatorOfficeLights.SetBool("isDark", false);
            animatorOfficeLights.SetBool("isLeft", true);
            animatorOfficeLights.SetBool("isRight", false);
        }
        else
        {
            animatorOfficeLights.SetBool("isDark", false);
            animatorOfficeLights.SetBool("isLeft", false);
            animatorOfficeLights.SetBool("isRight", true);
        }
    }
    // ATTENTION ANIMATION STUFF -> do not change or delete please or else break and ethan sad
    #region Start Tower Animation
    void WatchForPreditors(bool isWatching)
    {
        animatorWatchTower.SetBool("isWatching", isWatching);
    }
    IEnumerator StartWatchTowerTimer()
    {
        float waitForSec = fltAnimationStartDelay;
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
            if (timer > 1f)
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
                StartCoroutine(StartGarageDoor());
            }
            yield return null;
        }
    }
    #endregion

    #region Garage Door Animation
    IEnumerator StartGarageDoor()
    {
        float waitForSec = 1;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                animatorGarage.SetBool("isClosed", false);
                animatorGarage.SetBool("isOpen", true);
                StartCoroutine(GarageDoorOpened());
            }
            yield return null;
        }
    }
    IEnumerator GarageDoorOpened()
    {
        float waitForSec = 0.8f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                animatorGarage.SetBool("isOpen", false);
                animatorGarage.SetBool("isOpened", true);
            }
            yield return null;
        }
    }
    IEnumerator CloseGarageDoor()
    {
        float waitForSec = 0.6f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                animatorGarage.SetBool("isOpened", false);
                animatorGarage.SetBool("isClose", true);
                StartCoroutine(GarageDoorClosed());
            }
            yield return null;
        }
    }
    IEnumerator GarageDoorClosed()
    {
        float waitForSec = 0.8f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                animatorGarage.SetBool("isClose", false);
                animatorGarage.SetBool("isClosed", true);
            }
            yield return null;
        }
    }
    #endregion
}
