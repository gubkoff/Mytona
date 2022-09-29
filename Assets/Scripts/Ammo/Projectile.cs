using Mytona.MobCharacter;
using Mytona.PlayerCharacter;
using UnityEngine;

namespace Mytona.Ammo {
	public class Projectile : MonoBehaviour {
		[SerializeField] protected float damage;
		[SerializeField] private float speed = 8;
		[SerializeField] protected bool damagePlayer = false;
		[SerializeField] protected bool damageMob;
		[SerializeField] private float timeToLive = 5f;
		private float timer = 0f;
		protected bool destroyed = false;

		public float Damage
		{
			get => damage;
			set => damage = value;
		}

		protected virtual void OnTriggerEnter(Collider other) {
			if (destroyed) {
				return;
			}

			if (damagePlayer && other.CompareTag("Player")) {
				other.GetComponent<Player>().TakeDamage(damage);
				destroyed = true;
			}

			if (damageMob && other.CompareTag("Mob")) {
				var mob = other.GetComponent<Mob>();
				mob.TakeDamage(damage);
				destroyed = true;
			}
		}

		protected virtual void Update() {
			if (!destroyed) {
				transform.position += transform.forward * speed * Time.deltaTime;
			}

			timer += Time.deltaTime;
			if (timer > timeToLive) {
				Destroy(gameObject);
			}
		}
	}
}