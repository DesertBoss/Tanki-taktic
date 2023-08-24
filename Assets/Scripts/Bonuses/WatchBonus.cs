using UnityEngine;

public class WatchBonus : Bonus {
	protected override void OnCollided (Collider2D collision)
	{
		if (IsPlayer (collision, out PlayerTank tank))
		{
			ActivateEffect (new WatchEffect (_duration));
			GameObject.Destroy (gameObject);
		}
	}
}