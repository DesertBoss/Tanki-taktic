using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalmetBonus : Bonus {
	protected override void OnTriggerEnter2D (Collider2D collision) {
		if (IsPlayer (collision, out PlayerTank tank)) {
			InitContainer.instance.BonusController.OnTakeBonus (new HalmetEffect (tank, duration));
			GameObject.Destroy (gameObject);
		}
	}
}