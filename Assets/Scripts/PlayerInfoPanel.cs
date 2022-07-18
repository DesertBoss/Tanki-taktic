using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerInfoPanel : MonoBehaviour {
	[SerializeField] private Text _ScoreText;
	[SerializeField] private Text _ExtraLifesText;
	[SerializeField] private Text _EnemyCountText;
	[SerializeField] private GameObject _GameOverText;

	[SerializeField] private GameObject _ScoreGetterPrefab;

	private PlayerService myPlayerService;
	private SpawnController mySpawnController;

	private void Awake () {
		myPlayerService = InitContainer.instance.PlayerService;
		mySpawnController = InitContainer.instance.SpawnController;
	}

	private void OnDestroy () {
		_ScoreText = null;
		_ExtraLifesText = null;
		_EnemyCountText = null;
		_GameOverText = null;
		_ScoreGetterPrefab = null;
		myPlayerService = null;
		mySpawnController = null;
	}

	private void Start () {
		InitContainer.instance.Base.OnBaseDestroyed += (b) => { GameOverLabel (); };
		InitContainer.instance.PlayerService.OnEndLifes += (s) => { GameOverLabel (); };
	}

	void OnGUI () {
		UpdateInfo ();
	}

	private void UpdateInfo () {
		_ScoreText.text = $"Счет: {myPlayerService.Score}";
		_ExtraLifesText.text = $"Жизней: {myPlayerService.ExtraLifes}";
		_EnemyCountText.text = $"Врагов: {mySpawnController.WavesCount - mySpawnController.KilledEnemyes}";
	}

	private void GameOverLabel () {
		_GameOverText.SetActive (true);
	}

	public void CreateScoreMark (Vector3 position, string text) {
		Text comonent = GameObject.Instantiate (_ScoreGetterPrefab, position, Quaternion.identity, transform).GetComponentInChildren<Text> ();
		comonent.text = text;
	}
}
