using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SpawnController : MonoBehaviour {
	[SerializeField] private GameObject _playerPrefab;
	[SerializeField] private GameObject[] _enemyPrefabs;
	[SerializeField] private Spawner[] _enemySpawners;
	[SerializeField] private Spawner _playerSpawner;
	[SerializeField] private Transform _tanksEnemyContainer;
	[SerializeField] private int _dificulty = 1;
	[SerializeField] private int _wavesCount = 20;
	[SerializeField] private float _timeToEnemySpawn = 3;
	[SerializeField] private int _maxEnemyes = 4;

	private int _killedEnemyes = 0;
	private int _currentEnemyes = 0;
	private bool _freezeEnemyes = false;
	private bool _baseDestroyed = false;

	public int WavesCount => _wavesCount;
	public int KilledEnemyes => _killedEnemyes;


	private void Awake () {
		foreach (var spawner in _enemySpawners)
		{
			spawner.OnEnemySpawned += OnEnemySpawned;
		}
	}

	private void OnDestroy () {
		_playerPrefab = null;
		_enemyPrefabs = null;
		_enemySpawners = null;
		_playerSpawner = null;
		_tanksEnemyContainer = null;
	}

	private void Start () {
		SpawnPlayer ();
		StartCoroutine (StartSpawn ());

		Glabal.MainBase.OnBaseDestroyed += (b) => { _baseDestroyed = true; };
	}

	public void OnEnemyKilled () {
		_currentEnemyes--;
		_killedEnemyes++;
	}

	private IEnumerator StartSpawn () {
		for (int i = 0; i < _wavesCount; i++) {
			if (_baseDestroyed)
				break;

			while (_currentEnemyes >= _maxEnemyes)
				yield return new WaitForSeconds (1);

			int spawnerNum = Random.Range (0, _enemySpawners.Length);
			_enemySpawners[spawnerNum].Spawn (_enemyPrefabs[Random.Range (0, _enemyPrefabs.Length)], _tanksEnemyContainer);
			_currentEnemyes++;

			yield return new WaitForSeconds (_timeToEnemySpawn / _dificulty);
		}
	}

	public void SpawnPlayer () {
		_playerSpawner.Spawn (_playerPrefab, transform);
	}

	public void KillAllEnemyes () {
		for (int i = 0; i < _tanksEnemyContainer.childCount; i++) {
			BotTank tank = _tanksEnemyContainer.GetChild (i).GetComponent<BotTank> ();
			tank.ScoreOnKill = 0;
			tank.Destroy ();
		}
	}

	public void FreezeEnemyes () {
		_freezeEnemyes = true;

		for (int i = 0; i < _tanksEnemyContainer.childCount; i++) {
			BotTank tank = _tanksEnemyContainer.GetChild (i).GetComponent<BotTank> ();
			tank.FreezeBot (true);
		}
	}

	public void UnfreezeEnemyes () {
		_freezeEnemyes = false;

		for (int i = 0; i < _tanksEnemyContainer.childCount; i++) {
			BotTank tank = _tanksEnemyContainer.GetChild (i).GetComponent<BotTank> ();
			tank.FreezeBot (false);
		}
	}

	private void OnEnemySpawned (Spawner spawner, Tank tank)
	{
		if (_freezeEnemyes && tank.Side == Side.Enemy)
		{
			((BotTank)tank).FreezeBot (true);
		}
	}
}
