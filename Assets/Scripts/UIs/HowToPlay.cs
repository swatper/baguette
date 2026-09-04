using UnityEngine;
using UnityEngine.InputSystem;

public class HowToPlay : MonoBehaviour
{
    public InputAction exitKey;

    void OnEnable()
    {
        exitKey.Enable();
    }

    void Update()
    {
        CheckExitKey();
    }

    void CheckExitKey()
    {
        if (exitKey.triggered)
            Exit();
    }

    public void Exit()
    {
        exitKey.Disable();
        gameObject.SetActive(false);
    }
}
