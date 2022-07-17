using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tank : MonoBehaviour, IHitable {
	[SerializeField] protected int myHealth = 1;
	[SerializeField] protected int mySpeed = 10;
	[SerializeField] protected int myBulletSpeed = 10;
	[SerializeField] protected int myMaxBullets = 1;
	protected int myCurBullets = 0;

	protected Side mySide = Side.Enemy;
	protected BulletPower myGunPower = BulletPower.Normal;

	protected Transform myGun;
	protected Rigidbody2D myBullet;
	protected Rigidbody2D myBody;

	protected Animator myAnimator;

	protected Vector2 myMoveDirection;
	protected float myRotation;
	

	public int Health { get => myHealth; internal set => myHealth = value; }
	public int Speed { get => mySpeed; internal set => mySpeed = value; }
	public int CurBullets { get => myCurBullets; internal set => myCurBullets = value; }
	public Side Side { get => mySide; internal set => mySide = value; }
	public BulletPower GunPower { get => myGunPower; internal set => myGunPower = value; }

	protected virtual void Start () {
		myGun = transform.Find ("Gun");
		myBullet = Resources.Load<Rigidbody2D> ("Prefabs/Bullet");
		myBody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		myMoveDirection = new Vector2 (0, 0);
		myRotation = transform.rotation.z;

		myAnimator.speed = 0;
	}

	protected virtual void Update () {
		// Сделать логику вычисления угла через синус и косинус
		if (myMoveDirection == Vector2.up) {
			myRotation = 0;
		} else
		if (myMoveDirection == Vector2.left) {
			myRotation = 90;
		} else
		if (myMoveDirection == Vector2.right) {
			myRotation = -90;
		} else
		if (myMoveDirection == Vector2.down) {
			myRotation = 180;
		}

		if (myMoveDirection != Vector2.zero)
			myAnimator.speed = 1;
		else
			myAnimator.speed = 0;
		transform.rotation = Quaternion.Euler (0, 0, myRotation);
		myBody.AddForce (myMoveDirection * mySpeed * 500 * Time.deltaTime);
	}

	protected void Shoot () {
		if (myCurBullets >= myMaxBullets) return;
		Rigidbody2D bulletRigid = Instantiate(myBullet, myGun.position, transform.rotation);
		Bullet bulletInstance = bulletRigid.GetComponent<Bullet> ();
		bulletInstance.Owner = this;
		bulletInstance.Side = mySide;
		bulletInstance.BulletPower = myGunPower;

		bulletRigid.AddForce (myGun.TransformDirection (Vector2.up) * (myBulletSpeed * 20));
		myCurBullets++;
	}

	public void OnGetHit (Bullet bullet) {
		if (bullet.Side == mySide) return;
		myHealth--;
		bullet.Destroy ();
		if (myHealth <= 0) {
			Destroy ();
		}
	}

	internal virtual void Destroy () {
		Animator explosion = Instantiate (Resources.Load<Animator> ("Prefabs/Explosion"), transform.position, Quaternion.identity);
		explosion.SetBool ("Small", false);
		GameObject.Destroy (gameObject);
	}

	protected void SpawnBonus () {
		SpawnBonus ((BonusType)Random.Range (1, 7));
	}

	protected void SpawnBonus (BonusType type) {
		Bonus bonus = Instantiate (Resources.Load<Bonus> ("Prefabs/Bonus"), transform.position, Quaternion.identity);
		bonus.BonusType = type;
		bonus.GetComponent<SpriteRenderer> ().sprite = Resources.LoadAll <Sprite> ("Textures/BattleCityAtlas").Single (s => s.name == $"Bonus_{(int)type}");
	}
}

public enum Side {
	Player = 0,
	Enemy = 1
}