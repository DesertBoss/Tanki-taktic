using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour {
	[SerializeField] private GameObject[] _bonusPrefabs;

	private List<Effect> _effects;
	private Effect _effectInOrder;

	void Awake () {
		_effects = new List<Effect> ();
		_effectInOrder = null;
	}

	void Start () {
		
	}

	void FixedUpdate () {
		UpdateEffects ();
		CheckOrder ();
	}

	void Update () {

	}

	private void OnDestroy ()
	{
		_bonusPrefabs = null;
		_effectInOrder = null;
		_effects.Clear ();
		_effects = null;
	}

	private void UpdateEffects () {
		for (int i = 0; i < _effects.Count; i++) {
			Effect effect = _effects[i];
			if (_effectInOrder != null && _effectInOrder.BonusType == effect.BonusType)
				effect.Complete ();

			if (effect.Completed) {
				effect.End ();
				_effects.RemoveAt (i);
				i--;
				continue;
			}

			effect.Update (Time.fixedDeltaTime);
		}
	}

	private void CheckOrder () {
		if (_effectInOrder == null)
			return;

		_effectInOrder.Start ();
		_effects.Add (_effectInOrder);
		_effectInOrder = null;
	}

	public void OnTakeBonus (Effect effect) 
	{
		_effectInOrder = effect;
	}

	public void SpawnRandomBonus (Vector3 position) {
		Instantiate (_bonusPrefabs[Random.Range (0, _bonusPrefabs.Length)], position, Quaternion.identity, transform);
	}
}
