using System.Threading.Tasks;
using UnityEngine;

namespace Mytona.PlayerCharacter.Weapons {
	public class AutomaticRifle : PlayerWeapon {
		protected override WeaponType Type => WeaponType.AutomaticRifle;

		protected override void Awake() {
			base.Awake();
			lastTime = Time.time - reload;
		}

		protected override float GetDamage() {
			return player.GetDamage() / 5f;
		}

		protected override async void Fire(PlayerInputMessage message) {
			if (Time.time - reload < lastTime) {
				return;
			}

			if (!message.Fire) {
				return;
			}

			lastTime = Time.time;
			playerAnimator.TriggerShoot();

			await Task.Delay(16);

			var bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
			bullet.Damage = GetDamage();
			vfx.Play();
		}
	}
}