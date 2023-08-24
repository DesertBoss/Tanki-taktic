public class WatchEffect : TimedEffect {
	public WatchEffect () : base (BonusType.Watch) {

	}
	public WatchEffect (float duration) : this () {
		_remainingTime = duration;
	}

	public override void Start () {
		Glabal.SpawnController.FreezeEnemyes ();
	}

	public override void End () {
		Glabal.SpawnController.UnfreezeEnemyes ();
	}
}

