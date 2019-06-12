using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class menu : MonoBehaviour
{

    private Scene scene;

    public void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void Update()
    {
        if (scene.name == "Menu")
        {
            Button naarLVL1Button;
            naarLVL1Button = GameObject.Find("Canvas").GetComponentInChildren<Button>();

            if (boolUpdate.LVL1Complete == true)
            {
                naarLVL1Button.interactable = false;
            }
        }
    }

    public void playAgain()
    {
        SceneManager.LoadScene("Menu");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("main Menu");
    }

    public void naarLVL1()
    {
        SceneManager.LoadScene("lvl1");
    }
}