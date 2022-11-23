using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        [SerializeField]
        private Image leveleComplete;
        [SerializeField]
        private Text feedCount;
        [SerializeField]
        private Text pointsCount;

        void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            if (p != null)
            {
                var ev = Schedule<PlayerEnteredVictoryZone>();
                leveleComplete.gameObject.SetActive(true);
                feedCount.text = PointsController.collectedFeed.ToString();
                pointsCount.text = "0";
                Button nextButton = leveleComplete.GetComponentInChildren<Button>();
                ev.victoryZone = this;
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
}