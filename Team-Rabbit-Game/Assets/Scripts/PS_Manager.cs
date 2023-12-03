using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Manager : MonoBehaviour
{
    public ParticleSystem m_PS;
    
    public static PS_Manager s_Instance;

    private void Awake()
    {
        s_Instance = this;
    }


    public void PlayParticlesSystem(float xPosition)
    {
        m_PS.transform.position = new Vector3(xPosition, -1f, -1.5f);
        m_PS.gameObject.SetActive(true);
    }
}
