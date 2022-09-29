using System.Collections;
using UnityEngine;

public class ExplosionLifetime : MonoBehaviour {
    [SerializeField] private float Lifetime;

    private void OnEnable() {
        StartCoroutine(WaitAndDestroy());
    }

    private IEnumerator WaitAndDestroy() {
        yield return new WaitForSeconds(Lifetime);
        Destroy(gameObject);
    }
}
