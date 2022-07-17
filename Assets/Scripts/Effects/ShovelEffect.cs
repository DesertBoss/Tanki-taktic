using System.Collections.Generic;
using UnityEngine;

public class ShovelEffect : Effect {
	Base myBase;

	private float myInitTime = 20;
	private float myRemainingTime;

	public ShovelEffect (BonusController controller) : base (BonusType.Shovel) {
		myBase = controller.transform.root.GetComponent<GameController> ().Base.GetComponent <Base> ();
		myRemainingTime = myInitTime;
	}
	public ShovelEffect (BonusController controller, float duration) : this (controller) {
		myRemainingTime = duration;
	}

	public override void Start () {
		myBase.Fortify ();
	}

	public override void Update (float delta) {
		myRemainingTime -= delta;

		if (myRemainingTime <= 0) myCompleted = true;
	}

	public override void OnDestroy () {
		myBase.UnFortify ();
		myBase = null;
	}
}