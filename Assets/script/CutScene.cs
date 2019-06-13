﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GameObject.Find("Frame 1").GetComponent<Animator>();
        anim.SetBool("CutSceneStart", true);
        StartCoroutine(CutScneneStart());
    }

    IEnumerator CutScneneStart()
    {
        yield return new WaitForSeconds(6.11f);
        SceneManager.LoadScene("Menu");
    }
}
