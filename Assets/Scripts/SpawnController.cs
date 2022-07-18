using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SpawnController : MonoBehaviour {
	[SerializeField] private GameObject _PlayerPrefab;
	[SerializeField] private GameObject[] _EnemyPrefabs;
	[SerializeField] private Spawner[] _EnemySpawners;
	[SerializeField] private Spawner _PlayerSpawner;
	[SerializeField] private Transform _TanksEnemyContainer;
	[SerializeField] private int _Dificulty = 1;
	[SerializeField] private int _WavesCount = 20;
	[SerializeField] private float _TimeToEnemySpawn = 3;
	[SerializeField] private int _MaxEnemyes = 4;

	private int myKilledEnemyes = 0;
	private int myCurrentEnemyes = 0;
	private bool myPauseSpawn = false;
	private bool myBaseDestroyed = false;

	public int WavesCount => _WavesCount;
	public int KilledEnemyes => myKilledEnemyes;


	private void Awake () {
		
	}

	private void OnDestroy () {
		_PlayerPrefab = null;
		_EnemyPrefabs = null;
		_EnemySpawners = null;
		_PlayerSpawner = null;
		_TanksEnemyContainer = null;
	}

	private void Start () {
		SpawnPlayer ();
		StartCoroutine (StartSpawn ());

		InitContainer.instance.Base.OnBaseDestroyed += (b) => { myBaseDestroyed = true; };
	}

	public void OnEnemyKilled () {
		myCurrentEnemyes--;
		myKilledEnemyes++;
	}

	private IEnumerator StartSpawn () {
		for (int i = 0; i < _WavesCount; i++) {
			int spawnerNum = Random.Range (0, _EnemySpawners.Length);
			_EnemySpawners[spawnerNum].Spawn (_EnemyPrefabs[Random.Range (0, _EnemyPrefabs.Length)], _TanksEnemyContainer);
			myCurrentEnemyes++;
			while (myCurrentEnemyes >= _MaxEnemyes || myPauseSpawn) yield return new WaitForSeconds (1);
			if (myBaseDestroyed) break;
			yield return new WaitForSeconds (_TimeToEnemySpawn / _Dificulty);
		}
	}

	public void SpawnPlayer () {
		_PlayerSpawner.Spawn (_PlayerPrefab, transform);
	}

	public void KillAllEnemyes () {
		for (int i = 0; i < _TanksEnemyContainer.childCount; i++) {
			BotTank tank = _TanksEnemyContainer.GetChild (i).GetComponent<BotTank> ();
			tank.ScoreOnKill = 0;
			tank.Destroy ();
		}
	}

	public void FreezeEnemyes () {
		myPauseSpawn = true;

		for (int i = 0; i < _TanksEnemyContainer.childCount; i++) {
			BotTank tank = _TanksEnemyContainer.GetChild (i).GetComponent<BotTank> ();
			tank.FreezeBot (true);
		}
	}

	public void UnfreezeEnemyes () {
		myPauseSpawn = false;

		for (int i = 0; i < _TanksEnemyContainer.childCount; i++) {
			BotTank tank = _TanksEnemyContainer.GetChild (i).GetComponent<BotTank> ();
			tank.FreezeBot (false);
		}
	}
}
