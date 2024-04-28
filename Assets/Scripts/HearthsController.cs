using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HearthsController : MonoBehaviour
{
    public Transform HearthsHolder;
    public GameObject gameOverScreen;
    public PlayerMovement player;
    public string RestartSceneName = "Map";
    public AudioSource hitSfx;
    public TimerController timer;
    public void restartHearts()
    {
        foreach (Transform item in HearthsHolder)
        {
            item.gameObject.SetActive(true);
            item.localScale = Vector3.one;
        }

    }
    private int getActiveHearts()
    {
        int c = -1;
        for (int i = 0; i < HearthsHolder.transform.childCount; i++)
        {
            GameObject item = HearthsHolder.transform.GetChild(i).gameObject;
            if (item.activeInHierarchy)
            {
                c += 1;
            }
        }
        return c;
    }
    private GameObject getNextActiveHeart()
    {
        for (int i = HearthsHolder.transform.childCount - 1; i >= 0; i -= 1)
        {
            GameObject item = HearthsHolder.transform.GetChild(i).gameObject;
            if (item.activeInHierarchy)
            {
                return item;
            }
        }
        return null;
    }
    public void RemoveHeart()
    {
        if (getActiveHearts() <= 0)
        {
            player.CanMove = false;
            gameOverScreen.SetActive(true);
        }
        else
        {
            hitSfx.Stop();
            hitSfx.Play();
            Animation anim = getNextActiveHeart().GetComponent<Animation>();
            anim.Play();
            StartCoroutine(
            waitToanimate(anim, () =>
            {
                getNextActiveHeart().gameObject.SetActive(false);
            }));
        }
    }
    IEnumerator waitToanimate(Animation anim, Action action)
    {
        while (anim.isPlaying)
        {
            yield return null;
        }
        action.Invoke();
    }

    public void RestartButtonClick()
    {
        gameOverScreen.SetActive(false);
        timer.ResetTimer();
        player.RestartGame();
    }
}
