using UnityEngine;

public class _cameraFollow : MonoBehaviour {

    private Vector2 velocity;
    private GameObject player;

    //Camera Bounds Var
    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;
    public float cameraSmoothX;
    public float cameraSmoothY;
    
    public bool cameraBounds;
    	
    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (cameraBounds)
        {
            float xPos = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, cameraSmoothX);
            float yPos = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, cameraSmoothY);

            transform.position = new Vector3(xPos, yPos, transform.position.z);

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPos.x, maxCameraPos.x),
                Mathf.Clamp(transform.position.y, minCameraPos.y, maxCameraPos.y),
                Mathf.Clamp(transform.position.z, minCameraPos.z, maxCameraPos.z));
        }

    }
}
