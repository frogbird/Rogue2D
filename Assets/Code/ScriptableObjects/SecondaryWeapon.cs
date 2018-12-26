using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapon/SecondaryWeapon")]
public class SecondaryWeapon : Weapon {

    public Object projectile;

    private Gun launcher;

    public override void Initialize(GameObject obj)
    {
        launcher = obj.GetComponent<SecondaryGun>();
        launcher.projectile = projectile;
        launcher.FireCooldown = BaseCoolDown;
        launcher.fireSpeed = fireSpeed;
        launcher.baseDamage = baseDamage;
    }

    public override void TriggerAbility(int intensity, Vector3 aimPos)
    {
        launcher.Fire(intensity, aimPos);
    }
}
