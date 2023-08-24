using UnityEngine;


public class BulletFactory : MonoBehaviour {
	[SerializeField] private Bullet _bulletPrefab;
	[SerializeField] private Transform _bulletsParent;
	[SerializeField] private int _poolSize = 50;

	private BulletPoolSystem _bulletPoolSystem;

	private void Awake () {
		_bulletPoolSystem = new BulletPoolSystem (_bulletPrefab, _bulletsParent, _poolSize);
	}

	private void OnDestroy () {
		_bulletPrefab = null;
		_bulletsParent = null;
		_bulletPoolSystem.Clear ();
		_bulletPoolSystem = null;
	}

	public Bullet SpawnBullet (Tank owner, Vector3 position, Quaternion rotation) {
		Bullet bullet = _bulletPoolSystem.GetBullet ();
		bullet.transform.position = position;
		bullet.transform.rotation = rotation;
		bullet.Owner = owner;
		bullet.Rigidbody2D.velocity = Vector2.zero;
		bullet.Rigidbody2D.WakeUp ();
		bullet.gameObject.SetActive (true);
		return bullet;
	}

	public void DestroyBullet (Bullet bullet) {
		_bulletPoolSystem.RevertBullet (bullet);
	}
}