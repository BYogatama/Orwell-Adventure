using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _enemymove : MonoBehaviour {

    public int moveSpeed;
    public int moveDirection;

	// Update is called once per frame
	void Update () {
        Patrol();
	}

    public void Patrol()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveDirection, 0) * moveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBound"))
        {
            FlipEnemy();
        }
    }

    private void FlipEnemy()
    {
        if (moveDirection > 0)
        {
            moveDirection = -1;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if(moveDirection <= 0)
        {
            moveDirection = 1;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
