using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Image FullScreen_checkMark;
    public Dropdown ResolutionDropdown;
    public Slider VolumeSlider;
    public TMP_Dropdown QualitySetting;
    public Toggle FullScreen_Toggle;

    private int FullScreen;
    Resolution[] resolutions; 

    private void Start()
    {
        float VolumeValue = PlayerPrefs.GetFloat("Volume Slider", 0);
        VolumeSlider.value = VolumeValue;
        //AudioListener.volume = VolumeValue;

        QualitySetting.value = PlayerPrefs.GetInt("Quality Setting", 0);

        if (PlayerPrefs.GetInt("Full Screen", 0) == 1)
        {
            FullScreen_Toggle.isOn = true;
            FullScreen_checkMark.gameObject.SetActive(true);
        }

        else if (PlayerPrefs.GetInt("Full Screen", 0) == 0)
        {
            FullScreen_Toggle.isOn = false;
            FullScreen_checkMark.gameObject.SetActive(false);
        }

        resolutions = Screen.resolutions;
        
        ResolutionDropdown.ClearOptions();

        List<string> ResolutionList = new List<string>();


        for (int i = 0; i < resolutions.Length; i++)
        {
            string Options = resolutions[i].width + "x" + resolutions[i].height;

            ResolutionList.Add(Options);
        }

        ResolutionDropdown.AddOptions(ResolutionList);
        ResolutionDropdown.value = PlayerPrefs.GetInt("Resolution Setting", 0); // Current resolution is autometically set
        ResolutionDropdown.RefreshShownValue();
    }

    private void Update()
    {
        PlayerPrefs.SetInt("Quality Setting", QualitySetting.value);
    }

    public void SetResolution(int resolutionindex)
    {
        Resolution resolution = resolutions[resolutionindex];

        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("Resolution Setting", resolutionindex);
    }

    public void SetVolume(float Volume)
    {
        audioMixer.SetFloat("Volume", Volume);
        //AudioListener.volume = Volume;
        PlayerPrefs.SetFloat("Volume Slider", Volume);
    }

    public void SetQuality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;

        if (isFullScreen)
        {
            FullScreen_checkMark.gameObject.SetActive(true);
            FullScreen = 1;
        }

        if (!isFullScreen)
        {
            FullScreen_checkMark.gameObject.SetActive(false);
            FullScreen = 0;
        }

        PlayerPrefs.SetInt("Full Screen", FullScreen);
    }
}