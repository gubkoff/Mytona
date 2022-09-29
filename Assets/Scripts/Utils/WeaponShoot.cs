using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShoot : MonoBehaviour
{
    public void WeaponAttack() {
        EventBus.Pub(EventBus.MOB_SHOOT);
    }

    public void WeaponEndAttack() {
        Debug.Log("EEEEEEE");
    }
}
