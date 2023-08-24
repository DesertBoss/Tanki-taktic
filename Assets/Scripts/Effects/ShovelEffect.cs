public class ShovelEffect : TimedEffect {
	public ShovelEffect () : base (BonusType.Shovel) {
		_startTime = 20;
	}
	public ShovelEffect (float duration) : this () {
		_remainingTime = duration;
	}

	public override void Start () {
		Glabal.MainBase.Fortify ();
	}

	public override void End () {
		Glabal.MainBase.UnFortify ();
	}
}