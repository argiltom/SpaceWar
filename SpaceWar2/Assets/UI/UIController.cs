using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NewGame()
    {
        SceneManager.LoadScene("presentationScene");
    }
    public void NewGameVR()
    {
        SceneManager.LoadScene("presentationSceneVR");
    }
    public void LoadGame()
    {

    }
    public void GameTitle()
    {
        SceneManager.LoadScene("GameTitle");
    }
    public void GameTitleVR()
    {
        SceneManager.LoadScene("GameTitleVR");
    }
    public void GameClear()
    {
        SceneManager.LoadScene("GameClear");
    }
    public void GameClearVR()
    {
        SceneManager.LoadScene("GameClearVR");
    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
