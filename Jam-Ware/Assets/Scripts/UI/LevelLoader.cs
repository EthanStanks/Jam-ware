using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen; // loading screen canvas
    public Slider loadingBar; // the slider bar being used for the loading bar
    public TextMeshProUGUI loadingText; // the text mesh pro text used for displaying load %
    public GameObject pressAnyKey; // text used to tell user to continue
    public void LoadLevel(int index)
    {
        StartCoroutine(LoadAsynchronously(index)); // tells unity to load the new scene
    }

    IEnumerator LoadAsynchronously(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index); // starts to load new scene
        operation.allowSceneActivation = false; // don't want it moving onto the new scene just yet
        loadingScreen.SetActive(true); // shows the loading screen canvas
        pressAnyKey.SetActive(false); // makes sure that the continue text isn't showing
        while (!operation.isDone) // while it loads the new scene
        {
            float progress = Mathf.Clamp01(operation.progress / .9f); // gets the current load %
            loadingBar.value = progress; // changes the loading screen fill % to the current load %
            loadingText.text = progress * 100f + "%"; // sets the display % to the current load %

            if (progress == 1) // if the scene is loaded
                pressAnyKey.SetActive(true); // show the continue text

            if (Input.anyKeyDown) // if they press any key
                operation.allowSceneActivation = true; // tells unity that we can move onto the next scene
            yield return null;
        }
    }
}
