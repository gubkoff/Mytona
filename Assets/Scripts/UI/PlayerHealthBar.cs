using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
	[SerializeField] private GameObject Bar;
	[SerializeField] private SpriteRenderer BarImg;
	[SerializeField] private TMP_Text Text;
	[SerializeField] private TMP_Text DamageText;
	[SerializeField] private TMP_Text HealthChangeText;
	[SerializeField] private float HealthChangeTextShowTime = 1;
	private Player player;
	private void Awake()
	{
		player = GetComponent<Player>();
		player.OnHPChange += OnHPChange;
		OnHPChange(player.MaxHealth, 0);
		HealthChangeText.gameObject.SetActive(false);
	}

	public void OnDeath()
	{
		Bar.SetActive(false);
	}
    
	private void LateUpdate()
	{
		Bar.transform.rotation = Camera.main.transform.rotation;
	}

	private void OnHPChange(float health, float diff)
	{
		var frac = health / player.MaxHealth;
		Text.text = $"{health:####}/{player.MaxHealth:####}";
		BarImg.size = new Vector2(frac, BarImg.size.y);
		var pos = BarImg.transform.localPosition;
		pos.x = -(1 - frac) / 2;
		BarImg.transform.localPosition = pos;
		if (health <= 0)
		{
			Bar.SetActive(false);
		}
		StartCoroutine(ShowHealthChange(diff));
	}

	private IEnumerator ShowHealthChange(float diff) {
		if(diff == 0)
			yield break;
		HealthChangeText.gameObject.SetActive(true);
		HealthChangeText.text = HealthDiff(diff);
		yield return new WaitForSeconds(HealthChangeTextShowTime);
		HealthChangeText.gameObject.SetActive(false);
	}

	private string HealthDiff(float diff) {
		return diff > 0 ? $"+{diff}" : diff.ToString();
	}

	private void OnUpgrade()
	{
		DamageText.text = $"{player.Damage}";
	}
}