using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityStandardAssets.CrossPlatformInput;
using static Constants;

public class ColorController : MonoBehaviour
{
    private static Dictionary<PlatformsColors, bool> m_ColorOnMap;

    public static Dictionary<PlatformsColors, AudioClip> audioClipMap;
    private static PlatformsColors m_CurrentMusicColor = PlatformsColors.RED;
    private static AudioSource m_AudioSource;

    public GameObject redContainer, greenContainer, blueContainer;
    public LevelConfig levelConfig;

    private void Awake()
    {
        audioClipMap = new Dictionary<PlatformsColors, AudioClip>
        {
            {PlatformsColors.RED,Resources.Load<AudioClip>("Sounds/DJ Sona’s Music - Concussive")},
            {PlatformsColors.GREEN,Resources.Load<AudioClip>("Sounds/DJ Sona’s Music - Kinetic")},
            {PlatformsColors.BLUE,Resources.Load<AudioClip>("Sounds/DJ Sona’s Music - Ethereal")}

        };
        m_AudioSource = GetComponent<AudioSource>();
        ChangeMusic(PlatformsColors.BLUE);

        m_ColorOnMap = new Dictionary<PlatformsColors, bool>
        {
            {PlatformsColors.RED,false },
            {PlatformsColors.GREEN,false },
            {PlatformsColors.BLUE,false }
        };
    }

    private void Update()
    {
        redContainer.SetActive(m_ColorOnMap[PlatformsColors.RED]);
        greenContainer.SetActive(m_ColorOnMap[PlatformsColors.GREEN]);
        blueContainer.SetActive(m_ColorOnMap[PlatformsColors.BLUE]);
    }

    private static void ChangeMusic(PlatformsColors color)
    {
        m_CurrentMusicColor = color;
        m_AudioSource.clip = audioClipMap[color];
        Debug.Log(audioClipMap[color]);
        m_AudioSource.Play();
    }

    public static void SwitchColor(PlatformsColors color)
    {
        m_ColorOnMap[color] = !m_ColorOnMap[color];
        if (m_ColorOnMap[color] && m_CurrentMusicColor != color)
        {
            ChangeMusic(color);
        }
    }

    private void FixedUpdate()
    {
        if (CrossPlatformInputManager.GetButtonDown("Red") && levelConfig.canUseRedSwitch)
        {
            SwitchColor(PlatformsColors.RED);
        }
        if (CrossPlatformInputManager.GetButtonDown("Green") && levelConfig.canUseGreenSwitch)
        {
            SwitchColor(PlatformsColors.GREEN);
        }
        if (CrossPlatformInputManager.GetButtonDown("Blue") && levelConfig.canUseBlueSwitch)
        {
            SwitchColor(PlatformsColors.BLUE);
        }
    }
}
