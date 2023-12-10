using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartMenu : MonoBehaviour
{
    [SerializeField]
    AsyncSceneManager async;
    public void starExperinces() {
        async.StartMiniGameLoading(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
