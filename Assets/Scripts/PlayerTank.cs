using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : Tank {
	private int myStarsCollected = 0;

	protected override void Start () {
		base.Start ();
		_Side = Side.Player;
	}

	protected override void Update () {
		CheckInput ();

		base.Update ();
	}

	private void CheckInput () {
		//if (!Input.anyKey) return;

		myMoveDirection = Vector2.zero;

		myMoveDirection.x = Input.GetAxis ("Horizontal");
		myMoveDirection.y = myMoveDirection.x == 0 ? Input.GetAxis ("Vertical") : 0;

		/*if (Input.GetKey (KeyCode.W)) {
			myMoveDirection = Vector2.up;
		} else if (Input.GetKey (KeyCode.S)) {
			myMoveDirection = Vector2.down;
		} else if (Input.GetKey (KeyCode.A)) {
			myMoveDirection = Vector2.left;
		} else if (Input.GetKey (KeyCode.D)) {
			myMoveDirection = Vector2.right;
		}*/

		if (Input.GetKeyDown (KeyCode.Space)) {
			Shoot ();
		}
	}

	public void OnTakeStar () {
		myStarsCollected++;
		if (myStarsCollected > 3) {
			myGunPower = BulletPower.Extended;
		}
	}

	public override void Destroy () {
		InitContainer.instance.PlayerService.OnPlayerKilled ();
		base.Destroy ();
	}
}
