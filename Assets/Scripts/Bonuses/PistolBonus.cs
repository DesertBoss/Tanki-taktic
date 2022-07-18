using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolBonus : Bonus {
	protected override void OnTriggerEnter2D (Collider2D collision) {
		if (IsPlayer (collision, out PlayerTank tank)) {
			InitContainer.instance.BonusController.OnTakeBonus (new PistolEffect (tank, duration));
			GameObject.Destroy (gameObject);
		}
	}
}