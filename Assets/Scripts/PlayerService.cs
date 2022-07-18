using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class PlayerService : MonoBehaviour {
	[SerializeField] private int _ExtraLifes = 2;

	private int myScore = 0;

	public int ExtraLifes => _ExtraLifes;
	public int Score => myScore;


	public event Action <PlayerService> OnEndLifes;

	private void OnDestroy () {
		OnEndLifes = null;
	}

	public void AddScore (int score) {
		myScore += score;
	}

	public void AddScore (Vector2 position, int score) {
		AddScore (score);

		InitContainer.instance.PlayerInfoPanel.CreateScoreMark (position, score.ToString ());
	}

	public void OnPlayerKilled () {
		_ExtraLifes--;

		if (ExtraLifes > 0) {
			InitContainer.instance.SpawnController.SpawnPlayer ();
		} else {
			OnEndLifes?.Invoke (this);
		}
	}

	public void OnGetExtraLife () {
		_ExtraLifes++;
	}
}

