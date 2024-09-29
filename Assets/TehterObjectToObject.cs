using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class TehterObjectToObject : MonoBehaviour
{
    public GameObject from;
    public GameObject to;
    public float radius = 0.2f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update()
    {
        var delta = from.transform.position - to.transform.position;
        var magnitude = delta.magnitude;
        this.transform.position = to.transform.position + (delta) * 0.5f;
        this.transform.rotation = Quaternion.LookRotation(delta, Vector3.up);
        this.transform.localScale = new Vector3(radius, radius, magnitude * 0.5f);
    }
}
