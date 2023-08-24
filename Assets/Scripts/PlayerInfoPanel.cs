using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerInfoPanel : MonoBehaviour {
	[SerializeField] private Text _scoreText;
	[SerializeField] private Text _extraLifesText;
	[SerializeField] private Text _enemyCountText;
	[SerializeField] private GameObject _gameOverText;

	[SerializeField] private GameObject _scoreGetterPrefab;

	private PlayerService _playerService;
	private SpawnController _spawnController;

	private void Awake () {
		_playerService = Glabal.PlayerService;
		_spawnController = Glabal.SpawnController;
	}

	private void OnDestroy () {
		_scoreText = null;
		_extraLifesText = null;
		_enemyCountText = null;
		_gameOverText = null;
		_scoreGetterPrefab = null;
		_playerService = null;
		_spawnController = null;
	}

	private void Start () {
		Glabal.MainBase.OnBaseDestroyed += (b) => { GameOverLabel (); };
		Glabal.PlayerService.OnEndLifes += (s) => { GameOverLabel (); };
	}

	void OnGUI () {
		UpdateInfo ();
	}

	private void UpdateInfo () {
		_scoreText.text = $"Счет: {_playerService.Score}";
		_extraLifesText.text = $"Жизней: {_playerService.ExtraLifes}";
		_enemyCountText.text = $"Врагов: {_spawnController.WavesCount - _spawnController.KilledEnemyes}";
	}

	private void GameOverLabel () {
		_gameOverText.SetActive (true);
	}

	public void CreateScoreMark (Vector3 position, string text) {
		Text comonent = GameObject.Instantiate (_scoreGetterPrefab, position, Quaternion.identity, transform).GetComponentInChildren<Text> ();
		comonent.text = text;
	}
}
