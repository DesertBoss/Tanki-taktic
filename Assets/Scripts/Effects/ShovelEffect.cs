using System.Collections.Generic;
using UnityEngine;

public class ShovelEffect : Effect {
	private float myInitTime = 20;
	private float myRemainingTime;

	public ShovelEffect () : base (BonusType.Shovel) {
		myRemainingTime = myInitTime;
	}
	public ShovelEffect (float duration) : this () {
		myRemainingTime = duration;
	}

	public override void Start () {
		InitContainer.instance.Base.Fortify ();
	}

	public override void Update (float delta) {
		myRemainingTime -= delta;

		if (myRemainingTime <= 0) myCompleted = true;
	}

	public override void OnDestroy () {
		InitContainer.instance.Base.UnFortify ();
	}
}