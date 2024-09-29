using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    private bool hasStartedCalibrating = false;
    private float _score = 0f;
    public UnityEvent OnGameStarted;
    public UnityEvent OnGameEnded;
    public UnityEvent OnGameLost;
    public UnityEvent OnScoreIncreased;
    public UnityEvent OnScoreDecreased;

    public float Score {
        get => _score; set {
            if(_score < value)
                OnScoreIncreased?.Invoke();
            else if(_score > value)
                OnScoreDecreased?.Invoke();
            _score = value;
        }
    }

    public void StartGame() {
        OnGameStarted?.Invoke();
    }

    public void EndGame() {
        OnGameEnded?.Invoke();
    }

    public void LoseGame() {
        OnGameLost?.Invoke();
    }

    public void CalibrationContinueButtonPressed() {
        if (!hasStartedCalibrating)
            hasStartedCalibrating = true;
        else {
            OnGameStarted.Invoke();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnGameStarted.Invoke();
        }
    }
}
