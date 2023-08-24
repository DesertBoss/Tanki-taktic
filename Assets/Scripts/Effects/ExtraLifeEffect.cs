public class ExtraLifeEffect : Effect {

	public ExtraLifeEffect () : base (BonusType.ExtraLife) {
		
	}

	public override void Start () {
		Glabal.PlayerService.OnGetExtraLife ();
		Complete ();
	}
}