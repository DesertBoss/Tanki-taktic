using UnityEngine;

public class ShovelBonus : Bonus {
	protected override void OnCollided (Collider2D collision)
	{
		if (IsPlayer (collision, out PlayerTank tank))
		{
			ActivateEffect (new ShovelEffect (_duration));
			GameObject.Destroy (gameObject);
		}
	}
}