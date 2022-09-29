using Mytona.MobCharacter;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Mytona.Systems {
    public class MobSpawner : Handler<SpawnMobMessage> {
        [SerializeField] private Mob[] prefabs;

        protected override void Awake() {
            base.Awake();
            EventBus.Sub(() => { EventBus<SpawnMobMessage>.Unsub(HandleMessage); }, EventBus.PLAYER_DEATH);
        }

        public override void HandleMessage(SpawnMobMessage message) {
            var position = new Vector3(Random.value * 11 - 6, 1, Random.value * 11 - 6);
            Instantiate(prefabs[message.Type], position, Quaternion.identity);
        }
    }
}