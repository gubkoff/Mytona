using System.Threading.Tasks;
using UnityEngine;

public class Pistol : PlayerWeapon
{
	protected override int Type => Pistol;

	protected override void Awake()
	{
		base.Awake();
		EventBus<PlayerInputMessage>.Sub(Fire);
		lastTime = Time.time - Reload;
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