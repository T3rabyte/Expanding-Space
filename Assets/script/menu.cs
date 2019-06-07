using UnityEngine.SceneManagement;
using UnityEngine;

public class menu : MonoBehaviour
{
    public static bool LVL1Voltooid = false;

    public Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        DontDestroyOnLoad(this.gameObject);
        if (scene.name == "Menu")
        {

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

