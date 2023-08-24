using UnityEngine;

public class BotTank : Tank {
	[SerializeField] private float _minTimeToShoot = 1;
	[SerializeField] private float _maxTimeToShoot = 4;
	[SerializeField] private float _minTimeToMove = 5;
	[SerializeField] private float _maxTimeToMove = 10;
	//[SerializeField] private float _minTimeOnMove = 4;
	//[SerializeField] private float _maxTimeOnMove = 8;
	[SerializeField] private float _stuckFactor = 0.01f;
	[SerializeField] private int _containBonusChance = 50;

	private float _curTimeToShoot = 2;
	private float _curTimeToMove = 2;
	//private float _curTimeOnMove = 2;
	private int _scoreOnKill = 100;

	private Vector2 _botMoveDirection;
	private Vector2 _curPosition;
	private Vector2 _prePosition;
	private Vector2 _freezedPosition;

	private byte _updatesAtStuck = 0;
	private bool _freezed = false;
	private bool _containBonus = false;

	private ActionsQueue<object> _actionsQuaye = new ActionsQueue<object> ();

	public int ScoreOnKill { get => _scoreOnKill; set => _scoreOnKill = value; }

	protected override void Start () {
		base.Start ();
		RandomMoveDirection ();
		_containBonus = Random.Range (0, 100) > _containBonusChance ? true : false;
		_curPosition = transform.position;
		_prePosition = _curPosition;
		_freezedPosition = Vector2.zero;
		_actionsQuaye.ExecuteAll ();
	}

	private void FixedUpdate () {
		_actionsQuaye.ExecuteAll ();

		if (_freezed) return;

		CheckShoot ();
		CheckMove ();
		CheckStuck ();
	}

	protected override void Update () {
		_moveDirection = _botMoveDirection;
		base.Update ();
	}

	private void CheckShoot () {
		_curTimeToShoot -= Time.fixedDeltaTime;

		if (_curTimeToShoot <= 0) {
			ResetTimeToShoot ();
			Shoot ();
		}
		if (_curTimeToMove <= 0) {
			ResetTimeToMove ();
			RandomMoveDirection ();
		}
	}

	private void CheckMove () {
		_curTimeToMove -= Time.fixedDeltaTime;

		_prePosition = _curPosition;
		_curPosition = transform.position;
	}

	private void CheckStuck () {
		float dist = Vector2.Distance (_prePosition, _curPosition);
		if (dist < _stuckFactor) {
			_updatesAtStuck++;
			if (_updatesAtStuck > 10) {
				_updatesAtStuck = 0;
				ResetTimeToMove ();
				ChangeMoveDirection ();
			}
		}
	}

	public override void Destroy () {
		if (_side != Side.Player && _scoreOnKill > 0)
			Glabal.PlayerService.AddScore (transform.position, _scoreOnKill);
		if (_containBonus) 
			Glabal.BonusController.SpawnRandomBonus (transform.position);
		Glabal.SpawnController.OnEnemyKilled ();

		base.Destroy ();
	}

	private void RandomMoveDirection () {
		int x = Random.Range (-1, 1);
		int y = x == 0 ? Random.Range (-1, 1) : 0;
		_botMoveDirection = new Vector2 (x, y);
		if (_botMoveDirection == Vector2.zero) RandomMoveDirection ();
	}

	private void ChangeMoveDirection () {

		if (_botMoveDirection == Vector2.up) {
			_botMoveDirection = Random.Range (1, 100) > 50 ? Vector2.right : Vector2.left;
		} else
		if (_botMoveDirection == Vector2.left) {
			_botMoveDirection = Random.Range (1, 100) > 50 ? Vector2.up : Vector2.down;
		} else
		if (_botMoveDirection == Vector2.right) {
			_botMoveDirection = Random.Range (1, 100) > 50 ? Vector2.down : Vector2.up;
		} else
		if (_botMoveDirection == Vector2.down) {
			_botMoveDirection = Random.Range (1, 100) > 50 ? Vector2.left : Vector2.right;
		}
	}

	private void ResetTimeToMove () {
		_curTimeToMove = Random.Range (_minTimeToMove, _maxTimeToMove);
	}

	private void ResetTimeToShoot () {
		_curTimeToShoot = Random.Range (_minTimeToShoot, _maxTimeToShoot);
	}

	public void FreezeBot (bool freezed) {
		_actionsQuaye.AddAction ((o) => {
			var freezed = (bool)o;
			if (freezed && !_freezed)
			{
				_freezed = freezed;
				_freezedPosition = _botMoveDirection;
				_botMoveDirection = Vector2.zero;
			}
			else if (!freezed && _freezed)
			{
				_botMoveDirection = _freezedPosition;
				_freezed = freezed;
			}
		}, freezed);
	}
}
