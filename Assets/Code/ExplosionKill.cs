using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionKill : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Kill", 2);
	}
	
	void Kill()
    {
        Destroy(gameObject);
    }
}
