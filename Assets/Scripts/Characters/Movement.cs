using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Transform target;
    public float speed = 1f;
    public bool flip = false;
    private Vector3 origin;
    private bool movingToTarget = true;

    private void Awake()
    {
        origin = transform.position;
    }

    private void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        Vector3 destination;

        while (true)
        {
            destination = movingToTarget ? target.position : origin;
            while (Vector3.Distance(destination, transform.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                yield return null;
            }

            movingToTarget = !movingToTarget;
            Flip();
        }
    }

    private void Flip()
    {
        if (flip)
        {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
