using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterDetect : MonoBehaviour
{
    private float distance = 1.5f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, distance);

        // Check if the ray hits something
        if (hit.collider != null)
        {
            // Check if the hit object has the specified tag
            if (hit.collider.CompareTag("Monster"))
            {
                //Load Try Again
                SoundManager.instance.PlaySound(SoundManager.SoundType.TryAgain);
                SceneManager.LoadScene("tryAgainScene");
            }
        }
    }
}
