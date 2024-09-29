using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField]
    private float health = 1f;

    private float startingHealth;
    private MeshRenderer renderer;

    public UnityEvent OnHealthDecreased;
    public UnityEvent OnHealthIncreased;
    public UnityEvent OnDead;
    public string damagingTag = "";

    public float Health { get => health; set {
            if (health < value)
                OnHealthIncreased?.Invoke();
            else if (health > value)
                OnHealthDecreased?.Invoke();
            health = value;

            renderer.material.SetFloat("_healthLevel", health / startingHealth);

            if (health <= 0f)
                OnDead?.Invoke();
        }
    }

    public void Collision2D(Collider collider) {
        if (collider == null || damagingTag == "") return;
        if (collider.gameObject.tag == damagingTag) {
            var projectile = collider.GetComponent<projectile>();
            this.Health -= projectile.dps;
            Destroy(projectile.gameObject);
            if (this.health < 0f) {
                //Destroy(this.gameObject);

            }
        }
    }

    public void OnTriggerEnter(Collider other) {
        if(other == null || damagingTag == "") return;
        if (other.gameObject.tag == damagingTag) {
            var projectile = other.GetComponent<projectile>();
            if (projectile != null) {
                this.Health -= projectile.dps;
                Destroy(projectile.gameObject);
                if (this.health < 0f) {
                    //Destroy(this.gameObject);
                }
            }
        }
    }

    public void OnTriggerStay(Collider other) {
        var tether = other.GetComponent<AttentionTether>();
        if (tether != null) {
            this.Health -= tether.dps * Time.deltaTime;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        startingHealth = health;
        renderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
