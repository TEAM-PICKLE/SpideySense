using System.Collections;
using System.Collections.Generic;
using Oculus.VoiceSDK.UX;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    new public int m_MaxHealth;
    new public int m_currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        int m_MaxHealth = 10;
        int m_currentHealth = m_MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_currentHealth == 0)
        {
            print("you lose, but I'll allow you to keep going");
        }

        
    }


    public void TakeDamage(int damageValue)
    {
        m_currentHealth += damageValue;
    }

    public void ResetHealth()
    {
        int m_currentHealth = m_MaxHealth;
    }
}
