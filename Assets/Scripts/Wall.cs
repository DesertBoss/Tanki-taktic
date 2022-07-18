using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IHitable {
	[SerializeField] private WallType myWallType;

	public WallType WallType { get => myWallType; }

	public bool OnGetHit (Bullet bullet) {
		if (myWallType == WallType.Tree && bullet.BulletPower != BulletPower.Forced) return false;
		if (myWallType == WallType.Steel && bullet.BulletPower == BulletPower.Normal) return true;

		gameObject.SetActive (false);
		return true;
	}
}
