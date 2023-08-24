public abstract class Effect {
	protected BonusType _bonusType = BonusType.None;
	private bool _completed = false;

	public BonusType BonusType => _bonusType;
	public bool Completed => _completed;

	protected Effect (BonusType type) {
		_bonusType = type;
	}

	public virtual void Start () {

	}

	public virtual void Update (float delta) {

	}

	public virtual void End () {

	}

	public void Complete ()
	{
		_completed = true;
	}
}
