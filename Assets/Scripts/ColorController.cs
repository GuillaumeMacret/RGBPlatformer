using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityStandardAssets.CrossPlatformInput;
using static Constants;

public class ColorController : MonoBehaviour
{
    private static ColorController INSTANCE;

    public static ColorController GetInstance()
    {
        return INSTANCE;
    }

    private Dictionary<PlatformsColors, bool> m_ColorOnMap;

    public Dictionary<PlatformsColors, AudioClip> audioClipMap;
    private PlatformsColors m_CurrentMusicColor = PlatformsColors.RED;
    private AudioSource m_AudioSource;

    public GameObject redContainer, greenContainer, blueContainer;
    public LevelConfig levelConfig;

    public ColorButton currentTouchingButton;
    private bool m_IsUsingAction;

    private void Awake()
    {

        if (INSTANCE != null && INSTANCE != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            INSTANCE = this;
        }

        audioClipMap = new Dictionary<PlatformsColors, AudioClip>
        {
            {PlatformsColors.RED,Resources.Load<AudioClip>("Sounds/Lifeformed - 9-bit Expedition")},
            {PlatformsColors.GREEN,Resources.Load<AudioClip>("Sounds/Lifeformed - Undiscovery (Dustforce DX OST)")},
            {PlatformsColors.BLUE,Resources.Load<AudioClip>("Sounds/Lifeformed - The Magnetic Tree (Fastfall - Dustforce OST)")}

        };
        Debug.Log(audioClipMap);
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
        m_IsUsingAction = CrossPlatformInputManager.GetButtonDown("Action");

        if (CrossPlatformInputManager.GetButtonDown("Red") && levelConfig.CanUseColor(PlatformsColors.RED))
        {
            SwitchColor(PlatformsColors.RED);
        }
        if (CrossPlatformInputManager.GetButtonDown("Green") && levelConfig.CanUseColor(PlatformsColors.GREEN))
        {
            SwitchColor(PlatformsColors.GREEN);
        }
        if (CrossPlatformInputManager.GetButtonDown("Blue") && levelConfig.CanUseColor(PlatformsColors.BLUE))
        {
            SwitchColor(PlatformsColors.BLUE);
        }

        redContainer.SetActive(m_ColorOnMap[PlatformsColors.RED]);
        greenContainer.SetActive(m_ColorOnMap[PlatformsColors.GREEN]);
        blueContainer.SetActive(m_ColorOnMap[PlatformsColors.BLUE]);

        if (m_IsUsingAction && currentTouchingButton != null && currentTouchingButton.Activate())
        {
            SwitchColor(currentTouchingButton.color);
        }
    }

    private void ChangeMusic(PlatformsColors color)
    {
        m_CurrentMusicColor = color;
        m_AudioSource.clip = audioClipMap[color];
        Debug.Log(audioClipMap[color]);
        m_AudioSource.Play();
    }

    public void SwitchColor(PlatformsColors color)
    {
        m_ColorOnMap[color] = !m_ColorOnMap[color];
        if (m_ColorOnMap[color] && m_CurrentMusicColor != color)
        {
            ChangeMusic(color);
        }
    }

    private void FixedUpdate()
    {
        
    }

    public void UnlockColor(PlatformsColors color)
    {
        levelConfig.AddSwitch(color);
    }
}
