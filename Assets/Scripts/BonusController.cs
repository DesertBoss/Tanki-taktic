using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BonusController : MonoBehaviour {
	[SerializeField] GameObject[] _BonusPrefabs;

	List<Effect> myEffects;
	Effect myEffectInOrder;

	void Awake () {
		myEffects = new List<Effect> ();
		myEffectInOrder = null;
	}

	private void OnDestroy () {
		_BonusPrefabs = null;
		myEffectInOrder = null;
		myEffects.Clear ();
		myEffects = null;
	}

	void Start () {
		
	}

	void FixedUpdate () {
		UpdateEffects ();
		CheckOrder ();
	}

	void Update () {

	}

	private void UpdateEffects () {
		for (int i = 0; i < myEffects.Count; i++) {
			Effect effect = myEffects[i];
			if (myEffectInOrder != null && myEffectInOrder.BonusType == effect.BonusType)
				effect.Completed = true;

			if (effect.Completed) {
				effect.OnDestroy ();
				myEffects.RemoveAt (i);
				continue;
			}

			effect.Update (Time.fixedDeltaTime);
		}
	}

	private void CheckOrder () {
		if (myEffectInOrder != null) {
			myEffectInOrder.Start ();
			myEffects.Add (myEffectInOrder);
			myEffectInOrder = null;
		}
	}

	public void OnTakeBonus (Effect effect) 
	{
		myEffectInOrder = effect;
	}

	public void SpawnRandomBonus (Vector3 position) {
		Instantiate (_BonusPrefabs[Random.Range (0, _BonusPrefabs.Length)], position, Quaternion.identity, transform);
	}
}
