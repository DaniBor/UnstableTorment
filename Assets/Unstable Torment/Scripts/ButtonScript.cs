using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("HellArena");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) Application.Quit();
    }
    
}
