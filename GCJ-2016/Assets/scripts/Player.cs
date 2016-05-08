using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, Mortal {

    enum States
    {
        IDLE = 0,
        ATTACK,
        PROTECT
    }

    public float actionRate = 0.1f;
    public int lifes = 5;
    public int nxtLife = 10;
    public AnimationClip death;
    public AudioClip atk;
    public AudioClip prtct;
    private int score = 0;
    private Animator animator;
    private float nextAction;
    private States state;
    private SpriteRenderer render;
    private AudioSource source;

	// Use this for initialization
	void Start () {
        render = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        source = gameObject.GetComponent<AudioSource>();
        nextAction = actionRate;
	}

	// Update is called once per frame
	void Update () {
        if (Time.time >= nextAction)
        {
            state = States.IDLE;
            if (Input.GetAxis("Attack") > 0)
            {
                animator.SetTrigger("attack");
                state = States.ATTACK;
                nextAction = Time.time + actionRate;
                source.PlayOneShot(atk);
            }
            else if (Input.GetAxis("Protect") > 0)
            {
                animator.SetTrigger("protect");
                state = States.PROTECT;
                nextAction = Time.time + actionRate;
                source.PlayOneShot(prtct);
            }
        }
	}

    public void Die()
    {
        StartCoroutine(dieanimation());
    }

    IEnumerator dieanimation()
    {
        animator.SetTrigger("dead");
        yield return new WaitForSeconds(death.length);
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Mortal  obj = other.gameObject.GetComponent<Mortal>();

        if ((other.gameObject.tag == "Ennemy" && state != States.ATTACK) ||
            (other.gameObject.tag == "Laser" && state != States.PROTECT))
        {
            --lifes;
            StartCoroutine(hitted());
        }
        else
            ++score;
        if (score >= nxtLife)
        {
            ++lifes;
            nxtLife *= 2;
        }
        if (obj != null)
        {
            obj.Die();
        }
    }

    IEnumerator hitted()
    {
        int composant;

        for (int i = 0; i < 6; i++)
        {
            composant = 255 * (i % 2);
            render.color = new Color(255, composant, composant);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public int getScore()
    {
        return (score);
    }

    public int getNextLife()
    {
        return (nxtLife);
    }
}
