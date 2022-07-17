using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Base : MonoBehaviour, IHitable
{
	private Vector2[] myWallPositions = {
		new Vector2 (-0.75f, 0.75f),
		new Vector2 (-0.25f, 0.75f),
		new Vector2 (0.25f, 0.75f),
		new Vector2 (0.75f, 0.75f),
		new Vector2 (-0.75f, 0.25f),
		new Vector2 (0.75f, 0.25f),
		new Vector2 (-0.75f, -0.25f),
		new Vector2 (0.75f, -0.25f)
	};

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnGetHit (Bullet bullet) {
        GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll<Sprite> ("Textures/BattleCityAtlas").Single (s => s.name == "Base_2");
		GameController.OnBaseDestroyed ();
        bullet.Destroy ();
	}

	internal void Fortify () {
		DestroyWalls ();

		Transform steelWall = Resources.Load <Transform> ("Prefabs/SteelWall");

		foreach (var position in myWallPositions) {
			GameObject.Instantiate (steelWall, transform).localPosition = position;
		}
	}

	internal void UnFortify () {
		DestroyWalls ();

		Transform brickWall = Resources.Load <Transform> ("Prefabs/BrickWall");

		foreach (var position in myWallPositions) {
			GameObject.Instantiate (brickWall, transform).localPosition = position;
		}
	}

	private void DestroyWalls () {
		for (int i = 0; i < transform.childCount; i++) {
			GameObject.Destroy (transform.GetChild (i).gameObject);
		}
	}
}
