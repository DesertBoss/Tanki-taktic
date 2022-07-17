using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusController : MonoBehaviour {
	List<Effect> myEffects;
	Effect myEffectInOrder;

	void Start () {
		myEffects = new List<Effect> ();
		myEffectInOrder = null;
	}

	void FixedUpdate () {
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

		if (myEffectInOrder != null) {
			myEffectInOrder.Start ();
			myEffects.Add (myEffectInOrder);
			myEffectInOrder = null;
		}
	}

	void Update () {

	}

	public void OnTakeBonus (Tank owner, BonusType bonus) {
		Effect effect = null;

		switch (bonus) {
			case BonusType.Helmet:
			effect = new HalmetEffect (owner);
			break;
			case BonusType.Watch:
			effect = new WatchEffect (this);
			break;
			case BonusType.Shovel:
			effect = new ShovelEffect (this);
			break;
			case BonusType.Star:
			effect = new StarEffect (owner);
			break;
			case BonusType.Grenade:
			effect = new GrenadeEffect (this);
			break;
			case BonusType.ExtraLife:
			effect = new ExtraLifeEffect ();
			break;
			case BonusType.Pistol:
			effect = new PistolEffect (owner);
			break;
			case BonusType.None:
			default: return;
		}

		myEffectInOrder = effect;
	}

	public void OnTakeBonus (Tank owner, BonusType bonus, float duration) {
		Effect effect = null;

		switch (bonus) {
			case BonusType.Helmet:
			effect = new HalmetEffect (owner, duration);
			break;
			case BonusType.Watch:
			effect = new WatchEffect (this, duration);
			break;
			case BonusType.Shovel:
			effect = new ShovelEffect (this, duration);
			break;
			case BonusType.Star:
			effect = new StarEffect (owner);
			break;
			case BonusType.Grenade:
			effect = new GrenadeEffect (this);
			break;
			case BonusType.ExtraLife:
			effect = new ExtraLifeEffect ();
			break;
			case BonusType.Pistol:
			effect = new PistolEffect (owner, duration);
			break;
			case BonusType.None:
			default: return;
		}

		myEffectInOrder = effect;
	}
}
