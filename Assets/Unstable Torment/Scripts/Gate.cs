using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Freedom!");
        if(collision.CompareTag("Player")) FindObjectOfType<Manager>().LoadEpicWin();
    }
}
