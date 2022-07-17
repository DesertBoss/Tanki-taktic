using UnityEngine;

public class ExtraLifeEffect : Effect {

	public ExtraLifeEffect () : base (BonusType.ExtraLife) {
		
	}

	public override void Start () {
		GameController.OnGetExtraLife ();
		myCompleted = true;
	}
}