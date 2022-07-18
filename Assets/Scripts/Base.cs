using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent (typeof (SpriteRenderer))]
public class Base : MonoBehaviour, IHitable
{
	private readonly Vector2[] myWallPositions = {
		new Vector2 (-0.75f, 0.75f),
		new Vector2 (-0.25f, 0.75f),
		new Vector2 (0.25f, 0.75f),
		new Vector2 (0.75f, 0.75f),
		new Vector2 (-0.75f, 0.25f),
		new Vector2 (0.75f, 0.25f),
		new Vector2 (-0.75f, -0.25f),
		new Vector2 (0.75f, -0.25f)
	};

	private SpriteRenderer mySpriteRenderer;

	private List<GameObject> myBrickWalls;
	private List<GameObject> mySteelWalls;

	[SerializeField] private GameObject _SteelWallPrefab;
	[SerializeField] private GameObject _BrickWallPrefab;
	[SerializeField] private Sprite _DestroyedSprite;

	public event Action <Base> OnBaseDestroyed;

	void Awake () {
		mySpriteRenderer = GetComponent<SpriteRenderer> ();

		InitWalls ();
	}

	private void OnDestroy () {
		mySpriteRenderer = null;
		myBrickWalls.Clear ();
		mySteelWalls.Clear ();
		myBrickWalls = null;
		mySteelWalls = null;
		_SteelWallPrefab = null;
		_BrickWallPrefab = null;
		_DestroyedSprite = null;
		OnBaseDestroyed = null;
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }

	private void InitWalls () {
		myBrickWalls = new List<GameObject> (myWallPositions.Length);
		mySteelWalls = new List<GameObject> (myWallPositions.Length);

		for (int i = 0; i < myWallPositions.Length; i++) {
			GameObject wall = GameObject.Instantiate (_BrickWallPrefab);
			wall.transform.position = myWallPositions[i];
			wall.transform.SetParent (transform, false);
			myBrickWalls.Add (wall);
		}

		for (int i = 0; i < myWallPositions.Length; i++) {
			GameObject wall = GameObject.Instantiate (_SteelWallPrefab);
			wall.transform.position = myWallPositions[i];
			wall.transform.SetParent (transform, false);
			mySteelWalls.Add (wall);
			wall.SetActive (false);
		}
	}

    public bool OnGetHit (Bullet bullet) {
		mySpriteRenderer.sprite = _DestroyedSprite;
		OnBaseDestroyed?.Invoke (this);
		return true;
	}

	public void Fortify () {
		foreach (var myBrickWall in myBrickWalls) {
			myBrickWall.SetActive (false);
		}

		foreach (var mySteelWall in mySteelWalls) {
			mySteelWall.SetActive (true);
		}
	}

	public void UnFortify () {
		foreach (var mySteelWall in mySteelWalls) {
			mySteelWall.SetActive (false);
		}

		foreach (var myBrickWall in myBrickWalls) {
			myBrickWall.SetActive (true);
		}
	}
}
