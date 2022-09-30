using System.Threading.Tasks;
using UnityEngine;

namespace Mytona.PlayerCharacter.Weapons {
	public class Shotgun : PlayerWeapon {
		protected override WeaponType Type => WeaponType.Shotgun;

		protected override void Awake() {
			base.Awake();
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
			var directions = SpreadDirections(transform.rotation.eulerAngles, 3, 20);
			foreach (var direction in directions) {
				var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(direction));
				bullet.Damage = GetDamage();
			}
			vfx.Play();
		}

		private Vector3[] SpreadDirections(Vector3 direction, int num, int spreadAngle) {
			Vector3[] result = new Vector3[num];
			result[0] = new Vector3(0, direction.y - (num - 1) * spreadAngle / 2, 0);
			for (int i = 1; i < num; i++) {
				result[i] = result[i - 1] + new Vector3(0, spreadAngle, 0);
			}

			return result;
		}
	}
}