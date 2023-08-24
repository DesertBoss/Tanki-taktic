public class TimedEffect : Effect
{
	protected float _startTime = 10;	// Default time
	protected float _remainingTime;

	public TimedEffect (BonusType type) : base (type)
	{
		_remainingTime = _startTime;
	}

	public override void Update (float delta)
	{
		_remainingTime -= delta;

		if (_remainingTime <= 0f)
			Complete ();
	}
}