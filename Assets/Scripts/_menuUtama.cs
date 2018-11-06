using UnityEngine;
using UnityEngine.SceneManagement;


public class _menuUtama : MonoBehaviour {
    
	public static _menuUtama menuUtama;

    [SerializeField]
    private string pilihPindahScene;
    
    public GameObject menuBantuan;
    public GameObject menuTentang;

	private _loadLevel loadLevel;

    private void Start()
    {
		loadLevel = FindObjectOfType<_loadLevel> ();
        menuBantuan.SetActive(false);
        menuTentang.SetActive(false);
    }

	void FixedUpdate(){
		HandleInput ();
	}

    /* Memilih Scene */
    public void LevelPilih()
	{       
		loadLevel.LoadLevel(1);
    }

    //Script Bantuan
    public void BtnBantuanBuka()
    {
        menuBantuan.SetActive(true);
    }
    public void BtnBantuanTutup()
    {
        menuBantuan.SetActive(false);
    }

    //Tentang
    public void BtnTentangBuka()
    {
        menuTentang.SetActive(true);
    }
    public void BtnTentangTutup()
    {
        menuTentang.SetActive(false);
    }

    //Keluar Aplikasi
    public void Keluar()
	{
        Application.Quit();
    }

	private void HandleInput()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Keluar ();
		}
	}
}
