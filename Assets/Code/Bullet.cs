using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Damager
{

    public const float Lifetime = 2f; // bullets last this long
    private float _deathtime;
    public GameObject explosionPrefab;
    public Transform shooter;


    public void Initialize(Vector2 velocity, float deathtime)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        _deathtime = deathtime;
    }

    public void Initialize(Vector2 velocity, float deathtime, float bulletIntensity, Transform _shooter)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        _deathtime = deathtime;
        intensity = (int) bulletIntensity;
        shooter = _shooter;

        //print(intensity);
    }


    internal void Update()
    {
        if (Time.time > _deathtime) { Die(); }
    }



    internal void OnCollisionEnter2D(Collision2D other)
    {
        /*
        if (other.gameObject.GetComponent<Bullet>() == null)
        {
            Die();
        }
        */

        if (other.gameObject.transform != shooter)
        {
            Die();
        }

    }


    public void Die()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


}

