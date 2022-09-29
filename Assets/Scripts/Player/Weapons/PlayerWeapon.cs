using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
	protected const int Rifle = 0;
	protected const int Shotgun = 1;
	protected const int AutomaticRifle = 2;
	protected const int Pistol = 3;
	protected abstract int Type { get; }
	
	[SerializeField] protected Projectile BulletPrefab;
	[SerializeField] protected float Reload = 1f;
	[SerializeField] protected Transform FirePoint;
	[SerializeField] protected ParticleSystem VFX;
	[SerializeField] protected GameObject Model;

	protected float lastTime;
	
	protected Player player;
	protected PlayerAnimator playerAnimator;

	protected virtual void Awake() {
		player = GetComponent<Player>();
		playerAnimator = GetComponent<PlayerAnimator>();
		player.OnWeaponChange += Change;
	}

	protected virtual void OnDestroy()
	{
		EventBus<PlayerInputMessage>.Unsub(Fire);
	}
	
	protected virtual float GetDamage()
	{
		return player.GetDamage();
	}

	private void Change(int type)
	{
		EventBus<PlayerInputMessage>.Unsub(Fire);
		if (type == Type)
		{
			EventBus<PlayerInputMessage>.Sub(Fire);
		}
		Model.SetActive(type == Type);
	}

	protected abstract void Fire(PlayerInputMessage message);
}