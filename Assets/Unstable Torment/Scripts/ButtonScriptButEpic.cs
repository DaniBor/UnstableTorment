using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScriptButEpic : MonoBehaviour
{
    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) Application.Quit();
    }
}
