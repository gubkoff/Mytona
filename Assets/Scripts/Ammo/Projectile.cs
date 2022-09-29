using Mytona.MobCharacter;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	[SerializeField] protected float Damage;
	[SerializeField] private float Speed = 8;
	[SerializeField] protected bool DamagePlayer = false;
	[SerializeField] protected bool DamageMob;
	[SerializeField] private float TimeToLive = 5f;
	[SerializeField] private float timer = 0f;
	[SerializeField] protected bool destroyed = false;

	public void SetDamage(float damage) {
		Damage = damage;
	}

	protected virtual void OnTriggerEnter(Collider other)
	{
		if (destroyed)
		{
			return;
		}
		if (DamagePlayer && other.CompareTag("Player"))
		{
			other.GetComponent<Player>().TakeDamage(Damage);
			destroyed = true;
		}
		
		if (DamageMob && other.CompareTag("Mob"))
		{
			var mob = other.GetComponent<Mob>();
			mob.TakeDamage(Damage);
			destroyed = true;
		}
	}

	protected virtual void Update()
	{
		if (!destroyed)
		{
			transform.position += transform.forward * Speed * Time.deltaTime;
		}
		timer += Time.deltaTime;
		if (timer > TimeToLive)
		{
			Destroy(gameObject);
		}
	}
}