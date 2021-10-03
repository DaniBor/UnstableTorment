using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnouncementTextScript : MonoBehaviour
{
    private Text text;
    public float timer = 3f;
    private bool poppedUp = false;


    private void Awake()
    {
        text = GetComponent<Text>();
        text.text = "";
    }
    public void PopUp(int tier)
    {
        timer = timer + Time.time;
        poppedUp = true;
        switch (tier)
        {
            case 0:
                text.text = "Your speed has been increased! Your Range has been increased!";
                break;
            case 1:
                text.text = "The Arena grew in size! More Enemies are coming!";
                break;
            case 2:
                text.text = "Your Instability rate increased!";
                break;
            case 3:
                text.text = "Your attack became bigger and stronger!";
                break;
            case 4:
                text.text = "Your Instability rate increased! You became faster! The Arena grew in size! More Enemies are coming!";
                break;
            case 5:
                text.text = "Your attack became bigger!";
                break;
            case 6:
                text.text = "Your Instability rate increased!";
                break;
            case 7:
                text.text = "Your attack became bigger and stronger!";
                break;
            case 8:
                text.text = "Your Instability rate increased!";
                break;
            case 9:
                text.text = "You became faster!";
                break;
            case 10:
                text.text = "Your Instability rate increased! Your attack became bigger!";
                break;
            case 11:
                text.text = "A gate to the outside has opened...";
                break;

        }
    }

    private void Update()
    {
        
        if (poppedUp)
        {
            if(Time.time > timer)
            {
                text.text = "";
                timer = 3f;
                poppedUp = false;
            }
        }
    }
}
