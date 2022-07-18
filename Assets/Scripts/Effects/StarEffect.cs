using UnityEngine;

public class StarEffect : Effect {
	private PlayerTank myOwner;

	public StarEffect (Tank owner) : base (BonusType.Star) {
		myOwner = owner as PlayerTank;
	}

	public override void Start () {
		if (myOwner != null) {
			myOwner.OnTakeStar ();
			InitContainer.instance.PlayerService.AddScore (myOwner.transform.position, 300);
		}

		myCompleted = true;
	}

	public override void OnDestroy () {
		myOwner = null;
	}
}