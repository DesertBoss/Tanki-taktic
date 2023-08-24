using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : Tank {
	private int _starsCollected = 0;

	protected override void Start () {
		base.Start ();
		_side = Side.Player;
	}

	protected override void Update () {
		CheckInput ();

		base.Update ();
	}

	private void CheckInput () {
		//if (!Input.anyKey) return;

		_moveDirection = Vector2.zero;

		_moveDirection.x = Input.GetAxis ("Horizontal");
		_moveDirection.y = _moveDirection.x == 0 ? Input.GetAxis ("Vertical") : 0;

		/*if (Input.GetKey (KeyCode.W)) {
			_moveDirection = Vector2.up;
		} else if (Input.GetKey (KeyCode.S)) {
			_moveDirection = Vector2.down;
		} else if (Input.GetKey (KeyCode.A)) {
			_moveDirection = Vector2.left;
		} else if (Input.GetKey (KeyCode.D)) {
			_moveDirection = Vector2.right;
		}*/

		if (Input.GetKeyDown (KeyCode.Space)) {
			Shoot ();
		}
	}

	public void OnTakeStar () {
		_starsCollected++;
		if (_starsCollected > 3) {
			_gunPower = BulletPower.Extended;
		}
	}

	public override void Destroy () {
		Glabal.PlayerService.OnPlayerKilled ();
		base.Destroy ();
	}
}
