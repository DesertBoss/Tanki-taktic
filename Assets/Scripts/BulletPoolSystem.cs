using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;


public class BulletPoolSystem {
	private Bullet myBulletPrefab;
	private Transform myBulletsParent;
	private int myPoolSize;

	private List<Bullet> myActivePool;
	private List<Bullet> myReservPool;

	public BulletPoolSystem (Bullet bulletPrefab, Transform bulletsParent, int poolSize) {
		myBulletPrefab = bulletPrefab;
		myBulletsParent = bulletsParent;
		myPoolSize = poolSize;

		myActivePool = new List<Bullet> (myPoolSize);
		myReservPool = new List<Bullet> (myPoolSize);

		CreatePool ();
	}

	public void Clear () {
		myBulletPrefab = null;
		myBulletsParent = null;
		myActivePool.Clear ();
		myReservPool.Clear ();
		myActivePool = null;
		myReservPool = null;
	}

	private void CreatePool () {
		for (int i = 0; i < myPoolSize; i++) {
			Bullet obj = GameObject.Instantiate (myBulletPrefab, myBulletsParent);
			myReservPool.Add (obj);
			obj.gameObject.SetActive (false);
		}
	}

	public Bullet GetBullet () {
		//Bullet bullet = myReservPool.Find ((s) => { return !s.gameObject.activeSelf; });

		Bullet bullet = myReservPool[0];
		myReservPool.Remove (bullet);
		myActivePool.Add (bullet);
		bullet.gameObject.SetActive (true);
		return bullet;
	}

	public void RevertBullet (Bullet bullet) {
 		if (myReservPool.Contains (bullet)) return;

		myReservPool.Add (bullet);
		myActivePool.Remove (bullet);
		bullet.gameObject.SetActive (false);
	}

	public void ChangePoolSize (int newSize) {
		var oldPoolSize = myPoolSize;
		var oldActivePool = myActivePool;
		var oldReservPool = myReservPool;

		myPoolSize = newSize;
		myActivePool = new List<Bullet> (myPoolSize);
		myReservPool = new List<Bullet> (myPoolSize);

		for (int i = 0; i < myPoolSize; i++) {
			myActivePool.Add (oldActivePool[i]);
			myReservPool.Add (oldReservPool[i]);
		}

		for (int i = 0; i < myPoolSize - oldPoolSize; i++) {
			Bullet obj = GameObject.Instantiate (myBulletPrefab, myBulletsParent);
			myReservPool.Add (obj);
			obj.gameObject.SetActive (false);
		}
	}
}
