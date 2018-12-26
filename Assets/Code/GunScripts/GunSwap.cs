using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwap : MonoBehaviour {

    [SerializeField] private PrimaryWeapon primary;
    [SerializeField] private SecondaryWeapon secondary;
    [SerializeField] private GameObject weaponHolder;

    // Use this for initialization
    void Start () {
        Initialize(primary, secondary, weaponHolder);
	}

    public void Initialize(PrimaryWeapon pw, SecondaryWeapon sw, GameObject weapon)
    {
        pw.Initialize(weapon);
        sw.Initialize(weapon);
    }
}
