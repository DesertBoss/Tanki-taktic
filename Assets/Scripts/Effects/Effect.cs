public abstract class Effect {
	protected BonusType myBonusType = BonusType.None;
	protected bool myCompleted = false;

	public BonusType BonusType { get => myBonusType; }
	public bool Completed { get => myCompleted; set => myCompleted = value; }

	protected Effect (BonusType type) {
		myBonusType = type;
	}

	public virtual void Start () {

	}

	public virtual void Update (float delta) {

	}

	public virtual void OnDestroy () {

	}
}
