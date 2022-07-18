using UnityEngine;

public class GrenadeEffect : Effect {
	public GrenadeEffect () : base (BonusType.Grenade) {
		
	}

	public override void Start () {
		InitContainer.instance.SpawnController.KillAllEnemyes ();
		myCompleted = true;
	}
}