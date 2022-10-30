using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Header("Floats")]
    [SerializeField] float fltAnimationStartDelay;
    [Space(20)]
    bool isPlayAllowed;
    [Header("Buttons")]
    [SerializeField] GameObject buttonPlay;
    [SerializeField] GameObject buttonOptions;
    [SerializeField] GameObject buttonCredits;
    [SerializeField] GameObject buttonExit;
    [SerializeField] GameObject buttonLevelOne;
    [SerializeField] GameObject buttonLevelTwo;
    [SerializeField] GameObject buttonLevelThree;
    [SerializeField] GameObject buttonLevelFour;
    [SerializeField] GameObject buttonLeaveLevelSelect;
    [Space(20)]
    [Header("Game Objects")]
    [SerializeField] GameObject objDarkness;
    [SerializeField] GameObject objPlayLight;
    [SerializeField] GameObject objOptionLight;
    [SerializeField] GameObject objCreditLight;
    [SerializeField] GameObject objExitLight;
    [Space(10)]
    [SerializeField] GameObject objPlayCracks;
    [SerializeField] GameObject objOptionCracks;
    [SerializeField] GameObject objCreditCracks;
    [SerializeField] GameObject objExitCracks;
    [Space(10)]
    [SerializeField] GameObject objEmerge;
    [SerializeField] GameObject objTower;
    [Space(10)]
    [SerializeField] Camera camMain;
    [Space(20)]
    [Header("Animators")]
    [SerializeField] Animator animatorWatchTower;
    [SerializeField] Animator animatorHatch;
    [SerializeField] Animator animatorEmerge;
    [SerializeField] Animator animatorGarage;
    [Space(10)]
    [SerializeField] Animator animatorLeftOfficeLight;
    [SerializeField] Animator animatorRightOfficeLight;
    float fltFlickRate = 2.25f;
    float fltNextFlick;
    [Space(10)]
    [SerializeField] Animator animatorCar;
    [SerializeField] Animator animatorCamera;



    private void Start()
    {
        ButtonVisability(true);
        animatorCamera.SetBool("isMainMenu", true);
        animatorCamera.SetBool("isFollowCar", false);
        animatorCamera.SetBool("isLevelSelect", false);
        camMain.orthographicSize = 1.310894f;
        isPlayAllowed = false;
        objEmerge.SetActive(false);
        objTower.SetActive(false);
        StartSeeingEyeAnimation();
    }
    private void Update()
    {
        FlickerLights();
    }

    #region Button Stuff
    void ButtonVisability(bool isShown)
    {
        buttonPlay.SetActive(isShown);
        buttonOptions.SetActive(isShown);
        buttonCredits.SetActive(isShown);
        buttonExit.SetActive(isShown);

        buttonLevelOne.SetActive(!isShown);
        buttonLevelTwo.SetActive(!isShown);
        buttonLevelThree.SetActive(!isShown);
        buttonLevelFour.SetActive(!isShown);
        buttonLeaveLevelSelect.SetActive(!isShown);
    }
    public void LeaveLevelSelect()
    {
        ButtonVisability(true);
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
        if (isPlayAllowed)
        {
            PlayButtonCracked();
            KillTheSeeingEyeAnimation();
            isPlayAllowed = false;
        }
    }
    void ClickedOptionButton()
    {
        if (isPlayAllowed)
        {
            OptionButtonCracked();
            isPlayAllowed = false;
        }
    }
    void ClickedCreditButton()
    {
        if (isPlayAllowed)
        {
            CreditButtonCracked();
            isPlayAllowed = false;
        }
    }
    void ClickedExitButton()
    {
        if (isPlayAllowed)
        {
            ExitButtonCracked();
            isPlayAllowed = false;
        }
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

    
    #endregion

    #region Light Flickering For Office Building
    void FlickerLights()
    {
        if (Time.time > fltNextFlick)
        {
            fltNextFlick = Time.time + fltFlickRate;
            RandomFlicker();
        }
    }
    void RandomFlicker()
    {
        float rng = Random.Range(0, 3);
        if (rng <= 1.50f)
        {
            TurnOnOffOfficeLights(true, 0);
            StartCoroutine(StopFlicker());
        }
    }
    void TurnOnOffOfficeLights(bool isOn, int side)
    {
        // if 0 itll turn the left light of the office on or off based off the bool
        // else if 1 itll turn the right light of the office on or off based off the bool
        if (side == 0) animatorLeftOfficeLight.SetBool("isOn", isOn);
        else if (side == 1) animatorRightOfficeLight.SetBool("isOn", isOn);
    }
    IEnumerator StopFlicker()
    {
        float waitForSec = 0.5f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                TurnOnOffOfficeLights(false, 0);
            }
            yield return null;
        }
    }
    #endregion

    // ATTENTION ANIMATION STUFF -> do not change or delete please or else break and ethan sad
    #region Start Tower Animation
    void WatchForPreditors(bool isWatching)
    {
        animatorWatchTower.SetBool("isWatching", isWatching);
        isPlayAllowed = true;
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
                TurnOnOffOfficeLights(true, 1);
                StartCoroutine(StartCar());
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
                TurnOnOffOfficeLights(false, 1);
            }
            yield return null;
        }
    }
    #endregion

    #region Start of Car
    IEnumerator StartCar()
    {
        float waitForSec = 0.2f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                animatorCar.SetBool("isGarage", false);
                animatorCar.SetBool("isDriving", true);
                animatorCamera.SetBool("isFollowCar", true);
                animatorCamera.SetBool("isMainMenu", false);
                StartCoroutine(HaveCarWaitForLevel());
            }
            yield return null;
        }
    }
    IEnumerator HaveCarWaitForLevel()
    {
        float waitForSec = 2;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                animatorCar.SetBool("isDriving", false);
                animatorCar.SetBool("isWaiting", true);
                animatorCamera.SetBool("isFollowCar", false);
                animatorCamera.SetBool("isZoomOut", true);
                StartCoroutine(ZoomOutCameraToLevelSelect());
            }
            yield return null;
        }
    }
    IEnumerator CameraZoomOut()
    {
        float time = 2.0f;
        float steps = 120;

        for (float f = 0; f <= 1; f += time / steps)
        {
            camMain.orthographicSize = Mathf.Lerp(1.310894f, 3.56953f, f);

            yield return new WaitForSeconds(time / steps);
        }
    }
    IEnumerator ZoomOutCameraToLevelSelect()
    {
        float waitForSec = 0.7f;
        float timer = 0.0f;
        while (timer <= 1f)
        {
            timer += Time.deltaTime / waitForSec;
            if (timer > 1f)
            {
                animatorCamera.SetBool("isLevelSelect", true);
                animatorCamera.SetBool("isZoomOut", false);
                //camMain.orthographicSize = 3.56953f;
                StartCoroutine(CameraZoomOut());
                ButtonVisability(false);

            }
            yield return null;
        }
    }
    #endregion
}
