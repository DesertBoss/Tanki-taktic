using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IHitable {
	private Tank myOwner;
	private Side mySide = Side.Enemy;
	private BulletPower myBulletPower = BulletPower.Normal;
	private Rigidbody2D myRigidbody2D;
	[SerializeField] private GameObject _ExplosionPrefab;
	
	public Tank Owner { get => myOwner;
		set {
			myOwner = value;
			mySide = value.Side;
			myBulletPower = value.GunPower;
		} }
	public Side Side { get => mySide; }
	public BulletPower BulletPower { get => myBulletPower; }
	public Rigidbody2D Rigidbody2D { get => myRigidbody2D; }

	private void Awake()
	{
		myRigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnDestroy () {
		myOwner = null;
		myRigidbody2D = null;
		_ExplosionPrefab = null;
	}

	private void OnTriggerEnter2D (Collider2D collision) {
		//if (myOwner != null && collision.name == myOwner.name) return;

		bool hit = true;

		if (collision.TryGetComponent(out IHitable target)) hit = target.OnGetHit (this);

		if (hit) Destroy ();
	}

	public bool OnGetHit (Bullet bullet) {
		if (bullet.Side == mySide) return false;
		else return true;
		//Destroy ();
	}

	public void Destroy () {
		if (myOwner != null) myOwner.CurBullets -= 1;
		myOwner = null;
		Instantiate (_ExplosionPrefab, transform.position, Quaternion.identity);

		InitContainer.instance.BulletFactory.DestroyBullet (this);
	}
}
