using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mytona.PlayerCharacter {
    public class PlayerInput : MonoBehaviour {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Player player;

        private void Awake() {
            EventBus.Sub(PlayerDeadHandler, EventBus.PLAYER_DEATH);
        }

        private void OnDestroy() {
            EventBus.Unsub(PlayerDeadHandler, EventBus.PLAYER_DEATH);
        }

        private void PlayerDeadHandler() {
            enabled = false;
        }

        void Update() {
            var moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            var plane = new Plane(Vector3.up, Vector3.up * player.transform.position.y);
            plane.Raycast(ray, out var enter);
            var aimPos = ray.GetPoint(enter);
            var aimInput = aimPos - player.transform.position;


            var fire = Input.GetKey(KeyCode.Mouse0);
            EventBus<PlayerInputMessage>.Pub(new PlayerInputMessage() {
                MovementDirection = moveInput.normalized,
                AimDirection = new Vector2(aimInput.x, aimInput.z).normalized,
                Fire = fire
            });
        }
    }
}