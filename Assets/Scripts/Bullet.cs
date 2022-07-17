using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, IHitable {
	private Tank myOwner;
	private Side mySide = Side.Enemy;
	private BulletPower myBulletPower = BulletPower.Normal;

	public Tank Owner { get => myOwner; internal set => myOwner = value; }
	public Side Side { get => mySide; internal set => mySide = value; }
	public BulletPower BulletPower { get => myBulletPower; internal set => myBulletPower = value; }

	private void OnTriggerEnter2D (Collider2D collision) {
		if (myOwner != null && collision.name == myOwner.name) return;

		IHitable target = collision.gameObject.GetComponent<IHitable> ();

		if (target != null) target.OnGetHit (this);
	}

	public void OnGetHit (Bullet bullet) {
		if (bullet.Side == mySide) return;
		Destroy ();
	}

	public void Destroy () {
		if (myOwner != null) myOwner.CurBullets -= 1;
		myOwner = null;
		Instantiate (Resources.Load<GameObject> ("Prefabs/Explosion"), transform.position, Quaternion.identity);

		GameObject.Destroy (gameObject);
	}
}

public enum BulletPower {
	Normal,
	Extended,
	Forced
}