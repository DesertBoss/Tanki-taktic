using System;
using System.Collections.Generic;
using UnityEngine;


public class BulletFactory : MonoBehaviour {
	[SerializeField] private Bullet _BulletPrefab;
	[SerializeField] private Transform _BulletsParent;
	[SerializeField] private int _PoolSize = 50;

	private BulletPoolSystem myBulletPoolSystem;

	private void Awake () {
		myBulletPoolSystem = new BulletPoolSystem (_BulletPrefab, _BulletsParent, _PoolSize);
	}

	private void OnDestroy () {
		_BulletPrefab = null;
		_BulletsParent = null;
		myBulletPoolSystem.Clear ();
		myBulletPoolSystem = null;
	}

	public Bullet SpawnBullet (Tank owner, Vector3 position, Quaternion rotation) {
		Bullet bullet = myBulletPoolSystem.GetBullet ();
		bullet.transform.position = position;
		bullet.transform.rotation = rotation;
		bullet.Owner = owner;
		bullet.Rigidbody2D.velocity = Vector2.zero;
		bullet.Rigidbody2D.WakeUp ();
		bullet.gameObject.SetActive (true);
		return bullet;
	}

	public void DestroyBullet (Bullet bullet) {
		myBulletPoolSystem.RevertBullet (bullet);
	}
}