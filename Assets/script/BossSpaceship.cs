using System.Collections;
using UnityEngine;

public class BossSpaceship : MonoBehaviour
{
    public bool BossActive = false;
    public GameObject BossOrb;
    public GameObject Missle;

    public bool laserActive = false;

    Animator anim;

    public AudioSource audioSource;

    public AudioClip laserCharge;
    public AudioClip LaserShot;
    public AudioClip OrbShot;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(startBoss());
    }
    
    void Update()
    {
        
    }

    IEnumerator startBoss()
    {
        yield return new WaitForSeconds(1);
        BossActive = true;
        StartCoroutine(shoot());
        StartCoroutine(missleShoot());
        StartCoroutine(Laser());
        StopCoroutine(startBoss());
    }

    IEnumerator shoot()
    {
        while (BossActive)
        {
            yield return new WaitForSeconds(3);
            anim.Play("Boss2Shoot");
            yield return new WaitForSeconds(0.22f);
            audioSource.clip = OrbShot;
            audioSource.Play();
            Instantiate(BossOrb, new Vector3(this.gameObject.transform.position.x - 0.706f, this.transform.position.y - 0.4472385f, -2), Quaternion.identity);
        }
        StopCoroutine(shoot());
    }

    IEnumerator missleShoot()
    {
        while (BossActive)
        {
            yield return new WaitForSeconds(5.3f);
            Instantiate(Missle, new Vector3(this.gameObject.transform.position.x + 0.438f, this.transform.position.y + 0.426f, -2), Quaternion.Euler(new Vector3(0, 0, 90)));
        }
        StopCoroutine(missleShoot());
    }

    IEnumerator Laser()
    {
        while (BossActive)
        {
            Animator animLaser = GameObject.Find("laser").GetComponent<Animator>();

            yield return new WaitForSeconds(8);
            if (GameObject.Find("player").GetComponent<Transform>().position.x > -0.517)
            {
                GameObject.Find("laser").GetComponent<Transform>().position = new Vector3(0.167f, 0.181f, -2);
                animLaser.Play("LaserShoot");
                audioSource.clip = laserCharge;
                audioSource.Play();
                yield return new WaitForSeconds(1.2f);
                audioSource.clip = LaserShot;
                audioSource.Play();
                laserActive = true;
                yield return new WaitForSeconds(0.8f);
                laserActive = false;
            }
            else if (GameObject.Find("player").GetComponent<Transform>().position.x < -0.517)
            {
                GameObject.Find("laser").GetComponent<Transform>().position = new Vector3(-1.017f, 0.181f, -2);
                animLaser.Play("LaserShoot");
                audioSource.clip = laserCharge;
                audioSource.Play();
                yield return new WaitForSeconds(1.2f);
                audioSource.clip = LaserShot;
                audioSource.Play();
                laserActive = true;
                yield return new WaitForSeconds(0.8f);
                laserActive = false;
            }
        }
        StopCoroutine(Laser());
    }
}
