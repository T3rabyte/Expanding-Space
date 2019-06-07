using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class mainscreen : MonoBehaviour
{
    public Animator anim;

    public bool Buisy = false;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (Random.value <= 0.01 && !Buisy)
        {
            Buisy = true;
            StartCoroutine(Bling());
        }

        if (Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    IEnumerator Bling()
    {
        anim.Play("mainscreenBling");
        yield return new WaitForSeconds(0.45f);
        Buisy = false;
        StopCoroutine(Bling());
    }
}
