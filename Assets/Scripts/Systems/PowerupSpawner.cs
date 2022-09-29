using Mytona.Collectables;
using UnityEngine;

//Bad class
namespace Mytona.Systems {
	public class PowerupSpawner : MonoBehaviour {
		[Range(0, 100)] [SerializeField] private float healthUpgradeWeight = 10;
		[Range(0, 100)] [SerializeField] private float damageUpgradeWeight = 10;
		[Range(0, 100)] [SerializeField] private float moveSpeedUpgradeWeight = 5;
		[Range(0, 100)] [SerializeField] private float healWeight = 25;
		[Range(0, 100)] [SerializeField] private float weaponChangeWeight = 2;
		[Range(0, 100)] [SerializeField] private float rifleWeight = 25;
		[Range(0, 100)] [SerializeField] private float automaticRifleWeight = 15;
		[Range(0, 100)] [SerializeField] private float shotgunWeight = 20;

		[SerializeField] private PowerUp healthPrefab;
		[SerializeField] private PowerUp damagePowerUp;
		[SerializeField] private PowerUp moveSpeedPrefab;
		[SerializeField] private HealthPack healPrefab;
		[SerializeField] private WeaponPowerUp riflePrefab;
		[SerializeField] private WeaponPowerUp automaticRifleWPrefab;
		[SerializeField] private WeaponPowerUp shotgunPrefab;

		private float[] weights;
		private float[] weaponWeights;

		private GameObject[] prefabs;
		private WeaponPowerUp[] weaponPrefabs;

		private void Awake() {
			weights = new float[5];
			weights[0] = healthUpgradeWeight;
			weights[1] = weights[0] + damageUpgradeWeight;
			weights[2] = weights[1] + moveSpeedUpgradeWeight;
			weights[3] = weights[2] + healWeight;
			weights[4] = weights[3] + weaponChangeWeight;

			weaponWeights = new float[3];
			weaponWeights[0] = rifleWeight;
			weaponWeights[1] = weaponWeights[0] + automaticRifleWeight;
			weaponWeights[2] = weaponWeights[1] + shotgunWeight;

			prefabs = new[] {
				healthPrefab.gameObject,
				damagePowerUp.gameObject,
				moveSpeedPrefab.gameObject,
				healPrefab.gameObject
			};
			weaponPrefabs = new[] {
				riflePrefab,
				automaticRifleWPrefab,
				shotgunPrefab
			};

			EventBus.Sub(Handle, EventBus.MOB_KILLED);
		}

		private void Handle() {
			Spawn(PickRandomPosition());
		}

		private Vector3 PickRandomPosition() {
			var vector3 = new Vector3();
			vector3.x = Random.value * 11 - 6;
			vector3.z = Random.value * 11 - 6;
			return vector3;
		}


		private void Spawn(Vector3 position) {
			var rand = Random.value * weights[4];
			int i = 0;
			while (i < 5 && weights[i] >= rand) {
				i++;
			}

			if (i < 4) {
				Instantiate(prefabs[Mathf.Min(3, i)], position, Quaternion.identity);
			}
			else {
				rand = Random.value * weaponWeights[2];
				i = 0;
				while (i < 3 && weaponWeights[Mathf.Min(2, i)] >= rand) {
					i++;
				}

				Instantiate(weaponPrefabs[Mathf.Min(2, i)], position, Quaternion.identity);
			}
		}
	}
}