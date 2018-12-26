using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public float FireCooldown;
    private float _lastfire;
    private Player p;
    public float fireSpeed;

    public float baseDamage;

    private Quaternion spriteRotation;
    //private SpriteRenderer sprite;

    //private LayerMask gLayer;

    public Object projectile;





    public void Start()
    {
        //p = Game.overallPlayer;
        //sprite = p.GetComponent<SpriteRenderer>();
        //gLayer = Game.overallPlayer.groundLayer;

    }



    public void ForceSpawnWithIntensity(Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime, float intensity, Transform shooter)
    {

        var newBullet = (GameObject)Object.Instantiate(projectile, pos, rotation);

        Bullet temp = newBullet.GetComponent<Bullet>();

        temp.Initialize(velocity, deathtime, intensity, shooter);
    }




    public void Fire(float multiplier, Vector3 aimPos)
    {
        float angle = Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Rad2Deg - 90f;
        spriteRotation = Quaternion.Euler(0, 0, angle);
        aimPos.z = 0;
        aimPos = aimPos.normalized;

        float time = Time.time;
        if (time < _lastfire + FireCooldown) { return; }

        _lastfire = time;

        ForceSpawnWithIntensity(
                transform.position + aimPos,
                spriteRotation,
                aimPos * fireSpeed,
                time + Bullet.Lifetime,
                baseDamage * multiplier,
                transform);
    }

    /*
    public void BombDrop(int intensity, Vector3 aimPos)
    {
        float angle = Mathf.Atan2(aimPos.y, aimPos.x) * Mathf.Rad2Deg - 90f;
        spriteRotation = Quaternion.Euler(0, 0, angle);
        aimPos.z = 0;
        aimPos = aimPos.normalized;

        float time = Time.time;
        if (time < _lastfire + FireCooldown * 2) { return; }

        _lastfire = time;

        Game.Bullets.ForceSpawnBomb(
                transform.position + aimPos,
                spriteRotation,
                aimPos * fireSpeed,
                time + Bullet.Lifetime,
                intensity,
                transform);
    }
    */

}

