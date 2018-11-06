using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _playerEndlessRun : MonoBehaviour {

	public float moveSpeed;
	public float jumpForce;

    public Transform rayMid;

    public float range;

    private Vector2 dir = new Vector2(1, 0);

	private Rigidbody2D playerRB2D;

	// Use this for initialization
	void Start () {
		playerRB2D = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        playerRB2D.velocity = new Vector2(moveSpeed, playerRB2D.velocity.y);

        Debug.DrawRay(rayMid.position, dir);
        RaycastHit2D hit = Physics2D.Raycast(rayMid.position, dir, range);
        if(hit == true)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                playerRB2D.AddForce(new Vector2(0, jumpForce));
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRB2D.AddForce(new Vector2(0, jumpForce));
        }
    }

    private void FixedUpdate()
    {
    }
}
