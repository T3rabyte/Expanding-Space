using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menu : MonoBehaviour
{
    private Scene scene;
    public bool planeetReveel = false;
    public bool informatieLVL1 = false;
    public bool informatieLVL2 = false;
    public bool LVL1Available = true;
    public bool LVL2Available = false;
    public Animator anim;
    public Text text;
    private SpriteState ST;

    public Button buttonNaarLVL1;
    public Sprite buttonNaarLVL1normaal;
    public Sprite buttonNaarLVL1Ingedrukt;
    public Sprite buttonNaarLVL1Ingedrukt2;

    public Button buttonNaarLVL2;
    public Sprite buttonNaarLVL2normaal;
    public Sprite buttonNaarLVL2Ingedrukt;
    public Sprite buttonNaarLVL2Ingedrukt2;

    public Sprite buttonShipNormaal;
    public Sprite buttonShipIngedrukt;
    public Sprite buttonShipIngedrukt2;

    public AudioSource AS;
    public AudioClip backgroundMusic;

    public GameObject controls;
    public GameObject backButton;


    public void Start()
    {
        AS.clip = backgroundMusic;
        AS.Play();
        scene = SceneManager.GetActiveScene();
    }

    public void Update()
    {
        if (scene.name == "Menu")
        {
            anim = GameObject.Find("fade").GetComponent<Animator>();

            if (boolUpdate.LVL1Complete == true)
            {
                LVL1Available = false;
                LVL2Available = true;
            }
        }
    }

    public void naarControls()
    {
        controls.SetActive(true);
        backButton.SetActive(true);
    }

    public void playAgain()
    {
        SceneManager.LoadScene("Menu");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("main Menu");
    }

    public void back()
    {
        controls.SetActive(false);
        backButton.SetActive(false);
        ResetMenu();
    }

    public void ResetMenu()
    {
        informatieLVL1 = false;
        buttonNaarLVL1.image.sprite = buttonNaarLVL1normaal;
        informatieLVL2 = false;
        buttonNaarLVL2.image.sprite = buttonNaarLVL2normaal;
        text.text = "";
    }

    public void naarLVL1()
    {
        if (informatieLVL1 == false)
        {
            informatieLVL1 = true;
            informatieLVL2 = false;
            buttonNaarLVL2.image.sprite = buttonNaarLVL2normaal;
            text.text = "Tihiri";
            buttonNaarLVL1.image.sprite = buttonNaarLVL1Ingedrukt;
            ST.pressedSprite = buttonNaarLVL1Ingedrukt2;
            buttonNaarLVL1.spriteState = ST;
        }
        else if (informatieLVL1 == true && LVL1Available)
        {
            StartCoroutine(LoadLVL1());
        }
    }

    public void naarLVL2()
    {
        if (!planeetReveel)
        {
            if (informatieLVL2 == false)
            {
                informatieLVL2 = true;
                informatieLVL1 = false;
                buttonNaarLVL1.image.sprite = buttonNaarLVL1normaal;
                text.text = "Home";
                buttonNaarLVL2.image.sprite = buttonNaarLVL2Ingedrukt;
                ST.pressedSprite = buttonNaarLVL2Ingedrukt2;
                buttonNaarLVL2.spriteState = ST;
            }
            else if (informatieLVL2 == true && LVL2Available)
            {
                anim = GameObject.Find("fade").GetComponent<Animator>();
                anim.Play("FadePlaneetExplosie");
                planeetReveel = true;
                informatieLVL2 = false;
                buttonNaarLVL2normaal = buttonShipNormaal;
            }
        }
        if (planeetReveel)
        {
            if (informatieLVL2 == false)
            {
                informatieLVL2 = true;
                informatieLVL1 = false;
                buttonNaarLVL1.image.sprite = buttonNaarLVL1normaal;
                text.text = "praetor";
                buttonNaarLVL2.image.sprite = buttonShipIngedrukt;
                ST.pressedSprite = buttonShipIngedrukt2;
                buttonNaarLVL2.spriteState = ST;
            }
            else if (informatieLVL2 == true && LVL2Available)
            {
                StartCoroutine(LoadLVL2());
            }
        }
    }

    IEnumerator LoadLVL1()
    {
        anim.SetBool("FadeStart", true);
        yield return new WaitForSeconds(0.45f);
        SceneManager.LoadScene("lvl1");
    }

    IEnumerator LoadLVL2()
    {
        anim.SetBool("FadeStart", true);
        yield return new WaitForSeconds(0.45f);
        SceneManager.LoadScene("lvl2");
    }
}