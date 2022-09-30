using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mytona.Collectables;
using Mytona.PlayerCharacter;
using UnityEngine;
using Random = UnityEngine.Random;

//Bad class
namespace Mytona.Systems {
	public class PowerupSpawner : MonoBehaviour {
		private const float WEAPON_CHANGE_PROBABILITY_MAX = 100;
		[Range(0, 100)] [SerializeField] private float weaponChangeProbability = 10;
		
		[SerializeField] private List<SpawnItem> spawnItems;

		private Dictionary<int, int> chance;

		private GameObject[] prefabs;
		private WeaponPowerUp[] weaponPrefabs;

		private List<GameObject> prefabList;

		private void Awake() {
			chance = GetChance(spawnItems);
			EventBus.Sub(Handle, EventBus.MOB_KILLED);
		}
		
		private Dictionary<int, int> GetChance(List<SpawnItem> probabilities) {
			var result = new Dictionary<int, int>();
			if (probabilities != null && probabilities.Count > 0) {
				int tempSum = 0;
				for (int i = 0; i < probabilities.Count; i++) {
					tempSum += probabilities[i].Probability;
					result[i] = tempSum;
				}
			}
			return result;
		}
		
		private int PowerUp() {
			var newPowerUp = Random.Range(1, chance.Values.Max());
			foreach (var item in chance.OrderBy(i => i.Key)) {
				if (newPowerUp <= item.Value) {
					return item.Key;
				}
			}
			return -1;
		}

		private bool IsCorrectPowerUp(int powerUpIndex) {
			if (!IsWeaponChange() && spawnItems[powerUpIndex].IsWeapon) {
				return false;
			}

			if (spawnItems[powerUpIndex].WeaponType == Player.Instance.GetCurrentWeapon()) {
				return false;
			}

			return true;
		}

		private bool IsWeaponChange() {
			if (weaponChangeProbability > WEAPON_CHANGE_PROBABILITY_MAX) {
				weaponChangeProbability = WEAPON_CHANGE_PROBABILITY_MAX;
			}
			var weaponChange = Random.Range(1, chance.Values.Max());
			if (weaponChange <= weaponChangeProbability) {
				return true;
			}

			return false;
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
			var powerUpIndex = PowerUp();
			while (!IsCorrectPowerUp(powerUpIndex)) {
				powerUpIndex = PowerUp();
			}

			Instantiate(spawnItems[powerUpIndex].Prefab, position, Quaternion.identity);
		}
	}
}