using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    /// <summary>
    /// The game context.
    /// A pointer to the currently active game (so that we don't have to use something slow like "Find").
    /// </summary>
    public static Game Ctx;


    public static BulletManager Bullets;
    public static Player overallPlayer;

    internal void Start()
    {
        Ctx = this;

        Bullets = new BulletManager(GameObject.Find("Bullets").transform);
        overallPlayer = FindObjectOfType<Player>();
    }
}
