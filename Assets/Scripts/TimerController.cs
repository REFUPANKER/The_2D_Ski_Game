using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public PlayerMovement player;
    public TextMeshProUGUI label;
    public int time = 0;
    void Start()
    {
        StartCoroutine(timerCounter());
    }

    public void ResetTimer()
    {
        Stop();
        time = 0;
        label.text = GetAsUiTimeFormat();
        StartCoroutine(timerCounter());
    }
    IEnumerator timerCounter()
    {
        if (player.CanMove)
        {
            time += 1;
            label.text = GetAsUiTimeFormat();
        }
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(timerCounter());
    }
    public void StartTimer()
    {
        StartCoroutine(timerCounter());
    }
    public void Stop()
    {
        StopAllCoroutines();
    }
    public string GetAsUiTimeFormat()
    {
        return FillZero((time / (60 * 60)) % 60) + ":" + FillZero((time / (60)) % 60) + ":" + FillZero(time % 100);
    }
    private string FillZero(int num)
    {
        return num < 10 ? "0" + num : num + "";
    }
}
