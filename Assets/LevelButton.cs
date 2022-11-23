using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    private static LevelButton instance = null;

    public static LevelButton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(LevelButton)) as LevelButton;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("LevelManager");
                instance = obj.AddComponent(typeof(LevelButton)) as LevelButton;
                Debug.Log("Could not locate an MySingletonClass object. MySingletonClass was Generated Automaticly.");
            }

            return instance;
        }
    }

    void Awake()
    {
        //if (instance == null)
        //{
        //    DontDestroyOnLoad(this);
        //}
    }

    public string LevelScene { get => levelScene; private set => levelScene = value; }
    public bool IsDone { get => isDone; set => isDone = value; }
    public bool IsCurrent { get => isCurrent; set => isCurrent = value; }

    [SerializeField]
    private bool isDone = false;
    [SerializeField]
    private bool isCurrent;
    [SerializeField]
    private string levelScene;
    [SerializeField]
    private Sprite defaultIcon;
    [SerializeField]
    private Sprite currentIcon;
    [SerializeField]
    private Sprite doneIcon;

    private Image icon;


    private void Start()
    {
        //defaultIcon = GetComponent<Sprite>();
        //currentIcon = GetComponent<Sprite>();
        //doneIcon = GetComponent<Sprite>();
        icon = GetComponent<Image>();
        icon.sprite = defaultIcon;
    }

    private void Update()
    {
        if (IsDone) icon.sprite = doneIcon;
        else if (IsCurrent) icon.sprite = currentIcon;
        else icon.sprite = defaultIcon;
    }

}
