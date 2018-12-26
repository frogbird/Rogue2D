using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : ScriptableObject {

    public string gName = "New Gun";
    //public Sprite aSprite;
    //public AudioClip aSound;
    public float BaseCoolDown;
    public float fireSpeed;
    public float baseDamage;

    public abstract void Initialize(GameObject obj);
    public abstract void TriggerAbility(int intensity, Vector3 aimPos);
}
