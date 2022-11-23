using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel2 : MonoBehaviour
{

    [SerializeField]
    private Image leveleComplete;
    [SerializeField]
    private Text feedCount;
    [SerializeField]
    private Text pointsCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            leveleComplete.gameObject.SetActive(true);
            feedCount.text = PlayerController.Instance.CountOfFeed.ToString();
            pointsCount.text = PlayerController.Instance.CountOfObediencePoints.ToString();
            Button nextButton = leveleComplete.GetComponentInChildren<Button>();
            nextButton.onClick.AddListener(() =>
            {
                PlayerPrefs.SetInt("FeedCount", PlayerController.Instance.CountOfFeed);
                PlayerPrefs.SetInt("ObediencePoints", PlayerController.Instance.CountOfObediencePoints);
                SceneManager.LoadScene("SceneOne");
                //LevelManager.Instance.SceneIndex = 4;
                //LevelManager.Instance.isEndChapter();
            });
        }
    }
}
