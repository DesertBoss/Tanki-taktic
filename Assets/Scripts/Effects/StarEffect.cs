using UnityEngine;

public class StarEffect : Effect {
	private PlayerController myOwner;

	public StarEffect (Tank owner) : base (BonusType.Star) {
		myOwner = owner as PlayerController;
	}

	public override void Start () {
		if (myOwner != null) {
			myOwner.OnTakeStar ();
			myOwner.transform.root.GetComponent<GameController> ().AddScore (myOwner.transform.position, 300);
		}

		myCompleted = true;
	}

	public override void OnDestroy () {
		myOwner = null;
	}
}