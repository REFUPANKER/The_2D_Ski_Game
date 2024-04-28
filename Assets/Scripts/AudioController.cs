using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public List<AudioSource> AudioSrc;
    public Slider AudioSlider;
    void Start()
    {
        if (AudioSlider != null)
        {
            float avg = 0f;
            foreach (var item in AudioSrc)
            {
                avg += item.volume;
            }
            avg /= AudioSrc.Count;
            AudioSlider.value = avg;
            AudioSlider.onValueChanged.AddListener(delegate { ValueChanged(); });
        }
    }
    void ValueChanged()
    {
        foreach (var item in AudioSrc)
        {
            item.volume = AudioSlider.value;
        }
    }
}
