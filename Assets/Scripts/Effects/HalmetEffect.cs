using UnityEngine;

public class HalmetEffect : TimedEffect {
	private Tank _owner;
	private GameObject _forceField;
	private int _initHealth;

	public HalmetEffect (Tank owner) : base (BonusType.Helmet) {
		_owner = owner;
	}

	public HalmetEffect (Tank owner, float duration) : this (owner) {
		_remainingTime = duration;
	}

	public override void Start () {
		_initHealth = _owner.Health;
		_owner.Health = 999;
		_forceField = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/ForceField"), _owner.transform);
	}

	public override void Update (float delta) {
		base.Update (delta);

		if (_owner == null)
			Complete ();
	}

	public override void End () {
		if (_owner == null)
			return;

		_owner.Health = _initHealth;
		GameObject.Destroy (_forceField);
		_owner = null;
		_forceField = null;
	}
}
