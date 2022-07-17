using UnityEngine;

public class WatchEffect : Effect {
	private Transform tanksContainer;

	private float myInitTime = 10;
	private float myRemainingTime;

	public WatchEffect (BonusController controller) : base (BonusType.Watch) {
		tanksContainer = controller.transform.root.GetComponent<GameController> ().EnemyTanks;
		myRemainingTime = myInitTime;
	}
	public WatchEffect (BonusController controller, float duration) : this (controller) {
		myRemainingTime = duration;
	}

	public override void Start () {
		DisableAllObjects ();
	}

	public override void Update (float delta) {
		myRemainingTime -= delta;

		DisableAllObjects ();

		if (myRemainingTime <= 0) myCompleted = true;
	}

	public override void OnDestroy () {
		EnableAllObjects ();
		tanksContainer = null;
	}

	private void DisableAllObjects () {
		for (int i = 0; i < tanksContainer.childCount; i++) {
			tanksContainer.GetChild (i).GetComponent<BotController> ().FreezeBot (true);
		}
	}

	private void EnableAllObjects () {
		for (int i = 0; i < tanksContainer.childCount; i++) {
			tanksContainer.GetChild (i).GetComponent<BotController> ().FreezeBot (false);
		}
	}
}

