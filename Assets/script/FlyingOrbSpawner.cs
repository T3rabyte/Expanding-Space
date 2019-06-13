using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingOrbSpawner : MonoBehaviour
{
    public Transform Player;
    public float Flyspeed = 0.005f;
    public Animator anim;
    public bool alive = true;
    public GameObject missle;
    public bool facingRight = false;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        StartCoroutine(shoot());
    }

    
    void Update()
    {
        Vector3 TargetPlayer = new Vector3(Player.position.x, 5.295693f, -0.9f);
        transform.position = Vector3.MoveTowards(transform.position, TargetPlayer, Flyspeed);

        if (transform.position.x < Player.transform.position.x && !facingRight)
        {
            flip();
        }
        else if (transform.position.x > Player.transform.position.x && facingRight)
        {
            flip();
        }
    }

    public void flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    IEnumerator shoot()
    {
        while (alive)
        {
            anim.SetBool("shoot", true);
            if (!facingRight)
            {
                Instantiate(missle, new Vector3(this.transform.position.x - 0.042f, this.transform.position.y - 0.037693f, -1), Quaternion.identity);
            }
            if (facingRight)
            {
                Instantiate(missle, new Vector3(this.transform.position.x + 0.042f, this.transform.position.y + 0.037693f, -1), Quaternion.identity);
            }
            yield return new WaitForSeconds(0.25f);
            anim.SetBool("shoot", false);
            yield return new WaitForSeconds(3);
        }
    }
}
