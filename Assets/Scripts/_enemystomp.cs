using UnityEngine;

public class _enemystomp : MonoBehaviour
{

	private _soundManager soundManager;
    
	public GameObject bloodParticle;

	void Start(){
		//SoundManager Check
		soundManager = _soundManager.instance;
		if(soundManager == null)
		{
			Debug.Log("Sound Manager Not Found");
		}
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
		{
			soundManager.PlaySound ("Stomp");
			Instantiate(bloodParticle, transform.position, transform.rotation);
			Destroy (transform.parent.gameObject);
        }
    }
}
