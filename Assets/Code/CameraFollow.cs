using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject Player;
    public Vector3 Offset1, Offset2;


    void Start()
    {

        Offset1 = new Vector3(2, 1, -10);
        Player = GameObject.Find("Player");

    }


    void LateUpdate()
    {

        if (Player != null)
        {
            //gameObject.transform.position = Player.transform.position + Offset1;

            gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, Player.transform.position + Offset1, Time.deltaTime * 3f);

        }


    }
}
