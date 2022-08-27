using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayerObject : MonoBehaviour
{
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, audioSource.clip.length);//音が終了したら即時に破壊する
    }
}
