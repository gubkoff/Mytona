using Mytona.MobCharacter;
using TMPro;
using UnityEngine;

namespace Mytona.UI {
    public class HealthBar : MonoBehaviour, IMobComponent {
        [SerializeField] private GameObject bar;
        [SerializeField] private SpriteRenderer barImg;
        [SerializeField] private TMP_Text text;
        private float maxHP;

        private void Awake() {
            var mob = GetComponent<Mob>();
            maxHP = mob.MaxHealth;
            OnHPChange(mob.MaxHealth, 0);
            mob.OnHPChange += OnHPChange;
        }

        public void OnDeath() {
            bar.SetActive(false);
        }

        private void LateUpdate() {
            bar.transform.rotation = Camera.main.transform.rotation;
        }

        private void OnHPChange(float health, float diff) {
            var frac = health / maxHP;
            text.text = $"{health:####}/{maxHP:####}";
            barImg.size = new Vector2(frac, barImg.size.y);
            var pos = barImg.transform.localPosition;
            pos.x = -(1 - frac) / 2;
            barImg.transform.localPosition = pos;
        }
    }
}