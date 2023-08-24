using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
	[SerializeField] protected float _duration = 10.0f;

	private void OnTriggerEnter2D (Collider2D collision) 
	{
		OnCollided (collision);
	}

	protected abstract void OnCollided (Collider2D collision);

	protected bool IsPlayer(Collider2D collision, out PlayerTank tank)
	{
		return collision.TryGetComponent(out tank);
	}

	protected void ActivateEffect (Effect effect)
	{
		Glabal.BonusController.OnTakeBonus (effect);
	}

	/*protected void Animation(Action endAnimation)
	{
		Vector3 startScale = transform.localScale;

		transform.DOScale(startScale * 2, 1).OnComplete(() =>
		{
			transform.DOScale(startScale, 1).OnComplete(()=>
			{
				endAnimation?.Invoke();
			});
		});
	}*/
}
