using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GM : MonoBehaviour
{
    public int healthPlayer = 100;
    public int healthEnemy = 20;

    public bool startTimer = false;
    
    public Slider sliderHealthEnemy;
    
    public Slider sliderHealthPlayer;

    public bool EnemyActive = false;

    public bool eenkeer = false;

    public int score = 0;
    public Text scoreText;

    void Start()
    {
        StartCoroutine(activateEnemy());
        scoreText.text = "0000000" + score;
    }


    void Update()
    {
        sliderHealthEnemy.value = healthEnemy;
        sliderHealthPlayer.value = healthPlayer;

        if (healthPlayer <= 0)
        {
            SceneManager.LoadScene("Fail");
        }

        if (healthEnemy <= 0 && !eenkeer)
        {
            eenkeer = true;
            StartCoroutine(enemyDed());
        }

        scoreUpdate();
    }

    void scoreUpdate()
    {
        if (score <= 9)
        {
            scoreText.text = "0000000" + score;
        }
        else if (score <= 99)
        {
            scoreText.text = "000000" + score;
        }
        else if (score <= 999)
        {
            scoreText.text = "00000" + score;
        }
        else if (score <= 9999)
        {
            scoreText.text = "0000" + score;
        }
        else if (score <= 99999)
        {
            scoreText.text = "000" + score;
        }
        else if (score <= 999999)
        {
            scoreText.text = "00" + score;
        }
        else if (score <= 9999999)
        {
            scoreText.text = "0" + score;
        }
        else if (score <= 99999999)
        {
            scoreText.text = "" + score;
        }
        else if (score >= 99999999)
        {
            score = 99999999;
            scoreText.text = "" + score;
        }
    }

    IEnumerator activateEnemy()
    {
        yield return new WaitForSeconds(5);
        EnemyActive = true;
        StopCoroutine(activateEnemy());
    }

    IEnumerator enemyDed()
    {
        GameObject.Find("Boss").GetComponent<enemy>().audioSource.clip = GameObject.Find("Boss").GetComponent<enemy>().deathScream;
        GameObject.Find("Boss").GetComponent<enemy>().audioSource.Play();
        GameObject.Find("Boss").GetComponent<Animator>().Play("bossDied");
        menu.LVL1Voltooid = true;
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("Victory");
    }
}