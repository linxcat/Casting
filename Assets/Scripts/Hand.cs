using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    Wells wells;

    GameObject inHand;
    Transform tapWell;

    Dictionary<string, Collider> tapTargets;
    bool wellRaise = false;

    // Use this for initialization
    void Start () {
        wells = GameObject.Find("WellAnchor").GetComponent<Wells>();
        tapTargets = new Dictionary<string, Collider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (inHand != null) {
            inHand.transform.position = transform.position;
        }
	}

    void OnTriggerEnter(Collider Other) {
        tapTargets.Add(Other.name, Other);
    }

    void OnTriggerExit(Collider Other) {
        tapTargets.Remove(Other.name);
    }

    public void drag() {
        if (wellRaise) {
            Vector3 dragVector = transform.position - tapWell.position;
            if (FaceFollow.verticalFlattenFacing(dragVector).magnitude >= Wells.RAISE_DISTANCE) {
                wells.oneUp();
                wellRaise = false; //don't spam per-frame
            }
        }
    }

    public void tap() {
        GameObject target = Wells.prioritizeTapTarget(tapTargets);
        if (target == null) return;

        switch (Wells.getTapPriority(target.tag)) {
            case Wells.TAP_PRIORITY.WellTap:
                startWellRaise(target);
                break;

            case Wells.TAP_PRIORITY.Well:
                tapAWell(target);
                break;

            case Wells.TAP_PRIORITY.TapTarget:
                tapTarget(target);
                break;

            default:
                Debug.LogError("Invalid Hand Tap Target.");
                break;
        }
    }

    public void untap() {
        if (inHand != null && inHand.tag.Equals("Well")) {
            WellAnchor.startWellFollow(Wells.translateColourFromName(inHand.name));
        }
        release();
    }

    void startWellRaise(GameObject target) {
        wellRaise = true;
        tapWell = target.transform;
        Invoke("expireRaise", Wells.RAISE_EXPIRY);
    }

    void expireRaise() {
        wellRaise = false;
    }

    void tapAWell(GameObject well) {
        grab(well);
        WellAnchor.stopWellFollow(Wells.translateColourFromName(well.name));
    }

    void tapTarget(GameObject target) {
        //TODO message sending
    }

    public void grab(GameObject thing) {
        inHand = thing;
    }

    public GameObject getHeldObject() {
        return inHand;
    }

    public void release() {
        inHand = null;
        wellRaise = false;
    }
}
