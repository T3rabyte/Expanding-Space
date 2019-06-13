using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollendeEnemy : MonoBehaviour
{
    public float rollSpeed = 0.01f;
    public Transform target;
    public bool facingRight = false;
    public bool InrangePlayer;
    public bool buisy = false;
    public Collider2D RollCollider;

    void Start()
    {
        target = GameObject.Find("Missle/orb Target").GetComponent<Transform>();
    }
    
    void Update()
    {
        Vector3 TargetPlayer = new Vector3(target.position.x, this.transform.position.y, -1.2f);
        transform.position = Vector3.MoveTowards(transform.position, TargetPlayer, rollSpeed);

        InrangePlayer = RollCollider.IsTouchingLayers(LayerMask.GetMask("Player"));
        if (InrangePlayer && !buisy)
        {
            buisy = true;
            StartCoroutine(damage());
        }
        if (!InrangePlayer)
        {
            buisy = false;
            StopCoroutine(damage());
        }

        if (transform.position.x < target.transform.position.x && !facingRight)
        {
            flip();
        }
        else if (transform.position.x > target.transform.position.x && facingRight)
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

    IEnumerator damage()
    {
        GameObject.Find("player").GetComponent<SpriteRenderer>().color = new Color32(255, 144, 144, 255);
        GameObject.Find("GM").GetComponent<GM>().healthPlayer--;
        yield return new WaitForSeconds(0.11f);
        GameObject.Find("player").GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(1);
    }
}
