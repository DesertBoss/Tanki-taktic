using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	[SerializeField] private Side _side = Side.Enemy;
	[SerializeField] private GameObject _spawnerLightPrefab;
	
	public Side Side { get => _side; internal set => _side = value; }

	private void OnDestroy () {
		_spawnerLightPrefab = null;
	}

	public void Spawn (GameObject prefab, Transform parrent) {
		switch (_side) {
			case Side.Player:
			StartCoroutine (SpawnPlayer (prefab, parrent));
			break;
			case Side.Enemy:
			StartCoroutine (SpawnEnemy (prefab, parrent));
			break;
		}
	}

	private IEnumerator SpawnPlayer (GameObject prefab, Transform parrent) {
		GameObject.Instantiate (_spawnerLightPrefab, transform);
		yield return new WaitForSeconds (1.3f);

		Tank tank = GameObject.Instantiate (prefab, transform.position, Quaternion.identity, parrent).GetComponent<Tank> ();
		//t.transform.SetParent (_TanksContainer, true);

		Glabal.BonusController.OnTakeBonus (new HalmetEffect (tank, 3));
	}

	private IEnumerator SpawnEnemy (GameObject prefab, Transform parrent) {
		GameObject.Instantiate (_spawnerLightPrefab, transform);
		yield return new WaitForSeconds (1.3f);

		GameObject.Instantiate (prefab, transform.position, Quaternion.identity, parrent);
		//t.transform.SetParent (_TanksContainer, true);
	}
}
