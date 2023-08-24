using System.Collections.Generic;
using UnityEngine;


public class BulletPoolSystem {
	private Bullet _bulletPrefab;
	private Transform _bulletsParent;
	private int _poolSize;

	private List<Bullet> _activePool;
	private List<Bullet> _reservPool;

	public BulletPoolSystem (Bullet bulletPrefab, Transform bulletsParent, int poolSize) {
		_bulletPrefab = bulletPrefab;
		_bulletsParent = bulletsParent;
		_poolSize = poolSize;

		_activePool = new List<Bullet> (_poolSize);
		_reservPool = new List<Bullet> (_poolSize);

		CreatePool ();
	}

	public void Clear () {
		_bulletPrefab = null;
		_bulletsParent = null;
		_activePool.Clear ();
		_reservPool.Clear ();
		_activePool = null;
		_reservPool = null;
	}

	private void CreatePool () {
		for (int i = 0; i < _poolSize; i++) {
			Bullet obj = GameObject.Instantiate (_bulletPrefab, _bulletsParent);
			_reservPool.Add (obj);
			obj.gameObject.SetActive (false);
		}
	}

	public Bullet GetBullet () {
		//Bullet bullet = _reservPool.Find ((s) => { return !s.gameObject.activeSelf; });

		Bullet bullet = _reservPool[0];
		_reservPool.Remove (bullet);
		_activePool.Add (bullet);
		bullet.gameObject.SetActive (true);
		return bullet;
	}

	public void RevertBullet (Bullet bullet) {
 		if (_reservPool.Contains (bullet)) return;

		_reservPool.Add (bullet);
		_activePool.Remove (bullet);
		bullet.gameObject.SetActive (false);
	}

	public void ChangePoolSize (int newSize) {
		var oldPoolSize = _poolSize;
		var oldActivePool = _activePool;
		var oldReservPool = _reservPool;

		_poolSize = newSize;
		_activePool = new List<Bullet> (_poolSize);
		_reservPool = new List<Bullet> (_poolSize);

		for (int i = 0; i < _poolSize; i++) {
			_activePool.Add (oldActivePool[i]);
			_reservPool.Add (oldReservPool[i]);
		}

		for (int i = 0; i < _poolSize - oldPoolSize; i++) {
			Bullet obj = GameObject.Instantiate (_bulletPrefab, _bulletsParent);
			_reservPool.Add (obj);
			obj.gameObject.SetActive (false);
		}
	}
}
