public class PistolEffect : TimedEffect {
	private PlayerTank _owner;
	private BulletPower _initPower;

	public PistolEffect (Tank owner) : base (BonusType.Star) {
		_owner = owner as PlayerTank;
	}

	public PistolEffect (Tank owner, float duration) : this (owner) {
		_remainingTime = duration;
	}

	public override void Start () {
		if (_owner == null)
			return;

		_initPower = _owner.GunPower;
		_owner.GunPower = BulletPower.Forced;
	}

	public override void End () {
		if (_owner == null)
			return;

		_owner.GunPower = _initPower;
		_owner = null;
	}
}