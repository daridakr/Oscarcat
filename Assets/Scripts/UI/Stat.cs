using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour
{
    /// <summary>
    /// The actual image that chaing the fill of
    /// </summary>
    private Image content;

    /// <summary>
    /// Hold the current fill value
    /// </summary>
    private float currentFill;

    /// <summary>
    /// The lerp speed
    /// </summary>
    [SerializeField]
    private float lerpSpeed;

    /// <summary>
    /// Tha max value for example max health or mana
    /// </summary>
    public float MaxValue { get; set; }

    /// <summary>
    /// The current value for example the current health or mana
    /// </summary>
    private float currentValue;

    /// <summary>
    /// Property for setting the current value
    /// </summary>
    public float CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            if (value > MaxValue) currentValue = MaxValue;
            else if (value < 0) currentValue = 0;
            else currentValue = value;
            currentFill = currentValue / MaxValue;
        }
    }

    void Start()
    {
        content = GetComponent<Image>();
    }

    void Update()
    {
        // update the bar
        HandleBar();
    }

    /// <summary>
    /// Makes sure that the bar updates 
    /// </summary>
    private void HandleBar()
    {
        // if we have a new fill amount then we know that we need to update 
        if (currentFill != content.fillAmount)
            // lerps the fill amount so that we get a smooth movement
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
    }

    public void Initialize(float currentValue, float maxValue)
    {
        if (content == null) content = GetComponent<Image>();
        MaxValue = maxValue;
        CurrentValue = currentValue;
        // for instant update health in npc frame on select new target
        content.fillAmount = CurrentValue / MaxValue;
    }
}
