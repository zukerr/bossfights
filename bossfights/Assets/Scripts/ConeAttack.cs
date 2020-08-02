using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConeAttack : MonoBehaviour
{
    private Image img;
    private PolygonCollider2D myCollider;

    [SerializeField]
    private float attackFrequency = 5f;

    // Use this for initialization
	void Start ()
    {
        img = GetComponent<Image>();
        myCollider = GetComponent<PolygonCollider2D>();
        myCollider.enabled = false;
        StartCoroutine(ConeC());
    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameEvents.gameEvents.DisplayGameOver();
    }

    private void GenerateRandomCone()
    {
        float rVal = Random.Range(0.15f, 0.25f);
        int rQuarter = Random.Range(0, 4) + 1;

        rQuarter *= 90;

        Debug.Log(rQuarter);
        img.fillAmount = rVal;
        //transform.rotation = new Quaternion(0, transform.rotation.y, rQuarter, transform.rotation.w);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rQuarter));
        CalculateCollider(rVal);
    }

    private void CalculateCollider(float percentage)
    {
        percentage -= 0.125f;
        float angle1 = percentage * 360f;
        Debug.Log("Angle1: " + angle1);

        float angle2 = 45f;
        
        float angle3 = 180f - angle1 - angle2;
        Debug.Log("Angle3: " + angle3);

        float side3 = 5 * Mathf.Sqrt(2);
        Debug.Log("Side3: " + side3);

        float side1 = (side3 / Mathf.Sin(angle3 / 180 * Mathf.PI)) * Mathf.Sin(angle1 / 180 * Mathf.PI);
        Debug.Log("Side1: " + side1);

        float nextY = -5f + side1;

        Vector2[] temp = myCollider.points;

        //myCollider.points[2].Set(-5f, nextY);
        temp[2].Set(-5f, nextY);
        myCollider.SetPath(0, temp);
    }

    private IEnumerator ConeC()
    {
        float time = 0f;
        img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
        Color normal = img.color;
        while(time < attackFrequency)
        {
            yield return null;
            float alpha = time / attackFrequency;
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
            time += Time.deltaTime;
        }
        time = 0f;
        img.color = Color.red;
        myCollider.enabled = true;
        while (time < 0.1f)
        {
            yield return null;
            time += Time.deltaTime;
        }
        myCollider.enabled = false;
        img.color = normal;
        GenerateRandomCone();
        StartCoroutine(ConeC());
    }

}
