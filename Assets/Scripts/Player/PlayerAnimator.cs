using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
	[SerializeField] private Animator Animator;
	
	private void Awake()
	{
		EventBus<PlayerInputMessage>.Sub(AnimatePlayer);
		EventBus.Sub(AnimateDeath,EventBus.PLAYER_DEATH);
	}

	private void AnimatePlayer(PlayerInputMessage message) {
		Animator.SetBool("IsRun",message.MovementDirection.sqrMagnitude > 0);
	}

	private void OnDestroy()
	{
		EventBus<PlayerInputMessage>.Unsub(AnimatePlayer);
		EventBus.Unsub(AnimateDeath,EventBus.PLAYER_DEATH);
	}

	private void AnimateDeath()
	{
		Animator.SetTrigger("Death");
	}

	public void TriggerShoot()
	{
		Animator.SetTrigger("Shoot");
	}
}