using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraResolutionHandler : MonoBehaviour
{
    [SerializeField] public int divisor = 4;
    [SerializeField] public RawImage displayImage;

    void Start()
    {
        Camera c = GetComponent<Camera>();
        if (c.targetTexture != null)
        {
            c.targetTexture.Release();
        }
        c.targetTexture = new RenderTexture(Screen.width / divisor, Screen.height / divisor, 24);

        displayImage.texture = c.targetTexture;

        Debug.Log(c.targetTexture.width);
    }
}
