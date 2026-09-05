using System;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [Tooltip("게임 스타트 패널")]
    [SerializeField] private GameObject introductionPanel;
    [SerializeField] private GameObject howtoUI;

    void Awake()
    {
        introductionPanel.SetActive(true);
    }

    public void StartButtonClicked()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        introductionPanel.SetActive(false);
    }

    public void ShowHowToPlay()
    {
        howtoUI.SetActive(true);
    }
}
