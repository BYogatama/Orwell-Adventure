using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Sound{
	public string nama;
	public AudioClip clipAudio;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

	[Range(0f,0.5f)]
	public float randomVolume = 0.1f;
	[Range(0f,0.5f)]
	public float randomPitch = 0.1f;

	private AudioSource source;

	public void SetSource(AudioSource _source){
		source = _source;
		source.clip = clipAudio;
	}

	public void Play(){
        source.volume = volume * (1+Random.Range(-randomVolume/2f,randomVolume/2f));
		source.pitch = pitch * (1+Random.Range(-randomPitch/2f,randomPitch/2f));;
		source.Play ();
	}
}

public class _soundManager : MonoBehaviour {

	public static _soundManager instance;

    public Slider volEfekControl;
    [SerializeField]
	Sound[] sounds;

    void Awake(){
		if (instance != null) {
			Debug.LogError ("Ada Lebih dari satu Sound Manager !");
		} else {
			instance = this;
		}
	}

	void Start(){
		for (int i = 0; i < sounds.Length; i++) {
			GameObject _go = new GameObject ("Sounds_" + i + "_" + sounds [i].nama);
			_go.transform.SetParent (transform);
			sounds [i].SetSource(_go.AddComponent<AudioSource>());
		}
	}

    public void PlaySound(string _nama){
		for (int i = 0; i < sounds.Length; i++) {
			if (sounds [i].nama == _nama) {
				sounds[i].volume = volEfekControl.value;
				sounds [i].Play();
				return;
			}
		}
		//No Sounds
		Debug.LogWarning("Sound Manager : Sound or Audio Not Found : "+_nama);
	}
}