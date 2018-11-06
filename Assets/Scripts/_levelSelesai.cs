using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class _levelSelesai : MonoBehaviour {
	
	public Text totalSkor;

    private int score;
    private int highScore;
	private int volMusik;
	private _loadLevel loadLevel;
	private _levelMenu levelMenu;
    
	void Awake(){
		loadLevel = FindObjectOfType<_loadLevel> ();
	}

	// Use this for initialization
	void Start () {
        score = PlayerPrefs.GetInt("Score");
		totalSkor.text = ("" + score);
    }

    public void Reload()
    {
		loadLevel.LoadLevel(SceneManager.GetActiveScene ().buildIndex-1);
    }

    public void Lanjut()
    {
		loadLevel.LoadLevel(SceneManager.GetActiveScene ().buildIndex+1);
    }

    public void Keluar()
    {
        SceneManager.LoadScene("MenuUtama");
    }
}
