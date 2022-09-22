using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    GameObject pauseButton;
    GameObject pausePanel;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "panel")
                pausePanel = child.gameObject;
            else if (child.name == "pauseButton")
                pauseButton = child.gameObject;
        }
        pausePanel.SetActive(false);
    }

    public void Pause()
    {
        GameManager.instance.PauseGame();
        pauseButton.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void Resume()
    {
        GameManager.instance.ResumeGame();
        pauseButton.SetActive(true);
        pausePanel.SetActive(false);
    }


}
