using System;
using UnityEngine;

public class Glabal : MonoBehaviour {
	private static Glabal _instance;

	[SerializeField] private GameZone _gameZone;
	[SerializeField] private BonusController _bonusController;
	[SerializeField] private SpawnController _spawnController;
	[SerializeField] private PlayerService _playerService;
	[SerializeField] private MainBase _mainBase;
	[SerializeField] private BulletFactory _bulletFactory;
	[SerializeField] private PlayerInfoPanel _playerInfoPanel;
	[SerializeField] private SettingsController _settingsController;

	public static GameZone GameZone => _instance._gameZone;
	public static BonusController BonusController => _instance._bonusController;
	public static SpawnController SpawnController => _instance._spawnController;
	public static BulletFactory BulletFactory => _instance._bulletFactory;
	public static MainBase MainBase => _instance._mainBase;
	public static PlayerInfoPanel PlayerInfoPanel => _instance._playerInfoPanel;
	public static PlayerService PlayerService => _instance._playerService;


	void Awake () {
		_instance = this;
	}

	void OnDestroy () {
		_instance = null;

		_gameZone = null;
		_bonusController = null;
		_spawnController = null;
		_playerService = null;
		_mainBase = null;
		_bulletFactory = null;
		_playerInfoPanel = null;
	}
}
