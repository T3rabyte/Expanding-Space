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

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 MissleTarget = new Vector3(target.position.x,target.position.y, -1);
        transform.right = target.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, MissleTarget, MissleSpeed);

        player = MissleCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
        destroy = MissleCollider.IsTouchingLayers(LayerMask.GetMask("border"));
        bullet = MissleCollider.IsTouchingLayers(LayerMask.GetMask("bullet"));
        ground = MissleCollider.IsTouchingLayers(LayerMask.GetMask("ground"));


        if (player && !buisy)
        {
            buisy = true;
            StartCoroutine(damage());
        }
        if (destroy || bullet || ground)
        {
            Destroy(this.gameObject);
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
}
