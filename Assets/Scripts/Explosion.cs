using UnityEngine;

public class Explosion : StateMachineBehaviour
{
	public override void OnStateMachineExit (Animator animator, int stateMachinePathHash) {
		GameObject.Destroy (animator.gameObject);
	}
}
