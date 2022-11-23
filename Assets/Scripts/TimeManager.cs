using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    public float time = 30f;
    float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft > 0) timeLeft -= Time.deltaTime;
        else SceneManager.LoadScene("LevelMap");
    }
}
