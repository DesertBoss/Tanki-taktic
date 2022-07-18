using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchBonus : Bonus {
	protected override void OnTriggerEnter2D (Collider2D collision) {
		if (IsPlayer (collision, out PlayerTank tank)) {
			InitContainer.instance.BonusController.OnTakeBonus (new WatchEffect (duration));
			GameObject.Destroy (gameObject);
		}
	}
}