using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement values")]
    public bool CanMove = true;
    public float rotationForce = 250f;
    public float moveSpeed = 20f;
    public Rigidbody2D rb;

    [Header("IsGrounded Checker")]
    public GroundChecker gc;
    float flipTime = 0f;
    public int TotalFlipCount = 0;
    public TextMeshProUGUI flipLabel;

    [Header("Camera movement & fov")]
    public Camera calcFov;

    [Range(6, 15)]
    public int MaxCameraFov = 6;

    [Header("Player head hit controller")]
    public PlayerHeadController headHit;

    [Header("Surfing particles")]
    public ParticleSystem slideParticles;
    public AudioSource skiSfx;

    [Header("Checkpoints - start/end")]
    public Transform startPoint;
    public Transform respawnPoint;

    [Header("Player Controllers")]
    public PlayerDashController dashController;
    public PlayerSpritesController spriteController;
    public HearthsController hearthController;

    [Header("Settings Menu")]
    public GameObject SettingsMenu;
    [Header("Timer")]
    public TimerController timer;

    [Header("panoramaa controller")]
    public List<PanoramaScript> panoramaControllers;
    public void Respawn()
    {
        CanMove = false;
        transform.position = new Vector2(respawnPoint.position.x, respawnPoint.position.y + 2);
        transform.rotation = Quaternion.identity;
        headHit.hit = false;
        PauseResume();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        CanMove = true;
    }
    public void RestartGame()
    {
        TotalFlipCount = 0;
        flipLabel.text = "Flips : " + TotalFlipCount;
        respawnPoint = startPoint;
        hearthController.restartHearts();
        foreach (var item in panoramaControllers)
        {
            item.Reset();
        }
        Respawn();
    }
    void Start()
    {
        Respawn();
    }

    public void PauseResume(bool finished = false)
    {
        if (CanMove || finished)
        {
            skiSfx.Stop();
            CanMove = false;
            preVelocity = rb.velocity;
            rb.velocity = Vector3.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        else
        {
            CanMove = true;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.velocity = preVelocity;
        }
    }
    Vector3 preVelocity;
    public void ShowHideSettingsMenu()
    {
        PauseResume();
        SettingsMenu.SetActive(!SettingsMenu.activeSelf);
    }
    void FixedUpdate()
    {
        if (gc.Grounded)
        {
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(this.transform.right * moveSpeed);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            ShowHideSettingsMenu();
        }
        if (!CanMove)
        {
            return;
        }
        if (headHit.hit)
        {
            hearthController.RemoveHeart();
            if (CanMove)
            {
                Respawn();
            }
            return;
        }
        calcFov.orthographicSize = Mathf.Lerp(calcFov.orthographicSize, Mathf.Clamp(gc.distanceToGround, 5f, MaxCameraFov), Time.deltaTime * 5f);
        if (gc.rotater.collider != null)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up * Time.deltaTime, gc.rotater.normal), 3f * Time.deltaTime);
        }
        if (gc.Grounded)
        {
            spriteController.SwitchPlayerToNormal();
            if (rb.velocity.magnitude <= 10)
            {
                skiSfx.Stop();
                slideParticles.Stop();
            }
            else if (slideParticles.isStopped)
            {
                skiSfx.Play();
                slideParticles.Play();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                dashController.Dash(rb, this.transform);
            }
        }
        else
        {
            spriteController.SwitchPlayerToJump();
            if (!slideParticles.isStopped)
            {
                skiSfx.Stop();
                slideParticles.Stop();
            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.Rotate(Vector3.forward * rotationForce * Time.deltaTime);
                flipTime += 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                transform.Rotate(-Vector3.forward * rotationForce * Time.deltaTime);
                flipTime += 1;
            }
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            flipTime = 0;
        }
        if (flipTime >= 80)
        {
            TotalFlipCount += 1;
            flipTime = 0;
            flipLabel.text = "Flips : " + TotalFlipCount;
        }
    }
}
