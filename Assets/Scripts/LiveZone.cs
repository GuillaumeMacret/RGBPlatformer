using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;
using UnityEngine.SceneManagement;

public class LiveZone : MonoBehaviour
{
    private Collider2D m_Collider;

    private void Awake()
    {
        m_Collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
