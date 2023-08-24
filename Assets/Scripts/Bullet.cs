using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour, IHitable {
	private Tank _owner;
	private Side _side = Side.Enemy;
	private BulletPower _bulletPower = BulletPower.Normal;
	private Rigidbody2D _rigidbody2D;
	[SerializeField] private GameObject _explosionPrefab;
	
	public Tank Owner { get => _owner;
		set {
			_owner = value;
			_side = value.Side;
			_bulletPower = value.GunPower;
		} }
	public Side Side { get => _side; }
	public BulletPower BulletPower { get => _bulletPower; }
	public Rigidbody2D Rigidbody2D { get => _rigidbody2D; }

	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnDestroy () {
		_owner = null;
		_rigidbody2D = null;
		_explosionPrefab = null;
	}

	private void OnTriggerEnter2D (Collider2D collision) {
		//if (_owner != null && collision.name == _owner.name) return;

		bool hit = true;

		if (collision.TryGetComponent(out IHitable target))
			hit = target.OnGetHit (this);

		if (hit)
			Destroy ();
	}

	public bool OnGetHit (Bullet bullet) {
		if (bullet.Side == _side) return false;
		else return true;
		//Destroy ();
	}

	public void Destroy () {
		if (_owner != null) _owner.CurBullets -= 1;
		_owner = null;
		Instantiate (_explosionPrefab, transform.position, Quaternion.identity);

		Glabal.BulletFactory.DestroyBullet (this);
	}
}
