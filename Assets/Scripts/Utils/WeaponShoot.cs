using Mytona.MobCharacter;
using UnityEngine;

namespace Mytona.Utils {
    public class WeaponShoot : MonoBehaviour {
        [SerializeField] private MobAttack mobAttack;

        public void WeaponAttack() {
            mobAttack.Shoot();
        }

        public void WeaponEndAttack() { }
    }
}
