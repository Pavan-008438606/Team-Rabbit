using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Sprite[] MusicSprites;
    public Sprite[] SoundsSprites;

    public Image musicImage;
    public Image soundImage;
    
    public void TurnOffMusic()
    {
        SoundManager.instance.isMusicOn = !SoundManager.instance.isMusicOn;
        soundImage.sprite = SoundManager.instance.isMusicOn ? SoundsSprites[0] : SoundsSprites[1];
    }
    
    
    public void TurnOffSounds()
    {
        SoundManager.instance.isSoundOn = !SoundManager.instance.isSoundOn;
        SoundManager.instance.PlayBackGroundSound(SoundManager.instance.isSoundOn);
        musicImage.sprite = SoundManager.instance.isSoundOn ? MusicSprites[0] : MusicSprites[1];
    }
}
