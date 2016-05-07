using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour, Mortal {

    private Animator animator;
    public AnimationClip explosion;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
	}

    public void Die()
    {
        StartCoroutine(destroyProjectile());
    }

    IEnumerator destroyProjectile()
    {
        Destroy(rb);
        animator.SetTrigger("explode");
        yield return new WaitForSeconds(explosion.length);
        Destroy(gameObject);
    }
}
