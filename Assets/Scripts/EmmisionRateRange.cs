using UnityEngine;
using System.Collections;

public class EmmisionRateRange : MonoBehaviour {

    public float LowRate = 1;
    public float HighRange = 20;

	// Use this for initialization
	void Start () {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.emissionRate = (int)Random.Range(LowRate, HighRange);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
