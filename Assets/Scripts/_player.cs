using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Particles
{
    public GameObject playerBlood;
    public GameObject coinCollect;
}


public class _player : MonoBehaviour {

    //Object References
	private _soundManager soundManager;
    private _gameMaster gameMaster;
    private _levelMenu levelMenu;


    public Rigidbody2D rB2D;
    public Animator anim;
    
    public Particles particle;
    private bool isMirrored;
	public bool isGrounded;
	public bool isJump;
	public bool move;
	public bool isInAir;
    public bool doubleJump;
    
	public float direction;
	public int maxHealth = 100;
	public float movementSpeed;
	public float forceJump;

	[SerializeField]
	private Transform[] groundPoints;
	[SerializeField]
	private float groundRadius;
    [SerializeField]
    private LayerMask whatIsGround;

    

    void Awake()
    {
        gameMaster = FindObjectOfType<_gameMaster>();
        levelMenu = FindObjectOfType<_levelMenu>();
    }

    // Use this for initialization
    void Start () {
        //Object References
        rB2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        isMirrored = true;                          //Set Mirrored True (Player Face Right)
        gameMaster.curHealth = maxHealth;          //Set Current Health to MaxHealth

        //SoundManager Check
        soundManager = _soundManager.instance;
        if(soundManager == null)
        {
            Debug.Log("Sound Manager Not Found");
        }
	}
    
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = Grounded();
        if (move)
        {
            PlayerMovement(direction);
            Mirrored(direction);
        }
        else
        {
            PlayerMovement(horizontal);
            Mirrored(horizontal);
        }

        ResetValues();
        HandleInput();
        HandleJumpAnim();

        //Player Health
        if (gameMaster.curHealth > maxHealth) {
			gameMaster.curHealth = maxHealth;
		}
        //Player Death
		if (gameMaster.curHealth <= 0)
        {
            gameMaster.curHealth = 0;
            StartCoroutine(Death());
        }
    }

    //Player Controls
    private void HandleInput()
    {
		if (Input.GetKeyDown(KeyCode.Space) && !doubleJump)
        {
            isJump = true;
		}
		if (Input.GetKeyDown(KeyCode.Space) && doubleJump)
		{
			DoubleJump ();
		}

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            levelMenu.pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    //Player Movement
    private void PlayerMovement(float horizontal)
    {
		if (isGrounded) {
			isInAir = false;
            doubleJump = false;
		}

        if (rB2D.velocity.y < 0) //Landing From Jump
        {
            anim.SetBool("land", true);
        }

        if(isGrounded || isInAir) //Walking
        {
            rB2D.velocity = new Vector2(horizontal * movementSpeed, rB2D.velocity.y);
            anim.SetFloat("speed", Mathf.Abs(horizontal));
        }

        if (isGrounded && isJump && !doubleJump) //Jump
        {
            soundManager.PlaySound("Jump");
            isGrounded = false;
            doubleJump = true;
            isInAir = true;
            rB2D.AddForce(new Vector2(0, forceJump));
            anim.SetTrigger("jump");
        }
        
    }
    
    //DoubleJump
	public void DoubleJump(){
        doubleJump = false;
		soundManager.PlaySound("Jump");
		rB2D.AddForce(new Vector2(0, forceJump-400));
		anim.SetTrigger("jump");
	}

    //Fliping Player
    private void Mirrored(float horizontal)
    {
        if(horizontal > 0 && !isMirrored || horizontal < 0 && isMirrored)
        {
            isMirrored = !isMirrored;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    //Melakukan chek terhadap player apakah berada di Ground.
    private bool Grounded()
    {
        if(rB2D.velocity.y <= 0)
        {
            foreach(Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if(colliders[i].gameObject != gameObject)
                    {
                        anim.ResetTrigger("jump");
                        anim.SetBool("land", false);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    //Menghandle Animasi Jump.
    private void HandleJumpAnim()
    {
        if (!isGrounded)
        {
            anim.SetLayerWeight(1, 1);

        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }

    //Collision Trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Jika Player mengambil koin, koin akan dihancurkan dan menampilkan particle lalu skor bertambah.
        if (collision.CompareTag("Coin"))
		{
			soundManager.PlaySound("Coin");
            Instantiate(particle.coinCollect, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            gameMaster.koin += 1;
        }

		//Jika Player terkena Duri(Spikes) maka ...
        if (collision.CompareTag("Obstacle"))
		{
			//JIka player Health kurang dari 0 maka player akan kalah.
            if(gameMaster.curHealth < 0)					
            {
                gameMaster.curHealth = 0;
                StartCoroutine(Death());
			}
			//JIka tidak maka player health player akan berkurang.
            else if(gameMaster.curHealth > 0)
            {
				Instantiate(particle.playerBlood, transform.position, transform.rotation);
				soundManager.PlaySound ("Click");
				rB2D.AddForce(new Vector2(0,400));
				isInAir = true;
                gameMaster.curHealth -= 5; 
            }
        }

        //Jika Player Jatuh maka player akan kalah.
        if (collision.CompareTag("BottomBounds"))
        {
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        Instantiate(particle.playerBlood, transform.position, transform.rotation);
        this.enabled = false;
        this.GetComponent<Renderer>().enabled = false;
        yield return new WaitForSeconds(0.5f);
        levelMenu.gameOverMenu.SetActive (true);
        Time.timeScale = 0;
	}
    
	//Melakukan Reset values dari variable/mengembalikan nilai aslinya.
    private void ResetValues()
    {
        isJump = false;
    }
}