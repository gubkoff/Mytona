using System.Collections;
using Mytona.PlayerCharacter;
using TMPro;
using UnityEngine;

namespace Mytona.UI {
	public class PlayerHealthBar : MonoBehaviour {
		[SerializeField] private GameObject bar;
		[SerializeField] private SpriteRenderer barImg;
		[SerializeField] private TMP_Text text;
		[SerializeField] private TMP_Text damageText;
		[SerializeField] private TMP_Text healthChangeText;
		[SerializeField] private float healthChangeTextShowTime = 1;
		private Player player;

		private void Awake() {
			player = GetComponent<Player>();
			player.OnHPChange += OnHPChange;
			OnHPChange(player.GetMaxHealth(), 0);
			healthChangeText.gameObject.SetActive(false);
		}

		public void OnDeath() {
			bar.SetActive(false);
		}

		private void LateUpdate() {
			bar.transform.rotation = Camera.main.transform.rotation;
		}

		private void OnHPChange(float health, float diff) {
			var frac = health / player.GetMaxHealth();
			text.text = $"{health:####}/{player.GetMaxHealth():####}";
			barImg.size = new Vector2(frac, barImg.size.y);
			var pos = barImg.transform.localPosition;
			pos.x = -(1 - frac) / 2;
			barImg.transform.localPosition = pos;
			if (health <= 0) {
				bar.SetActive(false);
			}

			StartCoroutine(ShowHealthChange(diff));
		}

		private IEnumerator ShowHealthChange(float diff) {
			if (diff == 0)
				yield break;
			healthChangeText.gameObject.SetActive(true);
			healthChangeText.text = HealthDiff(diff);
			yield return new WaitForSeconds(healthChangeTextShowTime);
			healthChangeText.gameObject.SetActive(false);
		}

		private string HealthDiff(float diff) {
			return diff > 0 ? $"+{diff}" : diff.ToString();
		}

		private void OnUpgrade() {
			damageText.text = $"{player.GetDamage()}";
		}
	}
}