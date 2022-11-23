using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour
{
    //public Text FeedCountText;
    //public Text ObediencePointsCountText;

    public static int collectedFeed;
    public static int collectedPoints;

    private void Awake()
    {
        PlayerController.Instance.CountOfFeed = PlayerPrefs.GetInt("FeedCount");
        PlayerController.Instance.CountOfObediencePoints = PlayerPrefs.GetInt("ObediencePoints");
    }
}
