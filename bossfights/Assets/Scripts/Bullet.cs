using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkewDirection
{
    upperLeft,
    upperRight,
    lowerLeft,
    lowerRight
};

public class Bullet : MonoBehaviour
{

    [SerializeField]
    private SkewDirection direction;

    [SerializeField]
    private bool activated = true;

    [SerializeField]
    private float speed = 1f;

    private Vector2 startingPosition;

    // Use this for initialization
    void Start ()
    {
        startingPosition = transform.position;
        StartCoroutine(BulletC());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        GameEvents.gameEvents.DisplayGameOver();
    }

    private IEnumerator BulletC()
    {
        float time = 0f;
        Vector2 temp;
        Vector2 dir = GetMovementVector();
        float startingPos = transform.localPosition.magnitude;
        //Debug.Log("Starting position: " + startingPos);
        float distanceMade = 0;
        float maxDistance = 10 * Mathf.Sqrt(2) / 2;
        while (activated)
        {
            yield return null;
            time += Time.deltaTime;
            temp = transform.position;
            temp += dir * speed;
            transform.position = temp;
            distanceMade = transform.localPosition.magnitude - startingPos;
            if(distanceMade > maxDistance)
            {
                //Debug.Log("Destroying: " + name);
                GameObject newInstance = Instantiate(gameObject, startingPosition, transform.rotation, transform.parent);
                newInstance.name = "Bullet";
                Destroy(gameObject);
            }
        }
    }

    private Vector2 GetMovementVector()
    {
        Vector2 outcome;
        switch(direction)
        {
            case SkewDirection.lowerLeft:
                outcome = new Vector2(-1, -1);
                break;
            case SkewDirection.lowerRight:
                outcome = new Vector2(1, -1);
                break;
            case SkewDirection.upperLeft:
                outcome = new Vector2(-1, 1);
                break;
            case SkewDirection.upperRight:
                outcome = new Vector2(1, 1);
                break;
            default:
                Debug.Log("Something wrong here");
                outcome = new Vector2(1, 1);
                break;
        }
        outcome.Normalize();
        return outcome;
    }
}
