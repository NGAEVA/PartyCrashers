using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    public Vector3 rotation = new Vector3(45, 0, 0);
    public int height = 10;
    public int distanceOffset = 10;

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


        float xDistance = -1;
        float zDistance = -1;

        float x1 = 0;
        float z1 = 0;
        float x2 = 0;
        float z2 = 0;

        //Loop through players and set x to greatest x distance, and y to greatest y distance between current player and any other player
        for (int i = 0; i < players.Length; i++)
        {
            for (int j = i; j < players.Length; j++)
            {
                if (Mathf.Abs(players[i].transform.position.x - players[j].transform.position.x) > xDistance) // if the distance is greater than current
                {
                    xDistance = Mathf.Abs(players[i].transform.position.x - players[j].transform.position.x);
                    x1 = players[i].transform.position.x; // update x to the new greatest distance
                    x2 = players[j].transform.position.x; // set this gamobject to the other player that this player has the greatest distance with
                }

                if (Mathf.Abs(players[i].transform.position.z - players[j].transform.position.z) > zDistance)
                {
                    zDistance = Mathf.Abs(players[i].transform.position.z - players[j].transform.position.z);
                    z1 = players[i].transform.position.z;
                    z2 = players[j].transform.position.z;
                }
            }
        }

        float averageX = (x1 + x2) / 2;
        float averageZ = (z1 + z2) / 2;
        gameObject.transform.position = new Vector3(averageX, transform.position.y, averageZ - distanceOffset);

    }
}
