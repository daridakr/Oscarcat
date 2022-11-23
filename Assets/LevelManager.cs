using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    
    public static LevelManager Instance = null;

    int sceneIndex;
    int levelComplete;

   
    public int SceneIndex { get => sceneIndex; set => sceneIndex = value; }

    private void Start()
    {
        if (Instance == null) Instance = this;
        SceneIndex = SceneManager.GetActiveScene().buildIndex; //текущая сцена
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
    }

    public void isEndChapter()
    {
        if (levelComplete < SceneIndex) PlayerPrefs.SetInt("LevelComplete", SceneIndex);
        LoadMainMenu();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneIndex + 1);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadScene("LevelMap");
    }
}
