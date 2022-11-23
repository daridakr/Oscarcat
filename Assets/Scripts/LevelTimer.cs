using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTimer : MonoBehaviour
{
    [SerializeField]
    private Image leveleComplete;
    [SerializeField]
    private Text feedCount;
    [SerializeField]
    private Text pointsCount;

    Image timeBar;
    [SerializeField]
    Text output;
    public float time = 30f;
    float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timeBar = GetComponent<Image>();
        timeLeft = time;  
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timeBar.fillAmount = timeLeft / time;
            output.text = $"Оскар проснётся через {timeLeft}";
        }
        else
        {
            var ev = Simulation.Schedule<PlayerEnteredVictoryZone>();
            leveleComplete.gameObject.SetActive(true);
            feedCount.text = PointsController.collectedFeed.ToString();
            pointsCount.text = "0";
            Button nextButton = leveleComplete.GetComponentInChildren<Button>();
            nextButton.onClick.AddListener(() =>
            {
                PlayerPrefs.SetInt("FeedCount", PlayerController.Instance.CountOfFeed);
                PlayerPrefs.SetInt("ObediencePoints", PlayerController.Instance.CountOfObediencePoints);
                //SceneManager.LoadScene("LevelMap");
                //LevelManager.Instance.SceneIndex = 6;
                LevelManager.Instance.isEndChapter();
            });
        }
    }
}
