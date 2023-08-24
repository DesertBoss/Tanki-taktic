public class StarEffect : Effect {
	private PlayerTank _owner;
	private int _addScore = 300;	// Default

	public StarEffect (Tank owner) : base (BonusType.Star) {
		_owner = owner as PlayerTank;
	}

	public override void Start () {
		if (_owner != null) {
			_owner.OnTakeStar ();
			Glabal.PlayerService.AddScore (_owner.transform.position, _addScore);
		}

		Complete ();
	}

	public override void End () {
		_owner = null;
	}
}