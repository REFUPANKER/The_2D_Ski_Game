using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public AudioSource music1;
    public TextMeshProUGUI musicMuterLabel;
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void MuteUnmuteMusic()
    {
        if (music1.isPlaying)
        {
            music1.Stop();
            musicMuterLabel.text = "Play Music";
        }
        else
        {
            music1.Play();
            musicMuterLabel.text = "Mute Music";
        }
    }
}
