using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using static Constants;

public class ColorButton : MonoBehaviour
{
    public PlatformsColors color;

    private CircleCollider2D m_Collider;

    private float m_Cooldown = BUTTON_COOLDOWN;

    private bool m_IsActivated;
    public bool IsActivated { get => m_IsActivated;}

    private Animator m_Animator;
    private AudioSource m_ClickSound;

    private void Awake()
    {
        m_Collider = GetComponent<CircleCollider2D>();
        if (m_Collider == null) Debug.LogError("Button can't find its hitbox");

        m_Animator = GetComponent<Animator>();
        if (m_Animator == null) Debug.LogError("Button can't find its animator");

        m_ClickSound = GetComponent<AudioSource>();
        if (m_ClickSound == null) Debug.LogError("Button can't find its audio source");
    }

    private void Update()
    {
        if (m_Cooldown > 0)
        {
            m_IsActivated = true;
            m_Cooldown -= Time.deltaTime;
        }
        else
        {
            m_IsActivated = false;
        }
        m_Animator.SetBool("isActivated", IsActivated);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == PLAYER_TAG && m_Cooldown <= 0)
        {
            ColorController.SwitchColor(color);
            m_ClickSound.Play();
            m_Cooldown = BUTTON_COOLDOWN;
        }
    }
}
