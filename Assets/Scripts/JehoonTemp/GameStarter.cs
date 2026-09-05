using System;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [Tooltip("게임 스타트 패널")]
    [SerializeField] private GameObject introductionPanel;
    [SerializeField] private GameObject howtoUI;
    [SerializeField] private GameObject resumeUI;

    void Awake()
    {
        introductionPanel.SetActive(true);
    }

    public void ShowResume()
    {
        resumeUI.SetActive(true);
    }

    public void StartButtonClicked()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        resumeUI.SetActive(false);
        introductionPanel.SetActive(false);
    }

    public void ShowHowToPlay()
    {
        howtoUI.SetActive(true);
    }
}
