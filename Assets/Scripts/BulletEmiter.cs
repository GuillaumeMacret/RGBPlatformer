using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEmiter : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Vector3 bulletDirection;
    public GameObject bulletContainer;
    public int rotation;

    public int nbShoot;
    public float interShoots, cooldownSalve;

    private float m_CooldownCpt, m_InterShootsCpt;
    private int m_CptShooted;

    private void Update()
    {
        if(m_CptShooted < nbShoot && m_CooldownCpt <= 0)
        {
            //Shooting mode
            if(m_InterShootsCpt <= 0)
            {
                Shoot();
                m_CptShooted++;
                m_InterShootsCpt = interShoots;
                if(m_CptShooted == nbShoot)
                {
                    m_CooldownCpt = cooldownSalve;
                }
            }
            else
            {
                m_InterShootsCpt -= Time.deltaTime;
            }
        }
        else
        {
            //Cooling down
            m_CooldownCpt -= Time.deltaTime;
            if(m_CooldownCpt <= 0)
            {
                m_CptShooted = 0;
                m_InterShootsCpt = 0;
            }
        }
    }

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.velocity = bulletDirection;
        //bullet.transform.parent = bulletContainer.transform;
        bullet.transform.position = transform.position;
        Quaternion qt = new Quaternion();
        qt.eulerAngles = new Vector3(0, 0, rotation);
        bullet.transform.rotation = qt;
    }
}
