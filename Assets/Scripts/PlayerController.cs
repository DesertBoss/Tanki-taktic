using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Tank {
	private int starsCollected = 0;

	protected override void Start () {
		base.Start ();
		mySide = Side.Player;
	}

	protected override void Update () {
		//if (!Input.anyKey) return;
		// Использовать глобальную привязки клавиш для направления движения
		myMoveDirection = Vector2.zero;

		if (Input.GetKey (KeyCode.W)) {
			myMoveDirection = Vector2.up;
		} else if (Input.GetKey (KeyCode.S)) {
			myMoveDirection = Vector2.down;
		} else if (Input.GetKey (KeyCode.A)) {
			myMoveDirection = Vector2.left;
		} else if (Input.GetKey (KeyCode.D)) {
			myMoveDirection = Vector2.right;
		}

		base.Update ();

		if (Input.GetKeyDown (KeyCode.Space)) {
			Shoot ();
		}
	}

	internal void OnTakeStar () {
		starsCollected++;
		if (starsCollected > 3) {
			myGunPower = BulletPower.Extended;
		}
	}

	internal override void Destroy () {
		GameController.OnPlayerKilled ();
		base.Destroy ();
	}
}
