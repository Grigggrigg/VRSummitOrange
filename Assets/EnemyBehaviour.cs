using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject projectilePrefab;
    public Transform aimGroup;
    public HealthBehaviour health;
    public float speed = 1f;
    public float speedDecay = 1f;
    public float speedIncrease = 1f;
    public float playerDistanceMin = 3f;
    public bool canShoot = true;

    public EnemyWaveManager waveManager;

    public float shootDelayMin = 3f;
    public float shootDelayMax = 6f;
    public float shootDelayFactor = 1f;
    public bool isFriendly = false;

    private float shootDelayCurrent;
    private float speedCurrent = 1f;
    private List<Transform> aims = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        for(var i = 0; i < aimGroup.childCount; i++) {
            aims.Add(aimGroup.GetChild(i));
        }

        shootDelayCurrent = shootDelayMax;
    }

    private void Shoot() {
        foreach (var aim in aims) {
            var p = Instantiate<GameObject>(projectilePrefab);
            var direction = (aim.transform.position - gameObject.transform.position).normalized;
            p.transform.position = transform.position;
            //p.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            p.transform.LookAt(aim.transform);
        }
    }

    public void SetFriendly(bool isFriendly) {
        this.isFriendly = isFriendly;
    }

    // Update is called once per frame
    void Update() {

        if (isFriendly) {
            if (speedCurrent < speed) {
                speedCurrent += speedIncrease * Time.deltaTime;
            } else {
                speedCurrent = speed;
            }
            this.transform.position += this.transform.forward * speedCurrent * Time.deltaTime;
            return;
        }

        transform.LookAt(player.transform);
        var playerDelta = (transform.position - player.transform.position).magnitude;
        if(playerDelta > playerDistanceMin) {
            if (speedCurrent < speed) {
                speedCurrent += speedIncrease * Time.deltaTime;
            } else {
                speedCurrent = speed;
            }
        } else {
            if (speedCurrent > 0f) {
                speedCurrent -= speedDecay * Time.deltaTime;
            } else {
                speedCurrent = 0f;
            }
        }

        this.transform.position += this.transform.forward * speedCurrent * Time.deltaTime;


        if (!canShoot)
            return;

        shootDelayCurrent -= Time.deltaTime;
        if(shootDelayCurrent < 0) {
            shootDelayCurrent = Mathf.Lerp(shootDelayMin * shootDelayFactor, shootDelayMax * shootDelayFactor, Random.value);
            Shoot();
        }

    }
}
