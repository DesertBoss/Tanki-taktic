using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IHitable {
	[SerializeField] private WallType _wallType;

	public WallType WallType { get => _wallType; }

	public bool OnGetHit (Bullet bullet) {
		if (_wallType == WallType.Tree && bullet.BulletPower != BulletPower.Forced) return false;
		if (_wallType == WallType.Steel && bullet.BulletPower == BulletPower.Normal) return true;

		gameObject.SetActive (false);
		return true;
	}
}
