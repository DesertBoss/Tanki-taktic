using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Tank : MonoBehaviour, IHitable {
	[SerializeField] protected int _health = 1;
	[SerializeField] protected int _speed = 10;
	[SerializeField] protected int _speedMult = 500;
	[SerializeField] protected int _bulletSpeed = 10;
	[SerializeField] protected int _bulletSpeedMult = 20;
	[SerializeField] protected int _maxBullets = 1;
	protected int _curBullets = 0;

	[SerializeField] protected Side _side = Side.Enemy;
	protected BulletPower _gunPower = BulletPower.Normal;

	[SerializeField] protected Transform _gun;
	[SerializeField] protected GameObject _explosionPrefab;
	protected BulletFactory _bulletFactory;
	protected Rigidbody2D _rigidbody2D;
	protected Animator _animator;

	protected Vector2 _moveDirection;
	protected float _rotation;
	
	public int Health { get => _health; set => _health = value; }
	public int Speed { get => _speed; set => _speed = value; }
	public int CurBullets { get => _curBullets; set => _curBullets = value; }
	public Side Side { get => _side; set => _side = value; }
	public BulletPower GunPower { get => _gunPower; set => _gunPower = value; }
	public Transform Gun { get => _gun; }

	protected virtual void Awake () {
		_rigidbody2D = GetComponent<Rigidbody2D> ();
		_animator = GetComponent<Animator> ();
		_bulletFactory = Glabal.BulletFactory;

		_moveDirection = new Vector2 (0, 0);
		_rotation = transform.rotation.z;

		_animator.speed = 0;
	}

	protected void OnDestroy () {
		_gun = null;
		_explosionPrefab = null;
		_bulletFactory = null;
		_rigidbody2D = null;
		_animator = null;
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
		if (_moveDirection == Vector2.zero) return;

		_rotation = Vector2.SignedAngle (Vector2.up, _moveDirection);
		transform.rotation = Quaternion.Euler (0, 0, _rotation);
	}

	private void Move()
	{
		_rigidbody2D.AddForce (_moveDirection * _speed * _speedMult * Time.deltaTime);
	}

	private void CheckMove()
	{
		if (_moveDirection != Vector2.zero)
			_animator.speed = 1;
		else
			_animator.speed = 0;
	}

	protected void Shoot () 
	{
		if (_curBullets >= _maxBullets) return;

		Bullet bullet = _bulletFactory.SpawnBullet (this, _gun.position, transform.rotation);
		//bullet.Owner = this;
		bullet.Rigidbody2D.AddForce (_gun.TransformDirection (Vector2.up) * (_bulletSpeed * _bulletSpeedMult));

		_curBullets++;
	}

	public bool OnGetHit (Bullet bullet) {
		if (bullet.Side == _side) return false;
		_health--;
		if (_health <= 0) {
			Destroy ();
		}
		return true;
	}

	public virtual void Destroy () {
		Animator explosion = Instantiate (_explosionPrefab, transform.position, Quaternion.identity).GetComponent<Animator>();
		explosion.SetBool ("Small", false);
		GameObject.Destroy (gameObject);
	}
}
