using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{

    [SerializeField]
    private bool activated = true;

    [SerializeField]
    private float speed = 1f;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(LaserC());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        GameEvents.gameEvents.DisplayGameOver();
    }

    private IEnumerator LaserC()
    {
        float time = 0f;
        float tempRot = 0f;
        while (activated)
        {
            yield return null;
            time += Time.deltaTime;


            tempRot = time * speed;

            /*
            if(tempRot >= 360f)
            {
                tempRot = 0f;
            }
            */

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, tempRot));
        }
    }
}
