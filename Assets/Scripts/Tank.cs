using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
public class Tank : MonoBehaviour, IHitable {
	[SerializeField] protected int _Health = 1;
	[SerializeField] protected int _Speed = 10;
	[SerializeField] protected int _SpeedMult = 500;
	[SerializeField] protected int _BulletSpeed = 10;
	[SerializeField] protected int _BulletSpeedMult = 20;
	[SerializeField] protected int _MaxBullets = 1;
	protected int myCurBullets = 0;

	[SerializeField] protected Side _Side = Side.Enemy;
	protected BulletPower myGunPower = BulletPower.Normal;

	[SerializeField] protected Transform _Gun;
	[SerializeField] protected GameObject _ExplosionPrefab;
	protected BulletFactory _BulletFactory;
	protected Rigidbody2D myRigidbody2D;
	protected Animator myAnimator;

	protected Vector2 myMoveDirection;
	protected float myRotation;
	
	public int Health { get => _Health; set => _Health = value; }
	public int Speed { get => _Speed; set => _Speed = value; }
	public int CurBullets { get => myCurBullets; set => myCurBullets = value; }
	public Side Side { get => _Side; set => _Side = value; }
	public BulletPower GunPower { get => myGunPower; set => myGunPower = value; }
	public Transform Gun { get => _Gun; }

	protected virtual void Awake () {
		myRigidbody2D = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		_BulletFactory = InitContainer.instance.BulletFactory;

		myMoveDirection = new Vector2 (0, 0);
		myRotation = transform.rotation.z;

		myAnimator.speed = 0;
	}

	protected void OnDestroy () {
		_Gun = null;
		_ExplosionPrefab = null;
		_BulletFactory = null;
		myRigidbody2D = null;
		myAnimator = null;
	}

	protected virtual void Start () {
		
	}

	protected virtual void Update ()
	{
		ChangeRotation();
		CheckMove();
		Move();
	}

	private void ChangeRotation()
	{
		if (myMoveDirection == Vector2.zero) return;

		myRotation = Vector2.SignedAngle (Vector2.up, myMoveDirection);
		transform.rotation = Quaternion.Euler (0, 0, myRotation);
	}

	private void Move()
	{
		myRigidbody2D.AddForce (myMoveDirection * _Speed * _SpeedMult * Time.deltaTime);
	}

	private void CheckMove()
	{
		if (myMoveDirection != Vector2.zero)
			myAnimator.speed = 1;
		else
			myAnimator.speed = 0;
	}

	protected void Shoot () 
	{
		if (myCurBullets >= _MaxBullets) return;

		Bullet bullet = _BulletFactory.SpawnBullet (this, _Gun.position, transform.rotation);
		//bullet.Owner = this;
		bullet.Rigidbody2D.AddForce (_Gun.TransformDirection (Vector2.up) * (_BulletSpeed * _BulletSpeedMult));

		myCurBullets++;
	}

	public bool OnGetHit (Bullet bullet) {
		if (bullet.Side == _Side) return false;
		_Health--;
		if (_Health <= 0) {
			Destroy ();
		}
		return true;
	}

	public virtual void Destroy () {
		Animator explosion = Instantiate (_ExplosionPrefab, transform.position, Quaternion.identity).GetComponent<Animator>();
		explosion.SetBool ("Small", false);
		GameObject.Destroy (gameObject);
	}
}
