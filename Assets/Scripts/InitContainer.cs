using System;
using UnityEngine;

public class InitContainer : MonoBehaviour {
	public static InitContainer instance;

	[SerializeField] private GameZone _GameZone;
	[SerializeField] private BonusController _BonusController;
	[SerializeField] private SpawnController _SpawnController;
	[SerializeField] private PlayerService _PlayerService;
	[SerializeField] private Base _Base;
	[SerializeField] private BulletFactory _BulletFactory;
	[SerializeField] private PlayerInfoPanel _PlayerInfoPanel;


	public GameZone GameZone => _GameZone;
	public BonusController BonusController => _BonusController;
	public SpawnController SpawnController => _SpawnController;
	public BulletFactory BulletFactory => _BulletFactory;
	public Base Base => _Base;
	public PlayerInfoPanel PlayerInfoPanel => _PlayerInfoPanel;
	public PlayerService PlayerService => _PlayerService;


	void Awake () {
		instance = this;
	}

	void OnDestroy () {
		instance = null;

		_GameZone = null;
		_BonusController = null;
		_SpawnController = null;
		_PlayerService = null;
		_Base = null;
		_BulletFactory = null;
		_PlayerInfoPanel = null;
	}
}
