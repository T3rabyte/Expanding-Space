using System.Collections;
using UnityEngine;

public class backgroundAudio : MonoBehaviour
{

    public AudioClip BackgroundIntro;
    public AudioClip BackgroundLoop;

    public AudioSource audioSource;

    void Start()
    {
        StartCoroutine(audioStart());
        
    }

    IEnumerator audioStart()
    {
        audioSource.clip = BackgroundIntro;
        audioSource.Play();
        yield return new WaitForSeconds(119);
        StartCoroutine(audioLoop());
        StopCoroutine(audioStart());
    }

    IEnumerator audioLoop()
    {
        audioSource.clip = BackgroundLoop;
        audioSource.Play();
        yield return new WaitForSeconds(117);
    }
}
