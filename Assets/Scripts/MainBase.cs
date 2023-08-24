using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof (SpriteRenderer))]
public class MainBase : MonoBehaviour, IHitable
{
	private readonly Vector2[] _wallPositions = {
		new Vector2 (-0.75f, 0.75f),
		new Vector2 (-0.25f, 0.75f),
		new Vector2 (0.25f, 0.75f),
		new Vector2 (0.75f, 0.75f),
		new Vector2 (-0.75f, 0.25f),
		new Vector2 (0.75f, 0.25f),
		new Vector2 (-0.75f, -0.25f),
		new Vector2 (0.75f, -0.25f)
	};

	private SpriteRenderer _spriteRenderer;

	private List<GameObject> _brickWalls;
	private List<GameObject> _steelWalls;

	[SerializeField] private GameObject _steelWallPrefab;
	[SerializeField] private GameObject _brickWallPrefab;
	[SerializeField] private Sprite _destroyedSprite;

	public event Action <MainBase> OnBaseDestroyed;

	void Awake () {
		_spriteRenderer = GetComponent<SpriteRenderer> ();

		InitWalls ();
	}

	private void OnDestroy () {
		_spriteRenderer = null;
		_brickWalls.Clear ();
		_steelWalls.Clear ();
		_brickWalls = null;
		_steelWalls = null;
		_steelWallPrefab = null;
		_brickWallPrefab = null;
		_destroyedSprite = null;
		OnBaseDestroyed = null;
	}

	private void InitWalls () {
		_brickWalls = new List<GameObject> (_wallPositions.Length);
		_steelWalls = new List<GameObject> (_wallPositions.Length);

		for (int i = 0; i < _wallPositions.Length; i++) {
			GameObject wall = GameObject.Instantiate (_brickWallPrefab);
			wall.transform.position = _wallPositions[i];
			wall.transform.SetParent (transform, false);
			_brickWalls.Add (wall);
		}

		for (int i = 0; i < _wallPositions.Length; i++) {
			GameObject wall = GameObject.Instantiate (_steelWallPrefab);
			wall.transform.position = _wallPositions[i];
			wall.transform.SetParent (transform, false);
			_steelWalls.Add (wall);
			wall.SetActive (false);
		}
	}

    public bool OnGetHit (Bullet bullet) {
		_spriteRenderer.sprite = _destroyedSprite;
		OnBaseDestroyed?.Invoke (this);
		return true;
	}

	public void Fortify () {
		foreach (var myBrickWall in _brickWalls) {
			myBrickWall.SetActive (false);
		}

		foreach (var mySteelWall in _steelWalls) {
			mySteelWall.SetActive (true);
		}
	}

	public void UnFortify () {
		foreach (var mySteelWall in _steelWalls) {
			mySteelWall.SetActive (false);
		}

		foreach (var myBrickWall in _brickWalls) {
			myBrickWall.SetActive (true);
		}
	}
}
