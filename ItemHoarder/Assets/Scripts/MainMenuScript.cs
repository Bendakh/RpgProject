using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{


    public void StartGameButton()
    {
        GameManager._instance.isLoaded = false;
        SceneManager.LoadScene(1);
    }

    public void LoadGame()
    {
        GameManager._instance.isLoaded = true;
        GameManager._instance.mockData = SaveLoadManager.LoadPlayerData();
        SceneManager.LoadScene(1);
    }
}
