using UnityEngine;
using System.Collections;

public class DisableTimer : MonoBehaviour {
    public float timeout;
	// Use this for initialization
	void Start () {
        Invoke("DisableObject", timeout);
	}
	
	// Update is called once per frame
	void DisableObject() {
        gameObject.SetActive(false);
	}
}
