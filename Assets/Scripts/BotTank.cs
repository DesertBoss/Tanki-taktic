using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTank : Tank {
	[SerializeField] private float _MinTimeToShoot = 1;
	[SerializeField] private float _MaxTimeToShoot = 4;
	[SerializeField] private float _MinTimeToMove = 5;
	[SerializeField] private float _MaxTimeToMove = 10;
	//[SerializeField] private float myMinTimeOnMove = 4;
	//[SerializeField] private float myMaxTimeOnMove = 8;
	[SerializeField] private float _StuckFactor = 0.01f;
	[SerializeField] private int _ContainBonusChance = 50;

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

	public int ScoreOnKill { get => myScoreOnKill; set => myScoreOnKill = value; }

	protected override void Start () {
		base.Start ();
		RandomMoveDirection ();
		myContainBonus = Random.Range (0, 100) > _ContainBonusChance ? true : false;
		myCurPosition = transform.position;
		myPrePosition = myCurPosition;
		myFreezedPosition = Vector2.zero;
	}

	private void FixedUpdate () {
		if (myFreezed) return;

		CheckShoot ();
		CheckMove ();
		CheckStuck ();
	}

	protected override void Update () {
		myMoveDirection = myBotMoveDirection;
		base.Update ();
	}

	private void CheckShoot () {
		myCurTimeToShoot -= Time.fixedDeltaTime;

		if (myCurTimeToShoot <= 0) {
			ResetTimeToShoot ();
			Shoot ();
		}
		if (myCurTimeToMove <= 0) {
			ResetTimeToMove ();
			RandomMoveDirection ();
		}
	}

	private void CheckMove () {
		myCurTimeToMove -= Time.fixedDeltaTime;

		myPrePosition = myCurPosition;
		myCurPosition = transform.position;
	}

	private void CheckStuck () {
		float dist = Vector2.Distance (myPrePosition, myCurPosition);
		if (dist < _StuckFactor) {
			myUpdatesAtStuck++;
			if (myUpdatesAtStuck > 10) {
				myUpdatesAtStuck = 0;
				ResetTimeToMove ();
				ChangeMoveDirection ();
			}
		}
	}

	public override void Destroy () {
		if (_Side != Side.Player && myScoreOnKill > 0)
			InitContainer.instance.PlayerService.AddScore (transform.position, myScoreOnKill);
		if (myContainBonus) 
			InitContainer.instance.BonusController.SpawnRandomBonus (transform.position);
		InitContainer.instance.SpawnController.OnEnemyKilled ();

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
		myCurTimeToMove = Random.Range (_MinTimeToMove, _MaxTimeToMove);
	}

	private void ResetTimeToShoot () {
		myCurTimeToShoot = Random.Range (_MinTimeToShoot, _MaxTimeToShoot);
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
