using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSlider : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    public void OnChangeSlider(float value)
    {
        audioSource.volume=value;
    }
}
