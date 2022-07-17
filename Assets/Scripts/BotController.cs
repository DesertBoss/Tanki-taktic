using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : Tank {
	[SerializeField] private float myMinTimeToShoot = 1;
	[SerializeField] private float myMaxTimeToShoot = 4;
	[SerializeField] private float myMinTimeToMove = 5;
	[SerializeField] private float myMaxTimeToMove = 10;
	//[SerializeField] private float myMinTimeOnMove = 4;
	//[SerializeField] private float myMaxTimeOnMove = 8;

	private float myCurTimeToShoot = 2;
	private float myCurTimeToMove = 2;
	//private float myCurTimeOnMove = 2;
	private int myScoreOnKill = 100;

	private Vector2 myBotMoveDirection;
	private Vector2 myCurPosition;
	private Vector2 myPrePosition;
	private Vector2 myFreezedPosition;

	private byte myUpdatesAtStuck = 0;
	private bool myFreezed = false;
	private bool myContainBonus = false;

	public int ScoreOnKill { get => myScoreOnKill; internal set => myScoreOnKill = value; }

	protected override void Start () {
		base.Start ();
		RandomMoveDirection ();
		myContainBonus = Random.Range (1, 100) > 50 ? true : false;
		myCurPosition = transform.position;
		myPrePosition = myCurPosition;
		myFreezedPosition = Vector2.zero;
	}

	private void FixedUpdate () {
		if (myFreezed) return;

		myCurTimeToShoot -= Time.fixedDeltaTime;
		myCurTimeToMove -= Time.fixedDeltaTime;
		//myCurTimeOnMove -= Time.fixedDeltaTime;

		if (myCurTimeToShoot <= 0) {
			ResetTimeToShoot ();
			Shoot ();
		}
		if (myCurTimeToMove <= 0) {
			ResetTimeToMove ();
			RandomMoveDirection ();
		}
		/*if (myCurTimeOnMove <= 0) {
            myCurTimeOnMove = Random.Range (myMinTimeOnMove, myMaxTimeOnMove);
        }*/

		myPrePosition = myCurPosition;
		myCurPosition = transform.position;

		float dist = Vector2.Distance (myPrePosition, myCurPosition);
		if (dist < 0.01f) {
			myUpdatesAtStuck++;
			if (myUpdatesAtStuck > 10) {
				myUpdatesAtStuck = 0;
				ResetTimeToMove ();
				ChangeMoveDirection ();
			}
		}
	}

	protected override void Update () {
		myMoveDirection = myBotMoveDirection;
		base.Update ();
	}

	internal override void Destroy () {
		GameController controller = transform.root.GetComponent<GameController> ();
		if (mySide != Side.Player && myScoreOnKill > 0)
			controller.AddScore (transform.position, myScoreOnKill);
		if (myContainBonus) SpawnBonus ();
		controller.OnEnemyKilled ();
		base.Destroy ();
	}

	private void RandomMoveDirection () {
		int x = Random.Range (-1, 1);
		int y = x == 0 ? Random.Range (-1, 1) : 0;
		myBotMoveDirection = new Vector2 (x, y);
		if (myBotMoveDirection == Vector2.zero) RandomMoveDirection ();
	}

	private void ChangeMoveDirection () {

		if (myBotMoveDirection == Vector2.up) {
			myBotMoveDirection = Random.Range (1, 100) > 50 ? Vector2.right : Vector2.left;
		} else
		if (myBotMoveDirection == Vector2.left) {
			myBotMoveDirection = Random.Range (1, 100) > 50 ? Vector2.up : Vector2.down;
		} else
		if (myBotMoveDirection == Vector2.right) {
			myBotMoveDirection = Random.Range (1, 100) > 50 ? Vector2.down : Vector2.up;
		} else
		if (myBotMoveDirection == Vector2.down) {
			myBotMoveDirection = Random.Range (1, 100) > 50 ? Vector2.left : Vector2.right;
		}
	}

	private void ResetTimeToMove () {
		myCurTimeToMove = Random.Range (myMinTimeToMove, myMaxTimeToMove);
	}

	private void ResetTimeToShoot () {
		myCurTimeToShoot = Random.Range (myMinTimeToShoot, myMaxTimeToShoot);
	}

	public void FreezeBot (bool freezed) {
		if (freezed && !myFreezed) {
			myFreezed = freezed;
			myFreezedPosition = myBotMoveDirection;
			myBotMoveDirection = Vector2.zero;
		} else if (!freezed && myFreezed) {
			myFreezed = freezed;
			myBotMoveDirection = myFreezedPosition;
		}
	}
}
