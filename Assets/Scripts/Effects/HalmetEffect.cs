using UnityEngine;

public class HalmetEffect : Effect {
	private Tank myOwner;
	private GameObject forceField;
	private int myInitHealth;

	private float myInitTime = 10;
	private float myRemainingTime;

	public HalmetEffect (Tank owner) : base (BonusType.Helmet) {
		myOwner = owner;
		myRemainingTime = myInitTime;
	}

	public HalmetEffect (Tank owner, float duration) : this (owner) {
		myRemainingTime = duration;
	}

	public override void Start () {
		myInitHealth = myOwner.Health;
		myOwner.Health = 999;
		forceField = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/ForceField"), myOwner.transform);
	}

	public override void Update (float delta) {
		myRemainingTime -= delta;

		if (myRemainingTime <= 0 || myOwner == null) myCompleted = true;
	}

	public override void OnDestroy () {
		if (myOwner == null) return;
		myOwner.Health = myInitHealth;
		GameObject.Destroy (forceField);
		myOwner = null;
		forceField = null;
	}
}
