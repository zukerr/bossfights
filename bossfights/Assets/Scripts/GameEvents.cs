using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour {

    [SerializeField]
    private GameObject gameOverRef;

    public static GameEvents gameEvents;
	// Use this for initialization
	void Start ()
    {
        gameEvents = this;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void DisplayGameOver()
    {
        gameOverRef.SetActive(true);
    }
}
