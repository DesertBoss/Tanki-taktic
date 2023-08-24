using UnityEngine;

public class ExtraLifeBonus : Bonus {
	protected override void OnCollided (Collider2D collision)
	{
		if (IsPlayer (collision, out PlayerTank tank))
		{
			ActivateEffect (new ExtraLifeEffect ());
			GameObject.Destroy (gameObject);
		}
	}
}