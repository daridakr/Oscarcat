using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerSpawn.playerPosition = collision.transform.position;
        }
    }
}
