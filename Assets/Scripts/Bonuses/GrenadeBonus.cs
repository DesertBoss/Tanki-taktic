using UnityEngine;

public class GrenadeBonus : Bonus {
	protected override void OnCollided (Collider2D collision)
	{
		if (IsPlayer (collision, out PlayerTank tank))
		{
			ActivateEffect (new GrenadeEffect ());
			GameObject.Destroy (gameObject);
		}
	}
}