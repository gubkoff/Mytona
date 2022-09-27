using UnityEngine;

public abstract class PlayerWeapon : MonoBehaviour
{
	protected const int Rifle = 0;
	protected const int Shotgun = 1;
	protected const int AutomaticRifle = 2;
	protected const int Pistol = 3;
	protected abstract int Type { get; }
	
	public Projectile BulletPrefab;
	public float Reload = 1f;
	public Transform FirePoint;
	public ParticleSystem VFX;
	
	
	protected float lastTime;
	
	public GameObject Model;

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
		return player.Damage;
	}

	protected void Change(int type)
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