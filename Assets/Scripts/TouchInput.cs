using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : MonoBehaviour {

    Hand leftHand;
    bool leftTapped = false;
    bool leftDragging = false;

    Hand rightHand;
    bool rightTapped = false;
    bool rightDragging = false;

	// Use this for initialization
	void Start () {
        leftHand = GameObject.Find("LeftHand").GetComponent<Hand>();
        rightHand = GameObject.Find("RightHand").GetComponent<Hand>();
    }
	
	// Update is called once per frame
	void Update () {
        checkCalibrate();
        updateTapState();
        handleTapping();
    }

    void checkCalibrate() {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick) && OVRInput.Get(OVRInput.Button.SecondaryThumbstick)
            || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick) && OVRInput.Get(OVRInput.Button.PrimaryThumbstick)) {
            Vector3 difference = (leftHand.transform.position - rightHand.transform.position) / 2;
            Vector3 newPoint = rightHand.transform.position + difference;
            GameObject.Find("TapAnchor").SendMessage("calibrate", newPoint);
        }
    }

    void updateTapState()
    {
        bool leftTapping = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger);
        bool rightTapping = OVRInput.Get(OVRInput.Button.SecondaryHandTrigger);

        leftDragging = (leftTapped && leftTapping);
        rightDragging = (rightTapped && rightTapping);

        leftTapped = leftTapping;
        rightTapped = rightTapping;
    }

    void handleTapping() {
        if (leftDragging) leftHand.drag();
        else if (leftTapped) leftHand.tap();
        else leftHand.untap();

        if (rightDragging) rightHand.drag();
        else if (rightTapped) rightHand.tap();
        else rightHand.untap();
    }
}
