using UnityEngine;

public class GrenadeEffect : Effect {
	private Transform tanksContainer;

	public GrenadeEffect (BonusController controller) : base (BonusType.Grenade) {
		tanksContainer = controller.transform.root.GetComponent<GameController> ().EnemyTanks;
	}

	public override void Start () {
		for (int i = 0; i < tanksContainer.childCount; i++) {
			BotController tank = tanksContainer.GetChild (i).GetComponent<BotController> ();
			tank.ScoreOnKill = 0;
			tank.Destroy ();
		}
		myCompleted = true;
	}

	public override void OnDestroy () {
		tanksContainer = null;
	}
}