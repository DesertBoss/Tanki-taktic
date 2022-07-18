using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameZone : MonoBehaviour, IHitable {
	void Start () {

	}

	void FixedUpdate () {
		
	}

	void Update () {

	}

	public bool OnGetHit (Bullet bullet) {
		return true;
	}
}
