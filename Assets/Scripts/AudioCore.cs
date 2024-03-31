using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AudioCore : MonoBehaviour
{
    // Start is called before the first frame update

    private AudioSource m_AudioSource;
    [SerializeField] private AudioClip spooky1;
    [SerializeField] private AudioClip spooky2;
    [SerializeField] private AudioClip loop;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        InvokeRepeating("PlaySpook", Random.Range(30, 60), Random.Range(Random.Range(30, 40), Random.Range(60, 120)));
    }

    private void PlaySpook()
    {
        switch (Random.Range(1, 4))
        {
            case 1:
                m_AudioSource.PlayOneShot(spooky1);
                break;
            case 2:
                m_AudioSource.PlayOneShot(spooky2);
                break;
            case 3:
                m_AudioSource.PlayOneShot(loop);
                break;
            default:
                m_AudioSource.PlayOneShot(spooky1);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
