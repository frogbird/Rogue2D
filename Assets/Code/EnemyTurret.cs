using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour {


    public Transform target;
    public Gun _gun;

    public int baseDamage;

    public bool homingTurret;
    public bool fixedDirection;
    public Vector2 directionalShooting;

    public float damageMultiplier = 1.0f;


    // Use this for initialization
    void Start () {
        _gun = GetComponent<Gun>();
        target = Game.overallPlayer.transform;
	}
	
	// Update is called once per frame
	void Update () {

        if(target != null && homingTurret)
        {
            if ((target.position - transform.position).magnitude < 10f)
            {
                _gun.Fire(baseDamage, target.position - transform.position);
            }
        }

        if (target != null && fixedDirection)
        {
            _gun.Fire(baseDamage, directionalShooting);
        }

    }
}
