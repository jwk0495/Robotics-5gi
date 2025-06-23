using System.Collections.Generic;
using UnityEngine;

// 여러가지 오디오 클립들을 버튼에 따라 다르게 재생한다.
public class AudioManager : MonoBehaviour
{
    AudioSource audioSource; // Player역할
    public List<AudioClip> audioClips; // 음원파일들

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // 캐싱
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            audioSource.clip = audioClips[1];
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            audioSource.clip = audioClips[2];
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            audioSource.clip = audioClips[3];
            audioSource.Play();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            audioSource.clip = audioClips[4];
            audioSource.Play();
        }
    }
}
