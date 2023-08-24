using UnityEngine;

public class PistolBonus : Bonus {
	protected override void OnCollided (Collider2D collision)
	{
		if (IsPlayer (collision, out PlayerTank tank))
		{
			ActivateEffect (new PistolEffect (tank, _duration));
			GameObject.Destroy (gameObject);
		}
	}
}