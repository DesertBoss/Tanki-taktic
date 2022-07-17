using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
	private BonusController bonusController;
	[SerializeField] private BonusType myBonusType = BonusType.None;

	public BonusType BonusType { get => myBonusType; internal set => myBonusType = value; }

    void Start()
    {
		bonusController = FindObjectOfType<BonusController> ();
		//GameObject.Destroy (gameObject, 20);
	}

    void Update()
    {
        
    }

	void OnTriggerEnter2D (Collider2D collision) {
		Tank tank = collision.GetComponent<Tank> ();
		if (tank == null || tank.Side != Side.Player) return;

		bonusController.OnTakeBonus (tank, myBonusType);
		GameObject.Destroy (gameObject);
	}
}

public enum BonusType {
	None,
	Helmet,
	Watch,
	Shovel,
	Star,
	Grenade,
	ExtraLife,
	Pistol
}