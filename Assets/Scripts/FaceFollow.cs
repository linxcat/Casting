using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceFollow : MonoBehaviour {

    static Transform head;

    // Use this for initialization
    void Start () {
        head = GameObject.Find("CenterEyeAnchor").transform;
    }
	
	// Update is called once per frame
	void Update () {
        flatFace(transform, head);
	}

    void flatFace(Transform thing, Transform target) {
        thing.transform.forward = flattenFacing(target.forward);
    }

    public static Vector3 flattenFacing(Vector3 target) {
        return Vector3.ProjectOnPlane(target, Vector3.up);
    }

    public static Vector3 verticalFlattenFacing(Vector3 target) {
        Vector3 appropriateAxis;

        float upAmount = Vector3.Project(head.transform.up, Vector3.up).magnitude;
        float rightAmount = Vector3.Project(head.transform.right, Vector3.up).magnitude;

        if (rightAmount < upAmount) appropriateAxis = head.transform.right;
        else appropriateAxis = head.transform.up;

        Vector3 flattenedAxis = Vector3.ProjectOnPlane(appropriateAxis, Vector3.up);
        return Vector3.ProjectOnPlane(target, flattenedAxis);
    }
}
