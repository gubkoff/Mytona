using System.Collections;
using UnityEngine;

namespace Mytona.Utils {
    public class ExplosionLifetime : MonoBehaviour {
        [SerializeField] private float lifetime;

        private void OnEnable() {
            StartCoroutine(WaitAndDestroy());
        }

        private IEnumerator WaitAndDestroy() {
            yield return new WaitForSeconds(lifetime);
            Destroy(gameObject);
        }
    }
}
