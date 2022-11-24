using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsController : MonoBehaviour
{
    public LevelButton levelButton;
    public LevelButton levelButton2;
    public LevelButton levelButton3;
    public LevelButton levelButton4;
    public LevelButton levelButton5;

    int leveleComplete;

    private void Start()
    {
        leveleComplete = PlayerPrefs.GetInt("LevelComplete");

        //Debug.Log("LeveleComplete" + leveleComplete);

        switch(leveleComplete)
        {
            case 1:
                levelButton.IsCurrent = false;
                levelButton.IsDone = true;
                levelButton2.IsCurrent = true;

                levelButton5.IsDone = false;
                break;
            case 2:
                levelButton.IsCurrent = false;
                levelButton.IsDone = true;
                levelButton2.IsCurrent = false;
                levelButton2.IsDone = true;
                levelButton3.IsCurrent = true;
                break;
            case 3:
                levelButton.IsCurrent = false;
                levelButton.IsDone = true;
                levelButton2.IsDone = true;
                levelButton3.IsCurrent = false;
                levelButton3.IsDone = true;
                levelButton4.IsCurrent = true;
                break;
            case 4:
                levelButton.IsCurrent = false;
                levelButton.IsDone = true;
                levelButton3.IsCurrent = false;
                levelButton3.IsDone = true;
                levelButton2.IsCurrent = false;
                levelButton2.IsDone = true;
                levelButton4.IsCurrent = false;
                levelButton4.IsDone = true;
                levelButton5.IsCurrent = true;
                break;
            case 5:
                levelButton.IsCurrent = false;
                levelButton.IsDone = true;
                levelButton3.IsCurrent = false;
                levelButton3.IsDone = true;
                levelButton2.IsCurrent = false;
                levelButton2.IsDone = true;
                levelButton4.IsCurrent = false;
                levelButton4.IsDone = true;
                levelButton5.IsCurrent = false;
                levelButton5.IsDone = true;
                break;
        }

        if (levelButton5.IsDone)
        {
            if (PlayerPrefs.GetInt("FirstChapter") == 0)
            {
                SceneManager.LoadScene("TheEndFirstChapter");
                PlayerPrefs.SetInt("FirstChapter", 1);
            }
        }
    }

    private void Update()
    {
        
    }

    public void LoadLevel(LevelButton levelButton)
    {
        if (levelButton.IsCurrent)
        {
            SceneManager.LoadScene(levelButton.LevelScene); 
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadTrainingLevel()
    {
        SceneManager.LoadScene("TrainingLevel");
    }

    public void Reset()
    {
        levelButton.IsDone = false;
        levelButton.IsCurrent = true;

        levelButton2.IsDone = false;
        levelButton2.IsCurrent = false;
        levelButton3.IsDone = false;
        levelButton3.IsCurrent = false;
        levelButton4.IsDone = false;
        levelButton4.IsCurrent = false;
        levelButton5.IsDone = false;
        levelButton5.IsCurrent = false;
        PlayerPrefs.DeleteAll();
    }
}
