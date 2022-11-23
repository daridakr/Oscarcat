using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetJumpForce : MonoBehaviour
{
    [SerializeField]
    private int jumpForce;

    void OnTriggerEnter2D(Collider2D collider)
    {
        var p = collider.gameObject.GetComponent<PlayerController>();
        if (p != null) p.Bounce(jumpForce);
    }
}
