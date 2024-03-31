using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioLooper : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private AudioClip loop;
    private AudioSource m_AudioSource;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
       

        if (GameManager.instance.ShipCore.currentFuel < 0)
        {
            m_AudioSource.loop = false;
            m_AudioSource.Stop();
        }
       
    }
}
