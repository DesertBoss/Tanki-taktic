using UnityEngine;

public class HalmetBonus : Bonus {
	protected override void OnCollided (Collider2D collision)
	{
		if (IsPlayer (collision, out PlayerTank tank))
		{
			ActivateEffect (new HalmetEffect (tank, _duration));
			GameObject.Destroy (gameObject);
		}
	}
}