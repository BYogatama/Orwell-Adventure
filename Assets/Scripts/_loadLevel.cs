using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class _loadLevel : MonoBehaviour {

	public GameObject loadingScreen;
	public Slider loadBar;
	public Text progressText;

	void Start(){
		loadingScreen.SetActive (false);
	}

	public void LoadLevel(int sceneIndex){
		StartCoroutine (LoadAsychronously (sceneIndex));
	}

	IEnumerator LoadAsychronously (int sceneIndex){
		AsyncOperation loading = SceneManager.LoadSceneAsync (sceneIndex);

		loadingScreen.SetActive (true);

		while (!loading.isDone) {
			float progress = Mathf.Clamp01 (loading.progress / .9f);
			loadBar.value = progress;
			progressText.text = string.Format("{0:0.##}", progress * 100f)+" %";
			yield return null;
		}
	}
}
