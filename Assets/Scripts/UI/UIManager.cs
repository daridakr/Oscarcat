using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<UIManager>();
            return instance;
        }
    }

    [SerializeField]
    private Text feed;

    [SerializeField]
    private Text obedience;

    [SerializeField]
    private GameObject dialog;

    [SerializeField]
    private GameObject[] dialogsToHide;

    private void Update()
    {
        feed.text = PlayerController.Instance.CountOfFeed.ToString();
        obedience.text = PlayerController.Instance.CountOfObediencePoints.ToString();
    }

    public void SetDialogActive()
    {
        for (int i = 0; i < dialogsToHide.Length; i++) dialogsToHide[i].SetActive(false);
        dialog.SetActive(true);
    }
}
