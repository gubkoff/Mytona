using System.Threading.Tasks;
using UnityEngine;

public class AutomaticRifle : PlayerWeapon
{
	protected override int Type => AutomaticRifle;
	
	protected override void Awake()
	{
		base.Awake();
		lastTime = Time.time - Reload;
	}

	protected override float GetDamage()
	{
		return player.GetDamage() / 5f;
	}

	protected override async void Fire(PlayerInputMessage message)
	{
		if (Time.time - Reload < lastTime)
		{
			return;
		}

		if (!message.Fire)
		{
			return;
		}

		lastTime = Time.time;
		playerAnimator.TriggerShoot();

		await Task.Delay(16);

		var bullet = Instantiate(BulletPrefab, FirePoint.position, transform.rotation);
		bullet.SetDamage(GetDamage());
		VFX.Play();
	}
}