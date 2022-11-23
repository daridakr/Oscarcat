using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBonus : MonoBehaviour
{
    [SerializeField]
    private int jumpForce;

    void OnTriggerEnter2D(Collider2D collider)
    {
        var p = collider.gameObject.GetComponent<PlayerController>();
        if (p != null) { p.jumpTakeOffSpeed = jumpForce; Destroy(this.gameObject); }
    }
}
