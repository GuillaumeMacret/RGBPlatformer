using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class Bullet : MonoBehaviour
{
    private Collider2D m_Collider;
    public Vector3 velocity;

    private void Awake()
    {
        m_Collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            LevelConfig.GameOver();
        }
        Destroy(this.gameObject);
    }

    private void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
