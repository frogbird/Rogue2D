using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {



    public Transform target;
    private Rigidbody2D rb2d;
    public float speed;
    public bool moving;

    // Use this for initialization
    void Start () {
        //target = FindObjectOfType<Player>().transform;
        speed = 10f;
        rb2d = GetComponent<Rigidbody2D>();
        target = Game.overallPlayer.transform;
        //moving = true;


	}

    // Update is called once per frame
    void Update()
    {
        if(target != null && moving)
        {
            float step = speed * Time.deltaTime;
            //rb2d.position = Vector3.MoveTowards(transform.position, target.position, step);

            if ((target.position - transform.position).magnitude > 1f && (target.position - transform.position).magnitude < 20f)
            {

                rb2d.position = Vector3.MoveTowards(transform.position, target.position, step);
            }


            //transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }

    }


}
