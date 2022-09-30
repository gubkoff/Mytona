using Mytona.Ammo;
using UnityEngine;
public enum WeaponType {
	None,
	Rifle,
	Shotgun,
	AutomaticRifle,
	Pistol
}

namespace Mytona.PlayerCharacter.Weapons {
	public abstract class PlayerWeapon : MonoBehaviour {
		protected abstract WeaponType Type { get; }

		[SerializeField] protected GameObject model;
		[SerializeField] protected Projectile bulletPrefab;
		[SerializeField] protected float reload = 1f;
		[SerializeField] protected Transform firePoint;
		[SerializeField] protected ParticleSystem vfx;
		
		protected float lastTime;

		protected Player player;
		protected PlayerAnimator playerAnimator;

		protected virtual void Awake() {
			player = GetComponent<Player>();
			playerAnimator = GetComponent<PlayerAnimator>();
			player.OnWeaponChange += Change;
		}

		protected virtual void OnDestroy() {
			EventBus<PlayerInputMessage>.Unsub(Fire);
		}

		protected virtual float GetDamage() {
			return player.GetDamage();
		}

		private void Change(WeaponType type) {
			EventBus<PlayerInputMessage>.Unsub(Fire);
			if (type == Type) {
				EventBus<PlayerInputMessage>.Sub(Fire);
			}

			model.SetActive(type == Type);
		}

		protected abstract void Fire(PlayerInputMessage message);
	}
}