using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float speed = 1f;
    public float dps = 1f;
    public string dpsTag = "";

    // Start is called before the first frame update
    void Start() {
        
    }

    public void Collision2D(Collider collider) {
        if (collider == null) return;
        if (collider.gameObject.tag == dpsTag) {
            var health = collider.gameObject.GetComponent<HealthBehaviour>();
            if ((health != null)) {
                health.Health -= dps;
            }
        }
    }

    // Update is called once per frame
    void Update() {
        this.transform.position += this.transform.forward * speed * Time.deltaTime;
    }
}
