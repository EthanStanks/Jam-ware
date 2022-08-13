using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    [SerializeField] Sprite soundIcon;
    [SerializeField] bool isMuted;
    [SerializeField] Sprite muteIcon;
    [SerializeField] Image Icon;

    private void Start()
    {
        isMuted = false;
    }

    public void ClickedIcon()
    {
        if(isMuted) // if the sound is muted
        {
            Icon.sprite = soundIcon;
            isMuted = false;
            // call un mute function
        }
        else // if the sound isn't muted
        {
            Icon.sprite = muteIcon;
            isMuted = true;
            // call mute function
        }
    }
}
