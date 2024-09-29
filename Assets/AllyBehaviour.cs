using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyBehaviour : MonoBehaviour
{
    public GameObject player;
    public GameObject projectilePrefab;
    public Transform aimGroup;
    public HealthBehaviour health;
    public GameObject tetherPrefab;
    public AttentionTether tether;
    public float speed = 1f;
    public float speedMax = 3f;
    public float speedDecay = 1f;
    public float speedIncrease = 1f;
    public float playerDistanceMin = 3f;
    public bool canShoot = true;
    public bool canBeDamaged = false;
    public GameObject targetEnemy = null;

    public float attentionSpeedIncrease = 0.5f;
    public float attentionShotThreshold = 2f;
    public bool shotThresholdCanDecrease = false;
    public bool attentionShotThresholdReached = false;

    public float attentionMoveThreshold = 1f;
    public bool canMove = false;
    public bool attentionCanDecreaseMovement = false;

    public float shootDelayMin = 3f;
    public float shootDelayMax = 6f;
    public float shootDelayFactor = 1f;
    public float attentionShotIncrease = 0.5f;
    public float attentionShotIncreaseMax = 3f;

    public float switchTargetDelay = 1f;
    private float currentSwitchTargetDelay = 1f;

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

        if (tether == null) {
            var obj = Instantiate<GameObject>(tetherPrefab);
            tether = obj.GetComponent<AttentionTether>();
            var fromTo = obj.GetComponent<TehterObjectToObject>();
            fromTo.from = this.gameObject;
            fromTo.to = player;
            var acc = GetComponent<AttentionAccumulator>();
            tether.accumulator = acc;
        }
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

    private void TargetClosestEnemy() {
        var enemies = FindObjectsOfType<EnemyBehaviour>();
        var distanceLast = float.MaxValue;

        foreach (var enemy in enemies) {
            var dist = (enemy.transform.position - this.transform.position).magnitude;
            if (dist < distanceLast && !enemy.isFriendly) {
                distanceLast = dist;
                targetEnemy = enemy.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update() {

        if(tether.Attention < attentionMoveThreshold) {
            if (canMove && attentionCanDecreaseMovement) {
                canMove = false;
            }
        } else {
            canMove = true;
        }

        if (!canMove)
            return;

        if(!attentionShotThresholdReached) {
            if (tether.Attention >= attentionShotThreshold) {
                attentionShotThresholdReached = true;
            }
        } else if(shotThresholdCanDecrease && tether.Attention < attentionShotThreshold)
            attentionShotThresholdReached = false;

        var speedWithAttention = speed + (attentionSpeedIncrease * tether.Attention);

        transform.LookAt(player.transform);
        var playerDelta = (transform.position - player.transform.position).magnitude;
        if(playerDelta > playerDistanceMin) {
            if (speedCurrent < speedWithAttention) {
                speedCurrent += speedIncrease * Time.deltaTime;
            } else {
                speedCurrent = speedWithAttention;
            }
        } else {
            if (speedCurrent > 0f) {
                speedCurrent -= speedDecay * Time.deltaTime;
            } else {
                speedCurrent = 0f;
            }
        }

        this.transform.position += this.transform.forward * speedCurrent * Time.deltaTime;

        if (!canShoot || !attentionShotThresholdReached)
            return;

        currentSwitchTargetDelay -= Time.deltaTime;
        if (currentSwitchTargetDelay <= 0f) {
            currentSwitchTargetDelay = switchTargetDelay;
            TargetClosestEnemy();
        }

        if (targetEnemy == null)
            return;


        aimGroup.transform.LookAt(targetEnemy.transform);
        


        shootDelayCurrent -= Time.deltaTime;
        if(shootDelayCurrent < 0) {
            var shotDelayDecrease = tether.Attention * attentionShotIncrease;
            if (shotDelayDecrease > attentionShotIncreaseMax)
                shotDelayDecrease = attentionShotIncreaseMax;

            shootDelayCurrent = Mathf.Lerp(shootDelayMin * shootDelayFactor - shotDelayDecrease, shootDelayMax * shootDelayFactor - shotDelayDecrease, Random.value);
            Shoot();
        }

    }
}
