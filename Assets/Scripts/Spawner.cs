using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private Side mySide = Side.Enemy;
	[SerializeField] private GameObject mySpawnerLightPrefab;
	
	public Side Side { get => mySide; internal set => mySide = value; }


	private void Awake () {
		
	}

	private void OnDestroy () {
		mySpawnerLightPrefab = null;
	}

	void Start()
    {
		
	}

    
    void Update()
    {
        
    }

	public void Spawn (GameObject prefab, Transform parrent) {
		switch (mySide) {
			case Side.Player:
			StartCoroutine (SpawnPlayer (prefab, parrent));
			break;
			case Side.Enemy:
			StartCoroutine (SpawnEnemy (prefab, parrent));
			break;
		}
	}

	private IEnumerator SpawnPlayer (GameObject prefab, Transform parrent) {
		GameObject.Instantiate (mySpawnerLightPrefab, transform);
		yield return new WaitForSeconds (1.3f);

		Tank tank = GameObject.Instantiate (prefab, transform.position, Quaternion.identity, parrent).GetComponent<Tank> ();
		//t.transform.SetParent (_TanksContainer, true);

		InitContainer.instance.BonusController.OnTakeBonus (new HalmetEffect (tank, 3));
	}

	private IEnumerator SpawnEnemy (GameObject prefab, Transform parrent) {
		GameObject.Instantiate (mySpawnerLightPrefab, transform);
		yield return new WaitForSeconds (1.3f);

		GameObject.Instantiate (prefab, transform.position, Quaternion.identity, parrent);
		//t.transform.SetParent (_TanksContainer, true);
	}
}
