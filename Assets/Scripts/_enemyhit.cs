using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _enemyhit : MonoBehaviour
{
    _gameMaster gameMaster;
	_soundManager soundManager;

    private void Start()
    {
		gameMaster = FindObjectOfType<_gameMaster>();
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
			soundManager.PlaySound ("Click");
            gameMaster.curHealth -= 10;
			gameMaster.koin += 50;
        }
    }
        
}
