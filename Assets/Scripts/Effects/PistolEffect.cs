using UnityEngine;

public class PistolEffect : Effect {
	private PlayerController myOwner;
	private BulletPower myInitPower;

	private float myInitTime = 10;
	private float myRemainingTime;

	public PistolEffect (Tank owner) : base (BonusType.Star) {
		myOwner = owner as PlayerController;
		myRemainingTime = myInitTime;
	}

	public PistolEffect (Tank owner, float duration) : this (owner) {
		myRemainingTime = duration;
	}

	public override void Start () {
		if (myOwner != null) {
			myInitPower = myOwner.GunPower;
			myOwner.GunPower = BulletPower.Extended;
		}
	}

	public override void Update (float delta) {
		myRemainingTime -= delta;

		if (myRemainingTime <= 0) myCompleted = true;
	}

	public override void OnDestroy () {
		if (myOwner == null) return;
		myOwner.GunPower = myInitPower;
		myOwner = null;
	}
}