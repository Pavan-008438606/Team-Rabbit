using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    [SerializeField] Transform Monster;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, Monster.position, speed * Time.deltaTime);
        transform.position = newPosition;
        transform.Rotate(-transform.forward * 50 * Time.deltaTime);
    }
}
