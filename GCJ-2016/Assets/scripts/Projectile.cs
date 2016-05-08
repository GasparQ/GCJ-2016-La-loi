using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour, Mortal {

    private Animator animator;
    public AnimationClip explosion;
    public AudioClip shoot;
    public AudioClip hit;
    private Rigidbody2D rb;
    private AudioSource source;

	// Use this for initialization
	void Start () {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        source = gameObject.GetComponent<AudioSource>();
        source.PlayOneShot(shoot);
	}

    public void Die()
    {
        StartCoroutine(destroyProjectile());
    }

    IEnumerator destroyProjectile()
    {
        Destroy(rb);
        animator.SetTrigger("explode");
        source.PlayOneShot(hit);
        yield return new WaitForSeconds(explosion.length);
        Destroy(gameObject);
    }
}
