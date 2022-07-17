using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	public GameController GameController { get; internal set; }

	private int myMapSizeX = 13;
	private int myMapSizeY = 13;

	private List <Vector2> myBaseCoord = new List<Vector2> {
		new Vector2 (5, 0),
		new Vector2 (6, 0),
		new Vector2 (7, 0),
		new Vector2 (5, 1),
		new Vector2 (6, 1),
		new Vector2 (7, 1),
		new Vector2 (6, 2)
	};

	private List <Vector2> myBlackListCoord = new List<Vector2> ();


	

    void Start()
    {
        
    }

    void Update()
    {
        
    }

	public void GenerateNewMap () {
		Wall brickWall = Resources.Load <Wall> ("Prefabs/BrickWall");
		Wall steelWall = Resources.Load <Wall> ("Prefabs/SteelWall");
		Wall treeWall = Resources.Load <Wall> ("Prefabs/TreeWall");
		Spawner spawner = Resources.Load <Spawner> ("Prefabs/Spawner");

		brickWall.WallType = WallType.Brick;
		steelWall.WallType = WallType.Steel;
		treeWall.WallType = WallType.Tree;
		spawner.Side = Side.Enemy;

		// Void
		for (int i = 0; i < Random.Range (20, 30); i++) {
			int lenght = Random.Range (4, myMapSizeY);
			bool dirY = Random.Range (1, 100) > 50 ? true : false;
			Vector2 startPos = new Vector2 (7, 0);
			while (myBlackListCoord.Contains (startPos)) {
				startPos = new Vector2 (Random.Range (0, myMapSizeX - 1), Random.Range (0, myMapSizeY - 1));
			}

			if (dirY) {
				for (int j = 0; j < lenght; j++) {
					Vector2 pos = startPos + new Vector2 (0, startPos.y + j);
					if (pos.y > myMapSizeY) continue;
					myBlackListCoord.Add (pos);
				}
			} else {
				for (int j = 0; j < lenght; j++) {
					Vector2 pos = startPos + new Vector2 (startPos.x + j, 0);
					if (pos.x > myMapSizeX) continue;
					myBlackListCoord.Add (pos);
				}
			}
		}

		// BrickWall and SteelWall
		for (int x = 0; x < myMapSizeX; x++) {
			for (int y = 0; y < myMapSizeY; y++) {
				Vector2 position = new Vector2 (x, y);
				if (myBlackListCoord.Contains (position)) continue;
				if (myBaseCoord.Contains (position)) continue;

				if (Random.Range (1, 100) > 10)
					PlaceWall (brickWall, position);
				else
					PlaceWall (steelWall, position);
			}

		}

		// TreeWall
		for (int i = 0; i < myBlackListCoord.Count; i++) {
			if (myBaseCoord.Contains (myBlackListCoord[i])) continue;
			if (Random.Range (1, 100) > 70)
				PlaceWall (treeWall, myBlackListCoord[i]);
		}

		// Spawns
		for (int i = 0; i < myBlackListCoord.Count; i++) {
			if (myBaseCoord.Contains (myBlackListCoord[i])) continue;
			if (Random.Range (1, 100) > 95)
				GameObject.Instantiate (spawner, myBlackListCoord[i] + new Vector2 (0.5f, 0.5f), Quaternion.identity).transform.SetParent (GameController.EnemySpawns, true);
		}

		GameObject.Destroy (gameObject);
	}

	private void PlaceWall (Wall wall, Vector2 position) {
		GameObject.Instantiate (wall, position + new Vector2 (0.25f, 0.25f), Quaternion.identity).transform.SetParent (GameController.Walls, true);
		GameObject.Instantiate (wall, position + new Vector2 (0.25f, 0.75f), Quaternion.identity).transform.SetParent (GameController.Walls, true);
		GameObject.Instantiate (wall, position + new Vector2 (0.75f, 0.25f), Quaternion.identity).transform.SetParent (GameController.Walls, true);
		GameObject.Instantiate (wall, position + new Vector2 (0.75f, 0.75f), Quaternion.identity).transform.SetParent (GameController.Walls, true);
	}
}
