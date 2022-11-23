using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    [SerializeField]
    private Image leveleComplete;
    [SerializeField]
    private Text feedCount;
    [SerializeField]
    private Text pointsCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject gameObject = GameObject.Find("Abilities");
        //Button button = gameObject.GetComponentInChildren<Button>();
        int countOfMeow = 0;
        countOfMeow++;
        if (countOfMeow == 3)
        {
            PlayerController.Instance.CountOfObediencePoints += 3;
            leveleComplete.gameObject.SetActive(true);
            feedCount.text = PlayerController.Instance.CountOfFeed.ToString();
            pointsCount.text = PlayerController.Instance.CountOfObediencePoints.ToString();
            Button nextButton = leveleComplete.GetComponentInChildren<Button>();
            nextButton.onClick.AddListener(() =>
            {
                Destroy(GameObject.Find("Дверь@3x"));
                Destroy(GameObject.Find("DoorCircle"));
                PlayerPrefs.SetInt("FeedCount", PlayerController.Instance.CountOfFeed);
                PlayerPrefs.SetInt("ObediencePoints", PlayerController.Instance.CountOfObediencePoints);
                SceneManager.LoadScene("SceneOne");
                //LevelManager.Instance.isEndChapter();
            });
        }
        //button.onClick.AddListener(() =>
        //{
            
        //}
        //);
    }
}
