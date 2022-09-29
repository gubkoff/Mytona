using Mytona.MobCharacter;
using TMPro;
using UnityEngine;

namespace Mytona.UI {
    public class HealthBar : MonoBehaviour, IMobComponent {
        [SerializeField] private GameObject Bar;
        [SerializeField] private SpriteRenderer BarImg;
        [SerializeField] private TMP_Text Text;
        private float maxHP;

        private void Awake() {
            var mob = GetComponent<Mob>();
            maxHP = mob.MaxHealth;
            OnHPChange(mob.MaxHealth, 0);
            mob.OnHPChange += OnHPChange;
        }

        public void OnDeath() {
            Bar.SetActive(false);
        }

        private void LateUpdate() {
            Bar.transform.rotation = Camera.main.transform.rotation;
        }

        private void OnHPChange(float health, float diff) {
            var frac = health / maxHP;
            Text.text = $"{health:####}/{maxHP:####}";
            BarImg.size = new Vector2(frac, BarImg.size.y);
            var pos = BarImg.transform.localPosition;
            pos.x = -(1 - frac) / 2;
            BarImg.transform.localPosition = pos;
        }
    }
}