using UnityEngine;

public class _destroyParticle : MonoBehaviour {

    private ParticleSystem thisParticle;

	// Use this for initialization
	void Start () {
        thisParticle = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!thisParticle.isPlaying)
        {
            Destroy(gameObject);
        }

	}
}
