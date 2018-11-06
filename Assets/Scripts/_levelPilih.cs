using UnityEngine;
using UnityEngine.SceneManagement;

public class _levelPilih : MonoBehaviour {

	private _loadLevel loadLevel;

	void Start(){
		loadLevel = FindObjectOfType<_loadLevel> ();
	}

	void FixedUpdate(){
		HandleInput ();
	}

    public void MenuUtama()
    {
		loadLevel.LoadLevel(0);
    }

    /* Perpindahan Level melalui Button */
    public void LevelSatu()
    {
		loadLevel.LoadLevel (2);
        PlayerPrefs.SetInt("Score", 0);
    }

    public void LevelDua()
    {
		loadLevel.LoadLevel (4);
        PlayerPrefs.SetInt("Score", 0);
    }

    public void LevelTiga()
    {
		loadLevel.LoadLevel (6);
        PlayerPrefs.SetInt("Score", 0);
    }
    /*End of Perpindahan Level*/

	private void HandleInput()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			MenuUtama ();
		}
	}
}
