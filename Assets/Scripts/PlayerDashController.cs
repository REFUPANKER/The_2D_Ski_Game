using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDashController : MonoBehaviour
{
    public bool CanDash = true;
    public float dashPower = 40f;
    public float DashCooldown = 3;

    public Slider DashCooldownSlider;
    void Start()
    {
        DashCooldownSlider.maxValue = DashCooldown;
        DashCooldownSlider.value = DashCooldownSlider.maxValue;
    }
    public void Dash(Rigidbody2D rb, Transform player)
    {
        if (CanDash)
        {
            StartCoroutine("UseDash");
            CanDash = false;
            for (int i = 0; i < 100; i++)
            {
                rb.AddForce(player.right * dashPower);
            }
            StartCoroutine("CountDown");
        }
    }
    IEnumerator UseDash()
    {
        for (float i = DashCooldown; i >= 0; i -= 0.1f)
        {
            DashCooldownSlider.value = i;
            yield return new WaitForSeconds(0.1f);
        }

    }
    IEnumerator CountDown()
    {
        for (float i = 0; i < DashCooldown; i += 0.1f)
        {
            DashCooldownSlider.value = i;
            yield return new WaitForSeconds(0.1f);
        }
        CanDash = true;
    }
}
