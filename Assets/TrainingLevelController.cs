using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainingLevelController : MonoBehaviour
{
    [SerializeField]
    private Image completeOne;
    [SerializeField]
    private Image completeTwo;
    [SerializeField]
    private Image completeThree;

    [SerializeField]
    private EnemyController enemy;

    PlatformerModel model = Simulation.GetModel<PlatformerModel>();

    void OnTriggerEnter2D(Collider2D collider)
    {
        var p = collider.gameObject.GetComponent<PlayerController>();
        if (p != null)
        {
            completeOne.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        if (enemy._collider.enabled == false && completeTwo.gameObject.active == false)
        {
            enemy._collider.enabled = true;
            completeTwo.gameObject.SetActive(true);
        }
        if (model.player.CountOfFeed == 6 && completeThree.gameObject.active == false)
        {
            model.player.CountOfFeed = 0;
            completeThree.gameObject.SetActive(true);
        }
    }
}
