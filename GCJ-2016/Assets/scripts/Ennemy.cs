using UnityEngine;
using System.Collections;

public class Ennemy : MonoBehaviour, Mortal {
    public Transform focus;
    public float speed = 0.2f;
    public AnimationClip death;
    private bool dead = false;
    private Animator animator;
    private float distanceLength;
    private float startTime;
    private Vector3 start;
	
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        startTime = Time.time;
        start = transform.position;
        distanceLength = Vector3.Distance(start, focus.position);
    }

	// Update is called once per frame
	void Update () {
        if (!dead && focus != null)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fracJourney = distCovered / distanceLength;
            transform.position = Vector3.Lerp(start, focus.position, fracJourney);
        }
	}

    public void Pause()
    {
        dead = true;
    }

    public void Resume()
    {
        dead = false;
    }

    public void Die()
    {
        StartCoroutine(dieAndDestroy());
    }

    IEnumerator dieAndDestroy()
    {
        dead = true;
        animator.SetTrigger("dead");
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        yield return new WaitForSeconds(death.length);
        Destroy(gameObject);
    }
}
