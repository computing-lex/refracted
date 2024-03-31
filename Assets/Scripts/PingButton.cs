using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PingButton : MonoBehaviour, IInteractable
{
    public Renderer maskMaterial;
    public float pingLength = 5f;
    private float timeSincePing = 0;
    private Vector2 textureOffset = new(0, 0);
    private bool pinged = false;

    public void Interact()
    {
        Debug.Log("Ping!");
        GameManager.instance.Ping();
        GameManager.instance.monster.Pinged(transform.position);
        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
        pinged = true;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pinged)
        {
            if (timeSincePing < pingLength)
            {
                timeSincePing += Time.deltaTime;
                textureOffset.y = Mathf.InverseLerp(0, pingLength, timeSincePing);
            }
            else
            {
                pinged = false;
                textureOffset.y = 0;
                timeSincePing = 0;
            }
        }

        maskMaterial.material.SetTextureOffset("_MainTex", textureOffset);
    }

    public void DoPing()
    {

    }
}
