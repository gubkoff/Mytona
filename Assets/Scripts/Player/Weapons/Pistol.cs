using System.Threading.Tasks;
using UnityEngine;

namespace Mytona.PlayerCharacter.Weapons {
	public class Pistol : PlayerWeapon {
		protected override WeaponType Type => WeaponType.Pistol;

		protected override void Awake() {
			base.Awake();
			EventBus<PlayerInputMessage>.Sub(Fire);
			lastTime = Time.time - reload;
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