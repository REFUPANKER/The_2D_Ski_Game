using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBorderController : MonoBehaviour
{
    public PlayerMovement player;
    public HearthsController hearthsController;
    void OnCollisionEnter2D(Collision2D collision)
    {
        hearthsController.RemoveHeart();
        if (player.CanMove)
        {
            player.Respawn();
        }
    }
}
