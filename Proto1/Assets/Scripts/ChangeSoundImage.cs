using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSoundImage : MonoBehaviour
{
    public Sprite SoundOn;
    public Sprite SoundOff;

    private Button button;
    private bool SoundState = true;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        // Only check if sound is off because the default image is on
        if(PlayerPrefs.GetInt("Sound") == 2)
        {
            button.GetComponent<Image>().sprite = SoundOff;
            SoundState = false;
        }
    }

    public void ChangeImage()
    {
        if (SoundState)
        {
            button.GetComponent<Image>().sprite = SoundOff;
            SoundState = false;
        }
        else
        {
            button.GetComponent<Image>().sprite = SoundOn;
            SoundState = true; ;
        }
        
    }

    public void OnEnable()
    {
        MainMenu.ChangedSound += ChangeImage;
    }

}
