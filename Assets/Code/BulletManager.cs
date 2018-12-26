using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager
{
    private readonly Transform _holder;

    /// <summary>
    /// Bullet prefab. Use GameObject.Instantiate with this to make a new bullet.
    /// </summary>
    private readonly Object _bullet;
    private readonly Object _bomb;


    public BulletManager(Transform holder)
    {
        _holder = holder;
        _bullet = Resources.Load("Bullet");
        _bomb = Resources.Load("Bomb");
    }

    /*
    public void ForceSpawn(Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime, Transform shooter)
    {

        var newBullet = (GameObject)Object.Instantiate(_bullet, pos, rotation, _holder);

        Bullet temp = newBullet.GetComponent<Bullet>();

        temp.Initialize(velocity, deathtime, shooter);
    }
    */


    public void ForceSpawnWithIntensity(Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime, int intensity, Transform shooter)
    {

        var newBullet = (GameObject)Object.Instantiate(_bullet, pos, rotation, _holder);

        Bullet temp = newBullet.GetComponent<Bullet>();

        temp.Initialize(velocity, deathtime, intensity, shooter);
    }


    /*
    public void ForceSpawnBomb(Vector2 pos, Quaternion rotation, Vector2 velocity, float deathtime, int intensity, Transform shooter)
    {

        var newBullet = (GameObject)Object.Instantiate(_bomb, pos, rotation, _holder);

        Bullet temp = newBullet.GetComponent<Bullet>();

        temp.Initialize(velocity, deathtime, intensity, shooter);
    }
    */


}
