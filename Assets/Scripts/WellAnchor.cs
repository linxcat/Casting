using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellAnchor : MonoBehaviour {

    public float stickiness = 0.25F;

    public static Vector3[] offsets = new Vector3[5];
    private static bool[] wellAnchored = new bool[5];

    // Use this for initialization
    void Start () {
        offsets[0] = new Vector3(-0.3f, -0.4f, 0.2f);
        offsets[1] = new Vector3(-0.18f, -0.4f, 0.35f);
        offsets[2] = new Vector3(0, -0.4f, 0.4f);
        offsets[3] = new Vector3(0.18f, -0.4f, 0.35f);
        offsets[4] = new Vector3(0.3f, -0.4f, 0.2f);

        for (int i = 0; i < Wells.wells.Length; i++) {
            wellAnchored[i] = true;
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < Wells.wells.Length; i++) {
            if (wellAnchored[i]) {
                Wells.wells[i].transform.position = wellNewPosition(i);
            }
        }
	}

    private Vector3 wellNewPosition(int i) {
        float attenuatedStickiness = stickiness; // TODO outer are slower, use modulo
        Vector3 wellTarget = transform.position + offsets[i];
        return Vector3.Lerp(Wells.wells[i].transform.position, wellTarget, attenuatedStickiness);
    }

    public static void stopWellFollow(Wells.WELL_COLOUR colour) {
        wellAnchored[(int)colour] = false;
    }

    public static void startWellFollow(Wells.WELL_COLOUR colour) {
        wellAnchored[(int)colour] = true;
    }

    public static bool wellIsAnchored(Wells.WELL_COLOUR colour) {
        return wellAnchored[(int)colour];
    }
}