using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damageable : MonoBehaviour {

    [Tooltip("The maximum health for this GameObject")]
    public int maxHealth;
    public int maxShield;

    public Slider healthBar;

    private int health;
    private int shield;

    public GameObject explosionPrefab;

    public Rigidbody2D rb2d;


    // Use this for initialization
    void Start () {
        health = maxHealth;
        shield = maxShield;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
        }

        rb2d = GetComponent<Rigidbody2D>();


	}
	
	// Update is called once per frame
	void Update () {

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Damager other = collision.gameObject.GetComponent<Damager>();

        if (other != null)
        {

            Bullet bul = collision.gameObject.GetComponent<Bullet>();

            if (bul != null)
            {
                if (transform == bul.shooter)
                {
                    return;
                }
            }


            print(other.intensity);
            health -= other.intensity;

            if (healthBar != null)
            {
                healthBar.value = health;
            }
            
            if (health <= 0)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }

        Melee maylay = collision.gameObject.GetComponent<Melee>();

        if (maylay != null && rb2d != null & !Game.overallPlayer.knockback)
        {

            Vector2 direction = (transform.position - maylay.gameObject.transform.position).normalized;

            StartCoroutine(Game.overallPlayer.Knockback(direction, other.intensity));
            
        }
    }


    public void IncreaseMaxHealth(int addedHealth)
    {
        maxHealth += addedHealth;
        //health += addedHealth;

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            //healthBar.value = health;
        }

    }

    public void RefillHealth(int healedAmount)
    {
        health += healedAmount;

        if (healthBar != null)
        {
            healthBar.value = health;
        }
    }

}
