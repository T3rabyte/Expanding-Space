using UnityEngine;

public class boolUpdate : MonoBehaviour
{
    public static bool LVL1Complete = false;
    public static bool LVL2Complete = false;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
