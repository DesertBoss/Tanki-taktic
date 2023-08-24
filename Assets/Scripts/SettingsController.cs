using UnityEngine;

public class SettingsController : MonoBehaviour
{
	[SerializeField] private bool _vSync = true;
	[SerializeField] private bool _fullscreen = false;
	[SerializeField] private int _windowWidth = 1280;
	[SerializeField] private int _windowHeight = 720;
	[SerializeField] private int _framerateLimite = -1;

	private void Awake ()
	{
		SetWindowSettings ();
	}

	private void SetWindowSettings ()
	{
		Screen.SetResolution (_windowWidth, _windowHeight, _fullscreen);
		Application.targetFrameRate = _vSync ? -1 : _framerateLimite;
		QualitySettings.vSyncCount = _vSync ? 1 : 0;
	}
}
