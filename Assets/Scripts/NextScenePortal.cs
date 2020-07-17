using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;
using UnityEngine.SceneManagement;

public class NextScenePortal : MonoBehaviour
{
    public string sceneName;

    private Collider2D m_Collider;

    private void Awake()
    {
        m_Collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == PLAYER_TAG)
        {
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
