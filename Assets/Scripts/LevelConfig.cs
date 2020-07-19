using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Constants;

public class LevelConfig : MonoBehaviour
{
    private Dictionary<PlatformsColors, bool> m_CanUseSwitch;

    private void Awake()
    {
        m_CanUseSwitch = new Dictionary<PlatformsColors, bool>
        {
            {PlatformsColors.RED,false },
            {PlatformsColors.GREEN,false },
            {PlatformsColors.BLUE,false }
        };
    }  

    public void AddSwitch(PlatformsColors color)
    {
        m_CanUseSwitch[color] = true;
    }

    public bool CanUseColor(PlatformsColors color)
    {
        return m_CanUseSwitch[color];
    }

    public static void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
