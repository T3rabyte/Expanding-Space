using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingOrbSpawner : MonoBehaviour
{
    public Transform Player;
    public float Flyspeed = 0.005f;
    public bool alive = true;

    public GameObject missle;

    void Start()
    {
        StartCoroutine(shoot());
    }

    
    void Update()
    {
        Vector3 TargetPlayer = new Vector3(Player.position.x, 5.295693f, -1);
        transform.position = Vector3.MoveTowards(transform.position, TargetPlayer, Flyspeed);
    }

    IEnumerator shoot()
    {
        while (alive)
        {
            Instantiate(missle, new Vector3(this.transform.position.x, this.transform.position.y - 0.137693f, -1), Quaternion.identity);
            yield return new WaitForSeconds(8);
        }
    }
}
