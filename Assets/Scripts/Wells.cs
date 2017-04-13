using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wells : MonoBehaviour {

    public static GameObject[] wells = null;

    public enum WELL_COLOUR { Red, Blue, Green, Yellow, Purple };
    Color red = new Color(0.6f, 0, 0);
    Color blue = new Color(0.18f, 0.42f, 1);
    Color green = new Color(0, 0.44f, 0.06f);
    Color yellow = new Color(1, 1, 0.6f);
    Color purple = new Color(0.38f, 0, 0.55f);
    public enum TAP_PRIORITY { WellTap, Well, TapTarget };

    public const float WELL_LIFETIME = 5.3F;
    private float lifeTimer;

    public const float RAISE_EXPIRY = 0.4F;
    public const float RAISE_DISTANCE = 0.5F;
    private bool oneIsUp = false;
    private bool wellsUp = false;

    // Use this for initialization
    void Start () {
        GameObject RedWell = GameObject.Find("RedWell");
        GameObject BlueWell = GameObject.Find("BlueWell");
        GameObject GreenWell = GameObject.Find("GreenWell");
        GameObject YellowWell = GameObject.Find("YellowWell");
        GameObject PurpleWell = GameObject.Find("PurpleWell");
        RedWell.GetComponent<Renderer>().material.SetColor("_Color", red);
        BlueWell.GetComponent<Renderer>().material.SetColor("_Color", blue);
        GreenWell.GetComponent<Renderer>().material.SetColor("_Color", green);
        YellowWell.GetComponent<Renderer>().material.SetColor("_Color", yellow);
        PurpleWell.GetComponent<Renderer>().material.SetColor("_Color", purple);
        wells = new[] { RedWell, BlueWell, GreenWell, YellowWell, PurpleWell };
    }

    void Update() {
        lifeTimer += Time.unscaledDeltaTime;
        if (lifeTimer > WELL_LIFETIME) wellsUp = false;
        up(wellsUp);
    }

    public static WELL_COLOUR translateColourFromName(string wellName) {
        switch (wellName) {
            case "RedWell":
                return WELL_COLOUR.Red;

            case "BlueWell":
                return WELL_COLOUR.Blue;

            case "GreenWell":
                return WELL_COLOUR.Green;

            case "YellowWell":
                return WELL_COLOUR.Yellow;

            case "PurpleWell":
                return WELL_COLOUR.Purple;

            default:
                Debug.Log("Colour translation used incorrectly!");
                return (WELL_COLOUR)int.MaxValue;
        }
    }

    public static TAP_PRIORITY getTapPriority(string query) {
        switch (query) {
            case "WellTap":
                return TAP_PRIORITY.WellTap;
            case "Well":
                return TAP_PRIORITY.Well;
            case "TapTarget":
                return TAP_PRIORITY.TapTarget;
            default:
                return (TAP_PRIORITY)int.MaxValue;
        }
    }

    public static GameObject prioritizeTapTarget(Dictionary<string, Collider> dict) {
        if (dict.Count == 0) return null;

        Collider bestTarget = null;
        TAP_PRIORITY bestTargetPriority = (TAP_PRIORITY)int.MaxValue;

        foreach (KeyValuePair<string, Collider> target in dict) {
            TAP_PRIORITY currentTapPriority = getTapPriority(target.Value.tag);
            if (currentTapPriority < bestTargetPriority) {
                bestTarget = target.Value;
                bestTargetPriority = currentTapPriority;
            }
        }

        return bestTarget.gameObject;
    }

    public void oneUp() {
        if (oneIsUp) {
            lifeTimer = 0;
            wellsUp = true;
            oneIsUp = false;
        }
        else oneIsUp = true;
    }

    public void up(bool shouldBeUp) {
        foreach (GameObject well in wells) {
            if (WellAnchor.wellIsAnchored(translateColourFromName(well.name))) {
                well.SetActive(shouldBeUp);
            }
        }
    }
}
