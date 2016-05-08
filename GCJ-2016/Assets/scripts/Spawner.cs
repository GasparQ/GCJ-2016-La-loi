using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public Ennemy ennemy;
    public Transform focus;

    public void Spawn()
    {
        Ennemy newone = Instantiate(ennemy, transform.position, transform.rotation) as Ennemy;

        newone.focus = focus;
    }

    public void Clear()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Ennemy");

        foreach (GameObject obj in objects)
        {
            obj.GetComponent<Ennemy>().Die();
        }
    }

    public void Pause()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Ennemy");

        foreach (GameObject obj in objects)
        {
            obj.GetComponent<Ennemy>().Pause();
        }
    }

    public void Resume()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Ennemy");

        foreach (GameObject obj in objects)
        {
            obj.GetComponent<Ennemy>().Resume();
        }
    }
}
