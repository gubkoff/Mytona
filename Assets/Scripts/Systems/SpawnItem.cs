using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mytona.Systems {
    [CreateAssetMenu(menuName = "Data/SpawnItem")]
    public class SpawnItem : ScriptableObject {
        [Range(0, 100)] [SerializeField] private int probability;
        [SerializeField] private GameObject prefab;
        [SerializeField] private bool isWeapon;
        [SerializeField] private WeaponType weaponType;

        public int Probability => probability;
        public GameObject Prefab => prefab;
        public bool IsWeapon => isWeapon;
        public WeaponType WeaponType => weaponType;
    }
}
