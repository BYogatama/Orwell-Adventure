using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class _levelMenu : MonoBehaviour {

    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject menuInformasi;
	public GameObject informasiSelesai;
	public AudioSource sourceMusik;
	public Slider volMusikControl;

	private float volMusik;
	private float volEfek;
	private _loadLevel loadLevel;
	private _soundManager soundManager;
	private _gameMaster gameMaster;
	static int sceneIndex;

	void Awake(){
		//Object References
		soundManager = FindObjectOfType<_soundManager> ();
		loadLevel = FindObjectOfType<_loadLevel> ();
		gameMaster = FindObjectOfType<_gameMaster> ();
	}

	void Start()
    {	
		//Panel Menu Deactive
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        menuInformasi.SetActive(false);
		informasiSelesai.SetActive (false);
		//Volume Musik
		VolumeControl ();
		//Get Scene
		sceneIndex = SceneManager.GetActiveScene ().buildIndex;
    }

	public void vControl(){
		sourceMusik.volume = volMusikControl.value;
	}

    /* Level Button Event Handler */
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);

		saveVolumeSetting ();
    }

    public void BukaInfo()
    {
        Time.timeScale = 0;
        menuInformasi.SetActive(true);
	}

	public void TutupInfo()
	{
		Time.timeScale = 1;
		menuInformasi.SetActive(false);
	}

	public void Ulangi(){
		Time.timeScale = 1;
		for (int i = 1; i <= 3; i++) {
			if (SceneManager.GetActiveScene ().buildIndex == i*2) {
				SceneManager.LoadScene ("Level"+i);
			}
		}
		PlayerPrefs.SetInt("Score", 0);
	}

	public void Lanjut(){
		Time.timeScale = 1;
		loadLevel.LoadLevel (sceneIndex + 1);
		SaveScore ();
	}

    public void Keluar()
    {
        Time.timeScale = 1;
		loadLevel.LoadLevel(0);
    }
    /* End of Level Button Event Handler */

	// Check Volume Value and Save
	public void VolumeControl(){
		if (PlayerPrefs.HasKey ("vMusik") && PlayerPrefs.HasKey ("vEfek")) {
			volMusik = PlayerPrefs.GetFloat ("vMusik");
			volEfek = PlayerPrefs.GetFloat ("vEfek");

			volMusikControl.value = volMusik;
			soundManager.volEfekControl.value = volEfek;

		} else {
			volMusikControl.value = 1;
			soundManager.volEfekControl.value = 1; 
		}
	}

	// Saving Value of Volume
	public void saveVolumeSetting(){
		PlayerPrefs.SetFloat ("vMusik", volMusikControl.value);
		PlayerPrefs.SetFloat ("vEfek", soundManager.volEfekControl.value);
	}

	//Menghitung dan menyimpan total skor.
	private void SaveScore()
	{
		int score = gameMaster.koin * gameMaster.curHealth;
		PlayerPrefs.SetInt("Score", score);
	}
}