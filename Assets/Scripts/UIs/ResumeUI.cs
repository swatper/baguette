using UnityEngine;
using UnityEngine.UI;

public class ResumeUI : MonoBehaviour
{
    public GameStarter starter;
    public InputField nameField;

    public PlayerController player;

    public GameObject errorMessage;
    public void StartGame()
    {
        if (nameField.text.Length > 6)
        {
            errorMessage.SetActive(true);
            return;
        }
        else
        {
            errorMessage.SetActive(false);
            player.SetPlayerName(nameField.text);
            starter.StartButtonClicked();
        }
    }
}
