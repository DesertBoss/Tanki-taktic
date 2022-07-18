using UnityEngine;

public class PistolEffect : Effect {
	private PlayerTank myOwner;
	private BulletPower myInitPower;

	private float myInitTime = 10;
	private float myRemainingTime;

	public PistolEffect (Tank owner) : base (BonusType.Star) {
		myOwner = owner as PlayerTank;
		myRemainingTime = myInitTime;
	}

	public PistolEffect (Tank owner, float duration) : this (owner) {
		myRemainingTime = duration;
	}

	public override void Start () {
		if (myOwner == null) return;

		myInitPower = myOwner.GunPower;
		myOwner.GunPower = BulletPower.Forced;
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