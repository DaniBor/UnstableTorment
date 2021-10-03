using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstabilityBar : MonoBehaviour
{
    public Image instabilityBar;
    public PlayerScript player;
    private float instabilityProgress;

    private void Update()
    {
        instabilityProgress = ((float)player.curMeter / player.maxMeter);
        instabilityBar.fillAmount = instabilityProgress;
    }
}
