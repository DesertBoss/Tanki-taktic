using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IHitable {
	[SerializeField] private WallType myWallType;

	public WallType WallType { get => myWallType; internal set => myWallType = value; }

	public void OnGetHit (Bullet bullet) {
		switch (WallType) {
			case WallType.Steel: {
				if (bullet.BulletPower != BulletPower.Normal) goto default;
				else bullet.Destroy ();
			}
			break;
			case WallType.Tree: {
				if (bullet.BulletPower == BulletPower.Extended) goto default;
			}
			break;
			case WallType.Brick:
			default: {
				bullet.Destroy ();
				GameObject.Destroy (gameObject);
			}
			break;
		}
	}
}
public enum WallType {
	Brick,
	Steel,
	Tree
}