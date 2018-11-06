using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
 * Digunakan untuk menghandle UI di Menu Level
 */
public class _gameMaster : MonoBehaviour
{
    public int koin;
    public int curHealth;

    public Text totalHealth;
    public Text totalKoin;

    private _player player;

    void Update()
    {
        totalKoin.text = ("" + koin);
        totalHealth.text = ("" + curHealth);
    }

    void FixedUpdate()
    {
        player = FindObjectOfType<_player>();
    }

    /* Touch Screen Controls */
    public void BtnJump()
    {
		player.anim.SetTrigger("jump");
		player.isJump = true;
		if (player.doubleJump) {
			player.DoubleJump();
		}
    }

    public void BtnMove(float direction)
    {
        player.direction = direction;
        player.move = true;
    }

    public void BtnStopMove()
    {
        player.direction = 0;
        player.move = false;
    }
    /* End of Touch Screen Controls */
}