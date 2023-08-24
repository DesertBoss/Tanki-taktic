using UnityEngine;

public class GameZone : MonoBehaviour, IHitable {
	public bool OnGetHit (Bullet bullet) {
		return true;
	}
}
