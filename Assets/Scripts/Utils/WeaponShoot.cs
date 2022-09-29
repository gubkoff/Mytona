using System.Collections;
using System.Collections.Generic;
using Mytona.MobCharacter;
using UnityEngine;

public class WeaponShoot : MonoBehaviour {
    [SerializeField] private MobAttack mobAttack;
    public void WeaponAttack() {
        mobAttack.Shoot();
    }

    public void WeaponEndAttack() {
    }
}
