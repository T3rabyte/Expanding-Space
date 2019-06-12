using UnityEngine;

public class cameraController : MonoBehaviour
{
    public GameObject player;

    public float oudelocatie;
    public float nieuwelocatie;

    public GameObject background;

    public Collider2D endOfCamera;
    public bool inRange;

    public float borderLocatie;
    public float yLocatie;

    public float maxSpeed = 5f;

    public float yPlus = 0.063f;

    void Update()
    {

        locatie();

        inRange = endOfCamera.IsTouchingLayers(LayerMask.GetMask("player"));

        borderLocatie = this.transform.position.x;
        borderLocatie -= 1.511f;

        if (player.transform.position.x <= borderLocatie)
        {
            endOfCamera.enabled = true;
        }
        if (player.transform.position.x > borderLocatie)
        {
            endOfCamera.enabled = false;
        }
    }

    void locatie()
    {
        if (GameObject.Find("GM").GetComponent<GM>().healthPlayer >= 1)
        {
            if (player.transform.position.x >= 77.31)
            {
                yLocatie = player.transform.position.y;
                yLocatie += yPlus;
                transform.position = new Vector3(80.44f, yLocatie, -10f);
            }
            if (player.transform.position.x <= 78.98)
            {
                if (GameObject.Find("player").GetComponent<playerControllerScript>().facingRight == true && nieuwelocatie >= oudelocatie)
                {
                    yLocatie = player.transform.position.y;
                    yLocatie += yPlus;
                    background.transform.position = new Vector3(player.transform.position.x, 4.652212f, -1.0625f);
                    transform.position = new Vector3(player.transform.position.x, yLocatie, -10);
                    oudelocatie = transform.position.x;
                    nieuwelocatie = player.transform.position.x;
                }
                else if (GameObject.Find("player").GetComponent<playerControllerScript>().facingRight == true)
                {
                    yLocatie = player.transform.position.y;
                    yLocatie += yPlus;
                    transform.position = new Vector3(oudelocatie, yLocatie, -10);
                    oudelocatie = transform.position.x;
                    nieuwelocatie = player.transform.position.x;
                }
                else if (GameObject.Find("player").GetComponent<playerControllerScript>().facingRight == false)
                {
                    yLocatie = player.transform.position.y;
                    yLocatie += yPlus;
                    transform.position = new Vector3(oudelocatie, yLocatie, -10);
                    nieuwelocatie = player.transform.position.x;
                }
            }
        }
    }
}