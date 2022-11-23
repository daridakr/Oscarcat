using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player has died.
    /// </summary>
    /// <typeparam name="PlayerDeath"></typeparam>
    public class PlayerDeath : Simulation.Event<PlayerDeath>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            if (player.health.IsAlive)
            {
                if (player.HealthBar.CurrentValue <= 0)
                {
                    player.health.Die();
                    if (player.CountOfLives != 0)
                    {
                        Image currentHealthImage = player.HealthImages[player.CountOfLives - 1];
                        Object.Destroy(currentHealthImage);
                        if (player.CountOfLives == 1)
                        {
                            SceneManager.LoadScene("LevelMap");
                        }
                        player.CountOfLives--;
                    }
                    model.virtualCamera.m_Follow = null;
                    model.virtualCamera.m_LookAt = null;
                    // player.collider.enabled = false;
                    player.controlEnabled = false;

                    if (player.audioSource && player.ouchAudio)
                        player.audioSource.PlayOneShot(player.ouchAudio);
                    player.animator.SetTrigger("hurt");
                    player.animator.SetBool("dead", true);
                    Simulation.Schedule<PlayerSpawn>(2);
                }
                else
                {
                    if (player.audioSource && player.ouchAudio)
                        player.audioSource.PlayOneShot(player.ouchAudio);
                    player.animator.SetTrigger("hurt");
                    player.HealthBar.CurrentValue -= 20;
                }
            }
        }
    }
}