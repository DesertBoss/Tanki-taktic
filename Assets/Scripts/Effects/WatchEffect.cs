using UnityEngine;

public class WatchEffect : Effect {
	private float myInitTime = 10;
	private float myRemainingTime;

	public WatchEffect () : base (BonusType.Watch) {
		myRemainingTime = myInitTime;
	}
	public WatchEffect (float duration) : this () {
		myRemainingTime = duration;
	}

	public override void Start () {
		InitContainer.instance.SpawnController.FreezeEnemyes ();
	}

	public override void Update (float delta) {
		myRemainingTime -= delta;

		if (myRemainingTime <= 0) myCompleted = true;
	}

	public override void OnDestroy () {
		InitContainer.instance.SpawnController.UnfreezeEnemyes ();
	}
}

