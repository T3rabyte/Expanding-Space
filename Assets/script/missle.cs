using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class missle : MonoBehaviour
{
    public Animator anim;
    public Transform target;
    public float MissleSpeed = 5;
    public Collider2D MissleCollider;
    public bool buisy;

    public bool player;
    public bool destroy;
    public bool bullet;
    public bool ground;
    public bool blocked;

    public bool active = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Missle/orb Target").GetComponent<Transform>();
    }
    void Update()
    {
        if (!active)
        {
            StartCoroutine(MissleStart());
        }

        if (active)
        {
            Vector3 MissleTarget = new Vector3(target.position.x, target.position.y, -1);
            transform.right = new Vector3(target.position.x - transform.position.x, target.position.y - transform.position.y, 0);
            transform.position = Vector3.MoveTowards(transform.position, MissleTarget, MissleSpeed);

            player = MissleCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
            destroy = MissleCollider.IsTouchingLayers(LayerMask.GetMask("border"));
            bullet = MissleCollider.IsTouchingLayers(LayerMask.GetMask("bullet"));
            ground = MissleCollider.IsTouchingLayers(LayerMask.GetMask("ground"));
            blocked = MissleCollider.IsTouchingLayers(LayerMask.GetMask("block"));


            if (player && !buisy)
            {
                buisy = true;
                StartCoroutine(damage());
            }
            if (destroy || bullet || ground || blocked)
            {
                StartCoroutine(Explosion());
            }
        }
    }

    IEnumerator damage()
    {
        anim.Play("missleExplosion");
        GameObject.Find("player").GetComponent<SpriteRenderer>().color = new Color32(255, 144, 144, 255);
        yield return new WaitForSeconds(0.11f);
        GameObject.Find("player").GetComponent<SpriteRenderer>().color = Color.white;
        GameObject.Find("GM").GetComponent<GM>().healthPlayer -= 5;
        Destroy(this.gameObject);
        StopCoroutine(damage());
    }

    IEnumerator MissleStart()
    {
        Vector3 MissleTarget = new Vector3(1.496f, 1.411f, -1);
        transform.position = Vector3.MoveTowards(transform.position, MissleTarget, MissleSpeed);
        yield return new WaitForSeconds(3);
        active = true;
        StopCoroutine(MissleStart());
    }

    IEnumerator Explosion()
    {
        anim.Play("missleExplosion");
        yield return new WaitForSeconds(0.18f);
        Destroy(this.gameObject);
    }
}
