using UnityEngine;

public class Monstermovement : MonoBehaviour
{
    public float moveSpeed = 2.0f;  // Adjust the speed as needed
    private Rigidbody2D rb;
    private bool isMovingLeft = true;
    [SerializeField] GameObject projectile;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
        // Check if the rabbit should change direction
        if (transform.position.x <= -10f)
        {
            isMovingLeft = false;
        }
        else if (transform.position.x >= 10f)
        {
            isMovingLeft = true;
        }

        // Move the rabbit left or right based on its direction
        if (isMovingLeft)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1); // Flip the sprite to face left
        }
        else
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            transform.localScale = new Vector3(1, 1, 1); // Flip the sprite to face right
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        SoundManager.instance.PlaySound(SoundManager.SoundType.MonsterDeath);
        Debug.Log("collison happend");
        PS_Manager.s_Instance.PlayParticlesSystem(transform.position.x -0.5f);
        Vector3 newPosition = new Vector3(14.11f,-1.23f,0f);
        transform.position = newPosition;
        transform.Rotate(0f,0f,0f,Space.World);
        Vector3 projectileRespawn = new Vector3(-8f,-1.55f,0f);
        projectile.transform.position = projectileRespawn;
        projectile.transform.Rotate(0f,0f,0f,Space.World);
        projectile.SetActive(false);
        gameObject.SetActive(false);
        
    }

}
