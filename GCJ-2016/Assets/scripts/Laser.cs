using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    public Transform focus;
    public Transform projectile;
    public float fireStrength = 5f;
    private Vector3 direction;
    private Vector3 position;
    private Quaternion rotate;

	// Use this for initialization
	void Start () {
        if (focus != null)
            setFocus(focus);
        position = transform.position + new Vector3(0, -0.2f, 0);
	}

    public void setFocus(Transform focustoset)
    {
        focus = focustoset;
        direction = focus.position - transform.position;
        rotate.Set(direction.x, 0, direction.y, 0);
    }

    public void Shoot()
    {
        if (focus != null)
            StartCoroutine(shootProjectile());
    }

    IEnumerator shootProjectile()
    {
        GameObject shoot = Instantiate(projectile, position, rotate) as GameObject;

        shoot.transform.rotation = transform.rotation;
        shoot.GetComponent<Rigidbody2D>().AddForce(direction * fireStrength);
        yield return new WaitForSeconds(1f);
        Destroy(shoot);
    }
}
