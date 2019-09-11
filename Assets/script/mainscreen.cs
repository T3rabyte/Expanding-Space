using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class mainscreen : MonoBehaviour
{
    public Animator anim;
    public bool Busy = false;
    public AudioSource AS;
    public AudioClip backgroundMusic;

    void Start()
    {
        AS.clip = backgroundMusic;
        AS.Play();
    }

    void Update()
    {
        if (Random.value <= 0.002 && !Busy)
        {
            Busy = true;
            StartCoroutine(Bling());
        }

        if (Input.GetKey(KeyCode.Space))
        {
            anim = GameObject.Find("fade").GetComponent<Animator>();
            StartCoroutine(StartGame());
        }
    }

    IEnumerator Bling()
    {
        anim.Play("mainscreenBling");
        yield return new WaitForSeconds(0.45f);
        Busy = false;
        StopCoroutine(Bling());
    }

    IEnumerator StartGame()
    {
        anim.SetBool("FadeStart", true);
        yield return new WaitForSeconds(0.45f);
        SceneManager.LoadScene("Cutscene");
        StopCoroutine(StartGame());
    }
}
