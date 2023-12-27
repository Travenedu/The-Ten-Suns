using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    public static CustomSceneManager S; // singleton

    private void Awake(){
        S = this; // singleton declaration
    }
 
    // Load a scene by name
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // // Load a scene by index
    // // public void LoadScene(int sceneIndex)
    // // {
    // //     SceneManager.LoadScene(sceneIndex);
    // // }

    // // Reload the current scene
    // // public void ReloadScene()
    // // {
    // //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    // // }

    // // Quit the application
    // public void QuitGame()
    // {
    //     #if UNITY_EDITOR
    //         UnityEditor.EditorApplication.isPlaying = false;
    //     #else
    //         Application.Quit();
    //     #endif
    // }
}

