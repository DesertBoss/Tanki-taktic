using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour, IHitable {
	private static int Score = 0;
	private static int ExtraLifes = 2;
	private static int Dificulty = 1;
	private static bool PlayerKilled = false;
	private static bool BaseDestroyed = false;
	public static bool UseRandomMap = false;

	private List<Spawner> myEnemySpawners;
	private Spawner myPlayerSpawner;
	private int myWavesCount = 20;
	private float myTimeToEnemySpawn = 3;
	private int myKilledEnemyes = 0;
	private int myCurrentEnemyes = 0;
	private int myMaxEnemyes = 4;

	[SerializeField] private Transform myCanvas;
	private GameObject myPlayerPrefab;
	private GameObject[] myEnemyPrefabs;
	private Dictionary<string, Transform> myUIElements;

	public Transform PlayerSpawn { get; private set; }
	public Transform EnemySpawns { get; private set; }
	public Transform EnemyTanks { get; private set; }
	public Transform BonusController { get; private set; }
	public Transform Base { get; private set; }
	public Transform Walls { get; private set; }
	public Transform Ground { get; private set; }
	public Transform Canvas { get; private set; }


	void Start () {
		PlayerKilled = false;
		BaseDestroyed = false;

		BonusController = transform.Find ("BonusController");
		Base = transform.Find ("Base");
		PlayerSpawn = transform.Find ("PlayerSpawn");
		EnemySpawns = transform.Find ("EnemySpawns");
		EnemyTanks = transform.Find ("EnemyTanks");
		Walls = transform.Find ("Walls");
		Ground = transform.Find ("Ground");
		Canvas = myCanvas;

		myEnemySpawners = new List<Spawner> ();
		myPlayerSpawner = PlayerSpawn.GetComponent <Spawner> ();

		//if (UseRandomMap) PrepareForMapGenerator ();

		for (int i = 0; i < EnemySpawns.childCount; i++) {
			myEnemySpawners.Add (EnemySpawns.GetChild (i).GetComponent<Spawner> ());
		}

		myPlayerPrefab = Resources.Load<GameObject> ("Prefabs/PlayerTank");
		myEnemyPrefabs = new GameObject[] {
			Resources.Load<GameObject> ("Prefabs/LightTankBot"),
			Resources.Load<GameObject> ("Prefabs/FastTankBot"),
			Resources.Load<GameObject> ("Prefabs/RapidTankBot"),
			Resources.Load<GameObject> ("Prefabs/FatTankBot")
		};

		myUIElements = new Dictionary<string, Transform> ();
		for (int i = 0; i < myCanvas.childCount; i++) {
			Transform t = myCanvas.GetChild (i);
			myUIElements.Add (t.name, t);
		}

		myPlayerSpawner.Spawn (myPlayerPrefab);
		StartCoroutine (StartSpawn ());
	}

	void FixedUpdate () {
		if (BaseDestroyed) return;
		if (PlayerKilled && ExtraLifes > 0) {
			myPlayerSpawner.Spawn (myPlayerPrefab);
			PlayerKilled = false;
		}
		if (Input.GetKeyDown (KeyCode.Escape)) SceneManager.LoadScene ("MainMenu");
	}

	void Update () {

	}

	void OnGUI () {
		myUIElements["Score"].GetComponentInChildren<Text> ().text = $"Счет: {Score}";
		myUIElements["Lifes"].GetComponentInChildren<Text> ().text = $"Жизней: {ExtraLifes}";
		myUIElements["Enemies"].GetComponentInChildren<Text> ().text = $"Врагов: {myWavesCount - myKilledEnemyes}";
		if (BaseDestroyed || ExtraLifes < 1) myUIElements["GameOver"].gameObject.SetActive (true);
	}

	public static void AddScore (int score) {
		Score += score;
	}

	public void AddScore (Vector2 position, int score) {
		AddScore (score);

		GameObject tmp = GameObject.Instantiate (Resources.Load<GameObject> ("Prefabs/ScoreGetter"), myCanvas);
		tmp.transform.position = position;
		tmp.GetComponentInChildren<Text> ().text = score.ToString ();
	}

	public void OnGetHit (Bullet bullet) {
		bullet.Destroy ();
	}

	public static void OnGetExtraLife () {
		ExtraLifes++;
	}

	public static void OnPlayerKilled () {
		PlayerKilled = true;
		ExtraLifes--;
	}

	public void OnEnemyKilled () {
		myCurrentEnemyes--;
		myKilledEnemyes++;
	}

	public static void OnBaseDestroyed () {
		BaseDestroyed = true;
	}

	private void PrepareForMapGenerator () {
		Transform trash = new GameObject ("Trash").transform;

		for (int i = 0; i < EnemySpawns.childCount; i++) {
			//GameObject.Destroy (EnemySpawns.GetChild (i).gameObject);
			EnemySpawns.GetChild (i).SetParent (trash);
		}
		for (int i = 0; i < Walls.childCount; i++) {
			//GameObject.Destroy (Walls.GetChild (i).gameObject);
			Walls.GetChild (i).SetParent (trash);
		}
		for (int i = 0; i < Ground.childCount; i++) {
			//GameObject.Destroy (Ground.GetChild (i).gameObject);
			Ground.GetChild (i).SetParent (trash);
		}

		trash.gameObject.SetActive (false);
		GameObject.Destroy (trash.gameObject);

		MapGenerator mapGenerator = new GameObject ("MapGenerator").AddComponent<MapGenerator> ();
		mapGenerator.GameController = this;
		mapGenerator.GenerateNewMap ();
	}

	private IEnumerator StartSpawn () {
		for (int i = 0; i < myWavesCount; i++) {
			int spawnerNum = Random.Range (0, myEnemySpawners.Count);
			myEnemySpawners[spawnerNum].Spawn (myEnemyPrefabs[Random.Range (0, myEnemyPrefabs.Length)]);
			myCurrentEnemyes++;
			while (myCurrentEnemyes >= myMaxEnemyes) yield return new WaitForSeconds (1);
			if (BaseDestroyed) break;
			yield return new WaitForSeconds (myTimeToEnemySpawn / Dificulty);
		}
	}
}
