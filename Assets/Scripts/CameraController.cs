using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Vector3 rotation = new Vector3(45, 0, 0);
    public int height = 10;
    public int distanceOffset = 10;
    private bool stopCamera = false;

    GameObject[] players;
	// Use this for initialization
	void Start () {
        transform.position = new Vector3(0, height, 0);
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Quaternion.Euler(rotation);

        float x = 0;
        float z = 0;

        for (int i = 0; i < players.Length; i++)
        {
            x += players[i].gameObject.transform.position.x;
            z += players[i].gameObject.transform.position.z;
            if(players[i].GetComponent<PlayerController>().stopCamera == true)
            {
                stopCamera = true;
            }
        }

        if (stopCamera == false)
        {
             gameObject.transform.position = new Vector3(x / players.Length, transform.position.y, z / players.Length - distanceOffset);
        }
        stopCamera = false;
    }

    bool cameraIsNotCentered(float x, float z, int numPlayers)
    {
        float averageX = x / numPlayers;
        float averageZ = z / numPlayers - distanceOffset;

        if(Camera.main.transform.position.x != averageX || Camera.main.transform.position.z != averageZ)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
