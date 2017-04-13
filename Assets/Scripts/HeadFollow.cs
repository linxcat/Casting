using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadFollow : MonoBehaviour {

    static Transform head;
    Vector3 BIAS;

	// Use this for initialization
	void Start () {
        head = GameObject.Find("CenterEyeAnchor").transform;
        BIAS = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = biasedPosition(head.position);
	}

    Vector3 biasedPosition(Vector3 originalPosition) {
        Vector3 newSpot = new Vector3();
        newSpot.x = originalPosition.x + BIAS.x;
        newSpot.y = originalPosition.y + BIAS.y;
        newSpot.z = originalPosition.z + BIAS.z;
        return newSpot;
    }

    public void calibrate(Vector3 point) {
        BIAS = point - head.position;
    }
}
