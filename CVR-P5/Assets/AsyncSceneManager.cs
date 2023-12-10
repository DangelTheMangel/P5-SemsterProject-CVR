using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class AsyncSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject loadingScreenObject;  //loading screen GameObject that is enablede when laodnign screen apper.

    [SerializeField]
    private Slider loadingProgressBar;  // loading progress bar as a Slider.

    /// <summary>
    /// Start the mini-game loading process
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void StartMiniGameLoading(int sceneIndex)
    {
        // Activate the loading screen
        loadingScreenObject.SetActive(true);

        // Start the asynchronous loading operation
        StartCoroutine(LoadGameLevel(sceneIndex));
    }

    /// <summary>
    /// Coroutine to asynchronously load the game level
    /// </summary>
    /// <param name="sceneIndex"></param>
    /// <returns></returns>
    private IEnumerator LoadGameLevel(int sceneIndex)
    {
        // Start the asynchronous loading operation
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex);

        // Continue updating the loading progress until the operation is complete
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingProgressBar.value = progressValue;
            yield return null;
        }
    }

}
