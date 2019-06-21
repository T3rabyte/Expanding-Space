using UnityEngine;
using System.Collections;

public class Boss1 : MonoBehaviour
{
    public bool Buisy = false;
    public GameObject player;
    public GameObject enemyOrb;

    public Collider2D groundSpike;

    public Collider2D laserRange;

    public bool inRange;
    public bool blocked;

    public Animator anim;

    public AudioSource audioSource;

    public AudioClip orbShoot;
    public AudioClip groundPound;
    public AudioClip laserCharging;
    public AudioClip laserShot;
    public AudioClip deathScream;
    
    float distance;
    public float maxAllowedDistance = 1.837f;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("player");
    }

    void Update()
    {
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < maxAllowedDistance)
        {
            if (GameObject.Find("GM").GetComponent<GM>().EnemyActive == true)
            {
                if (GameObject.Find("GM").GetComponent<GM>().healthEnemy >= 0)
                {
                    if (!Buisy && player.transform.position.x >= 78.954 && player.transform.position.x <= 81.47 && player.transform.position.y >= 4.682)
                    {
                        Buisy = true;
                        StartCoroutine(enemySchootingOrb());
                    }
                    else if (!Buisy && player.transform.position.y <= 4.682 && player.transform.position.x >= 80.208 && player.transform.position.x <= 81.47)
                    {
                        Buisy = true;
                        StartCoroutine(enemyGroundPound());
                    }
                    else if (!Buisy && player.transform.position.y <= 4.682 && player.transform.position.x >= 78.954 && player.transform.position.x <= 80.208)
                    {
                        Buisy = true;
                        StartCoroutine(enemyLazerBeam());

                    }
                }
            }
        }
    }

    IEnumerator enemySchootingOrb()
    {
        anim.Play("bossShoot");
        yield return new WaitForSeconds(0.2f);
        audioSource.clip = orbShoot;
        audioSource.Play();
        Instantiate(enemyOrb, new Vector3(this.gameObject.transform.position.x + 0.486f,this.transform.position.y - 0.028f, -2), Quaternion.identity);
        yield return new WaitForSeconds(1);
        Buisy = false;
        StopCoroutine(enemySchootingOrb());
    }

    IEnumerator enemyGroundPound()
    {
        anim.SetBool("stamp", true);
        
        yield return new WaitForSeconds(1);
        GameObject.Find("Groundpound Range").GetComponent<Animator>().SetBool("spike", true);
        audioSource.clip = groundPound;
        audioSource.Play();
        inRange = groundSpike.IsTouchingLayers(LayerMask.GetMask("Player"));

        if (inRange)
        {
            GameObject.Find("player").GetComponent<SpriteRenderer>().color = new Color32(255, 144, 144, 255);
            yield return new WaitForSeconds(0.11f);
            GameObject.Find("player").GetComponent<SpriteRenderer>().color = Color.white;
            GameObject.Find("GM").GetComponent<GM>().healthPlayer -= 20;
        }
        yield return new WaitForSeconds(0.3f);
        GameObject.Find("Groundpound Range").GetComponent<Animator>().SetBool("spike", false);
        anim.SetBool("stamp", false);
        yield return new WaitForSeconds(1.5f);
        Buisy = false;
        StopCoroutine(enemyGroundPound());
    }

    IEnumerator enemyLazerBeam()
    {
        anim.SetBool("laser", true);

        audioSource.clip = laserCharging;
        audioSource.pitch = 3;
        audioSource.Play();

        yield return new WaitForSeconds(1.3f);

        audioSource.clip = laserShot;
        audioSource.pitch = 1;
        audioSource.Play();

        inRange = laserRange.IsTouchingLayers(LayerMask.GetMask("Player"));
        blocked = laserRange.IsTouchingLayers(LayerMask.GetMask("block"));
        if (blocked && GameObject.Find("player").GetComponent<playerControllerScript>().facingRight == true)
        {
        }
        else if (inRange)
        {
            GameObject.Find("player").GetComponent<SpriteRenderer>().color = new Color32(255, 144, 144, 255);
            yield return new WaitForSeconds(0.11f);
            GameObject.Find("player").GetComponent<SpriteRenderer>().color = Color.white;
            GameObject.Find("GM").GetComponent<GM>().healthPlayer -= 40;
        }

        anim.SetBool("laser", false);
        yield return new WaitForSeconds(1.8f);
        Buisy = false;
        StopCoroutine(enemyLazerBeam());
    }
}
