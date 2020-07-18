using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class WindArea : MonoBehaviour
{
    public Vector3 WindStrengh;

    private BoxCollider2D m_Collider;

    void Awake()
    {
        m_Collider = GetComponent<BoxCollider2D>();
        if (m_Collider == null) Debug.LogError("Wind area can't find its hitbox");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            Debug.Log(other.GetComponent<Rigidbody2D>());
            other.GetComponent<Rigidbody2D>().AddForce(WindStrengh);
        }
    }
}
