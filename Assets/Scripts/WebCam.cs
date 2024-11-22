using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebCam : MonoBehaviour
{

    WebCamTexture tex;
    WebCamDevice[] devices;
    Renderer rend;

    void Start()
    {
        devices = WebCamTexture.devices;
        rend = GetComponent<Renderer>();

        tex = new WebCamTexture(devices[0].name);
        rend.material.mainTexture = tex;

        PlayCam();
    }



    public void PlayCam()
    {
        if (devices.Length > 0)
        {
            tex.Play();
        }
    }

    public void StopCam()
    {
        if (devices.Length > 0)
        {
            tex.Stop();
        }
    }
}
