using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class Key : MonoBehaviour
{
    public PlatformsColors color;
    public AudioClip pickupsound;

    private CircleCollider2D m_Collider;
    // Start is called before the first frame update
    void Awake()
    {
        m_Collider = GetComponent<CircleCollider2D>();
        if (m_Collider == null) Debug.LogError("Button can't find its hitbox");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            ColorController.GetInstance().UnlockColor(color);
            other.GetComponent<AudioSource>().PlayOneShot(pickupsound);
            Destroy(gameObject);
        }
    }
}
