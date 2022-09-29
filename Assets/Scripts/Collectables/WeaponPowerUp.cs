﻿using Mytona.PlayerCharacter;
using UnityEngine;

namespace Mytona.Collectables {
	public class WeaponPowerUp : MonoBehaviour {
		[SerializeField] private int type;

		private void OnTriggerEnter(Collider other) {
			if (other.CompareTag("Player")) {
				other.GetComponent<Player>().ChangeWeapon(type);
				Destroy(gameObject);
			}
		}
	}
}