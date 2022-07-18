using UnityEngine;

public class ExtraLifeEffect : Effect {

	public ExtraLifeEffect () : base (BonusType.ExtraLife) {
		
	}

	public override void Start () {
		InitContainer.instance.PlayerService.OnGetExtraLife ();
		myCompleted = true;
	}
}