using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RatioProgression : MonoBehaviour
{
    private Material _material;

    void Awake()
    {
        if (GetComponent<Renderer>() != null)
        {
            // GetComponent<Renderer>().material creates its own instance of the material, so it's not shared
            _material = GetComponent<Renderer>().material;
        }
        else if (GetComponent<Image>() != null)
        {
            // For the UI images, GetComponent<Image>().material is a shared material, so we have to clone it
            _material = Instantiate(GetComponent<Image>().material);
            GetComponent<Image>().material = _material;
        }
    }

    public void SetCompletedRatio(float ratio)
    {
        _material.SetFloat("_Ratio", ratio);
    }
}
