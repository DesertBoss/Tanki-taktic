using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
	[SerializeField] protected float duration = 10.0f;

	protected virtual void Start () {

	}

	protected virtual void Update ()
    {
        
    }

	protected virtual void OnTriggerEnter2D (Collider2D collision) 
	{
		
	}

	protected bool IsPlayer(Collider2D collision, out PlayerTank tank)
	{
		return collision.TryGetComponent(out tank);
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
