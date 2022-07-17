using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private Side mySide = Side.Enemy;
	private GameController myGameController;
	private GameObject mySpawnerLight;
	
	public Side Side { get => mySide; internal set => mySide = value; }
    
    void Start()
    {
		myGameController = transform.root.GetComponent <GameController> ();

		mySpawnerLight = Resources.Load<GameObject> ("Prefabs/SpawnLight");
	}

    
    void Update()
    {
        
    }

	internal void Spawn (GameObject prefab) {
		switch (mySide) {
			case Side.Player:
			StartCoroutine (SpawnPlayer (prefab));
			break;
			case Side.Enemy:
			StartCoroutine (SpawnEnemy (prefab));
			break;
		}
	}

	private IEnumerator SpawnPlayer (GameObject prefab) {
		GameObject.Instantiate (mySpawnerLight, transform);
		yield return new WaitForSeconds (1.3f);

		GameObject t = GameObject.Instantiate (prefab, transform.position, Quaternion.identity);
		t.transform.SetParent (myGameController.transform, true);

		myGameController.BonusController.GetComponent<BonusController> ().OnTakeBonus (t.GetComponent<Tank> (), BonusType.Helmet, 3);
	}

	private IEnumerator SpawnEnemy (GameObject prefab) {
		GameObject.Instantiate (mySpawnerLight, transform);
		yield return new WaitForSeconds (1.3f);

		GameObject t = GameObject.Instantiate (prefab, transform.position, Quaternion.identity);
		t.transform.SetParent (myGameController.EnemyTanks, true);
	}
}
