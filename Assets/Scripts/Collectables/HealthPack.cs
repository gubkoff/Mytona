using Mytona.PlayerCharacter;
using UnityEngine;

namespace Mytona.Collectables {
	public class HealthPack : MonoBehaviour
	{
		[SerializeField] private int health;
		private void OnTriggerEnter(Collider other)
		{
			if (other.CompareTag("Player"))
			{
				other.GetComponent<Player>().Heal(health);
				Destroy(gameObject);
			}
		}
	}
}
