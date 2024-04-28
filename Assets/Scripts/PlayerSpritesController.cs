using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerSpritesController : MonoBehaviour
{
    [Header("Player Sprites")]
    public Sprite pNormal;
    public Sprite pJumping;

    [Description("for Scene manager")]
    [Header("Board Sprites")]
    public Sprite bNormal;

    [Header("InGame Objects")]
    public SpriteRenderer pBoard;
    public SpriteRenderer pBody;

    [Header("Head Checker Positions")]
    public PlayerHeadController headChecker;
    public Vector3 Normal_HC;
    public Vector3 Jumping_HC;

    void Start()
    {
        pBoard.sprite = bNormal;
        pBody.sprite = pNormal;
    }

    public void SwitchPlayerToJump()
    {
        if (pBody.sprite != pJumping)
        {
            pBody.sprite = pJumping;
            headChecker.transform.localPosition = Jumping_HC;
        }
    }

    public void SwitchPlayerToNormal()
    {
        if (pBody.sprite != pNormal)
        {
            pBody.sprite = pNormal;
            headChecker.transform.localPosition = Normal_HC;
        }
    }
}
