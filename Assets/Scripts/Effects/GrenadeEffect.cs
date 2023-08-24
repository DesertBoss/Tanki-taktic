public class GrenadeEffect : Effect {
	public GrenadeEffect () : base (BonusType.Grenade) {
		
	}

	public override void Start () {
		Glabal.SpawnController.KillAllEnemyes ();
		Complete ();
	}
}