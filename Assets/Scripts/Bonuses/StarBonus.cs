using UnityEngine;

public class StarBonus : Bonus {
	protected override void OnCollided (Collider2D collision)
	{
		if (IsPlayer (collision, out PlayerTank tank))
		{
			ActivateEffect (new StarEffect (tank));
			GameObject.Destroy (gameObject);
		}
	}
}