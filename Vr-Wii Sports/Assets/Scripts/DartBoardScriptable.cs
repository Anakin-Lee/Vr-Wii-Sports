using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="dartboardscriptable", menuName = "Custom/Dartboard")]
public class DartBoardScriptable : ScriptableObject
{
    public float radius = 10f;
    public float bullseye = .10f;
    public float outerBullseye = .25f;
    public float tripleRingInner = .50f;
    public float tripleRingOuter = .60f;
    public float doubleRingInner = .70f;
    public float doubleRingOuter = .80f;
}
