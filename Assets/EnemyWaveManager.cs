using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWaveManager : MonoBehaviour
{

    public List<WaveInfo> waves = new List<WaveInfo>();
    public int waveCurrent = -1;
    public GameObject player;

    public UnityEvent OnWavesFinished;

    [Serializable]
    public class WaveInfo {
        public List<GameObject> enemies;
    }


    // Start is called before the first frame update
    void Start()
    {
        foreach(var wave in waves) {
            foreach(var enemy in wave.enemies) {
                enemy.gameObject.SetActive(false);
            }
        }

        //SpawnNextWave();
    }

    public void RemoveEnemy(GameObject enemy) {
        if (waveCurrent < waves.Count) {
            waves[waveCurrent].enemies.Remove(enemy);

            if(waves[waveCurrent].enemies.Count == 0) {
                SpawnNextWave();
            }
        }
    }

    public void SpawnNextWave() {
        waveCurrent += 1;

        if (waveCurrent == waves.Count)
            return;

        if(waveCurrent >= waves.Count) {
            OnWavesFinished.Invoke();
        } else {
            foreach (var enemy in waves[waveCurrent].enemies) {
                var e = enemy.gameObject.GetComponent<EnemyBehaviour>();
                if (e != null) {
                    e.waveManager = this;
                    e.player = this.player;
                }
                enemy.gameObject.SetActive(true);
            }
        }
    }
}
