using UnityEngine;
using UnityEngine.UI;

public class ChangeVibrationImage : MonoBehaviour
{
    [SerializeField]
    private Sprite VibrateOn, VibrateOff;

    [SerializeField]
    private Button VibrateButton;

    private bool VibrateState = true;

    void Awake()
    {
        VibrateButton = GetComponent<Button>();

        // Only check if sound is off because the default image is on
        if (PlayerPrefs.GetInt("Vibration") == 2)
        {
            VibrateButton.GetComponent<Image>().sprite = VibrateOff;
            VibrateState = false;
        }
    }

    // Change image
    public void ChangeImage()
    {
        if (VibrateState)
        {
            VibrateButton.GetComponent<Image>().sprite = VibrateOff;
            VibrateState = false;
        }
        else
        {
            VibrateButton.GetComponent<Image>().sprite = VibrateOn;
            VibrateState = true; ;
        }
    }

    // Add ChangeImage method to Trigger ChangedSound (Main Menu)
    public void OnEnable()
    {
        GetComponentInParent<MainMenu>().ChangedVibration += ChangeImage;
    }
}
