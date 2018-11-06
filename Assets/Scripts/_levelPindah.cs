using UnityEngine;
using UnityEngine.SceneManagement;

public class _levelPindah : MonoBehaviour {

	private _levelMenu levelMenu;

	void Awake(){
		levelMenu = FindObjectOfType<_levelMenu> ();
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.CompareTag("Player")){
			levelMenu.informasiSelesai.SetActive (true);
			Time.timeScale = 0;
		}
	}
}
