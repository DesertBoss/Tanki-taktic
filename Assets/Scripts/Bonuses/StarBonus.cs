using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarBonus : Bonus {
	protected override void OnTriggerEnter2D (Collider2D collision) {
		if (IsPlayer (collision, out PlayerTank tank)) {
			InitContainer.instance.BonusController.OnTakeBonus (new StarEffect (tank));
			GameObject.Destroy (gameObject);
		}
	}
}