using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLineController : MonoBehaviour
{
    public ParticleSystem particles;
    public AudioSource finishSound;
    public GameObject FinishCanvas;
    public PlayerMovement player;

    [Header("timer score - flip count")]
    public TimerController timer;
    public TextMeshProUGUI FinishTimerLabel;
    public TextMeshProUGUI FlipCountLabel;

    [Header("next level creater")]
    public SpriteGroundGenerater MapGenerater;

    void OnTriggerEnter2D(Collider2D collider)
    {
        player.PauseResume(true);
        timer.Stop();
        particles.Play();
        finishSound.Play();
        FinishCanvas.SetActive(true);

        FinishTimerLabel.text = timer.label.text;
        FlipCountLabel.text = player.TotalFlipCount + " Flips";
    }
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void NextLevelButtonClick()
    {
        MapGenerater.ReStart(true);
        timer.ResetTimer();
        player.RestartGame();
        FinishCanvas.SetActive(false);
    }
}
