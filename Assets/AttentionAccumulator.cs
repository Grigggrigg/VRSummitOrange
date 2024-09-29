using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttentionAccumulator : MonoBehaviour
{

    [SerializeField]
    private float attention = 0f;
    private bool isSelected = false;
    private Vector3 startSize;
    private MeshRenderer renderer;

    public float attentionGatheringRate = 1f;
    public float attentionDecayRate = 0.1f;
    public float attentionMax = -1f;
    public float attentionSizeIncrease = 0f;

    public UnityEvent OnAttentionChanged;
    public UnityEvent OnAttentionZero;
    public UnityEvent OnAttentionMax;

    public void Select() {
        isSelected = true;
    }

    public void Deselect() {
        isSelected = false;
    }

    // Start is called before the first frame update
    void Start() {
        startSize = transform.localScale;
        renderer = GetComponent<MeshRenderer>();

    }

    private void UpdateAttention() {
        transform.localScale = startSize + attentionSizeIncrease * new Vector3(1f, 1f, 1f) * attention;
        if(renderer != null) {
            renderer.material.SetFloat("attention", attention);
            renderer.material.SetFloat("attentionLevel", attention / attentionMax);
        }
    }

    // Update is called once per frame
    void Update() {
        if (isSelected) {
            attention += attentionGatheringRate * Time.deltaTime;
            OnAttentionChanged.Invoke();
            if (attentionMax > -0.1f && attention > attentionMax) {
                attention = attentionMax;
                OnAttentionMax.Invoke();
            }
        } else {
            attention -= attentionDecayRate * Time.deltaTime;
            OnAttentionChanged.Invoke();
            if (attention <= 0f) {
                attention = 0f;
                OnAttentionZero.Invoke();
            }
        }
    }
}
