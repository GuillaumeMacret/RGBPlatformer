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
    public GameObject redContainerKeepMotion, greenContainerKeepMotion, blueContainerKeepMotion;
    public LevelConfig levelConfig;

    public ColorButton currentTouchingButton;
    private bool m_IsUsingAction;

    private AudioSource m_crossfadeAudioSource;
    public float crossFadeTime = .5f;
    public float musicBaseVolume = .2f;
    private float crossFadeTimer;

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
        m_AudioSource = GetComponents<AudioSource>()[0];
        m_AudioSource.clip = audioClipMap[PlatformsColors.BLUE];
        m_AudioSource.Play();

        m_crossfadeAudioSource = GetComponents<AudioSource>()[1];


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

        foreach(SpriteRenderer child in redContainerKeepMotion.GetComponentsInChildren<SpriteRenderer>())
        {
            child.enabled = m_ColorOnMap[PlatformsColors.RED];
        }
        foreach (Collider2D child in redContainerKeepMotion.GetComponentsInChildren<Collider2D>())
        {
            child.enabled = m_ColorOnMap[PlatformsColors.RED];
        }

        foreach (SpriteRenderer child in blueContainerKeepMotion.GetComponentsInChildren<SpriteRenderer>())
        {
            child.enabled = m_ColorOnMap[PlatformsColors.BLUE];
        }
        foreach (Collider2D child in blueContainerKeepMotion.GetComponentsInChildren<Collider2D>())
        {
            child.enabled = m_ColorOnMap[PlatformsColors.BLUE];
        }

        foreach (SpriteRenderer child in greenContainerKeepMotion.GetComponentsInChildren<SpriteRenderer>())
        {
            child.enabled = m_ColorOnMap[PlatformsColors.GREEN];
        }
        foreach (Collider2D child in greenContainerKeepMotion.GetComponentsInChildren<Collider2D>())
        {
            child.enabled = m_ColorOnMap[PlatformsColors.GREEN];
        }

        if (m_IsUsingAction && currentTouchingButton != null && currentTouchingButton.Activate())
        {
            SwitchColor(currentTouchingButton.color);
        }

        if (crossFadeTimer > 0)
        {
            crossFadeTimer -= Time.deltaTime;
            float fadePercent = crossFadeTimer / crossFadeTime;
            m_AudioSource.volume = musicBaseVolume * (1 - fadePercent);
            m_crossfadeAudioSource.volume = musicBaseVolume * fadePercent;
            Debug.Log(fadePercent);
        }
    }

    private void ChangeMusic(PlatformsColors color)
    {
        m_CurrentMusicColor = color;
        float currTime = m_AudioSource.time;

        crossFadeTimer = crossFadeTime;
        m_crossfadeAudioSource.clip = m_AudioSource.clip;
        m_AudioSource.clip = audioClipMap[color];

        m_crossfadeAudioSource.Play();
        m_crossfadeAudioSource.time = currTime;
        m_AudioSource.Play();
        m_AudioSource.time = currTime;
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
