using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttentionTether : MonoBehaviour
{

    [SerializeField]
    private float attention = 0f;
    private bool isSelected = false;
    private MeshRenderer renderer;
    public GameObject tetherObj;

    public AttentionAccumulator accumulator;

    public float attentionGatheringRate = 1f;
    public float attentionDecayRate = 0.1f;
    public float attentionMax = -1f;
    public float attentionSizeIncrease = 0f;
    public TehterObjectToObject tether;
    public float startRadius = 0.2f;

    public UnityEvent OnAttentionChanged;
    public UnityEvent OnAttentionZero;
    public UnityEvent OnAttentionMax;

    public float dps = 1f;

    public float Attention { get => attention; }

    public void Select() {
        isSelected = true;
    }

    public void Deselect() {
        isSelected = false;
    }

    // Start is called before the first frame update
    void Start() {
        renderer = GetComponent<MeshRenderer>();
        tetherObj.SetActive(false);

    }

    private void UpdateAttention() {
        if(attention > 0.2f) tetherObj.gameObject.SetActive(true);
        if (tether != null)
            tether.radius = startRadius + attentionSizeIncrease * Attention;
        if (renderer != null) {
            renderer.material.SetFloat("_attention", Attention);
            renderer.material.SetFloat("_attentionLevel", Attention / attentionMax);
        }
    }

    public void OnAttentionChangedEvent() {
        OnAttentionChanged.Invoke();
    }

    public void OnAttentionZeroEvent() {
        OnAttentionZero.Invoke();
    }

    public void OnAttentionMaxEvent() {
        OnAttentionMax.Invoke();
    }



    // Update is called once per frame
    void Update() {

        this.attention = accumulator.Attention;

        UpdateAttention();

        /*
        if (isSelected) {
            attention += attentionGatheringRate * Time.deltaTime;
            OnAttentionChanged.Invoke();
            if (attentionMax > -0.1f && Attention > attentionMax) {
                attention = attentionMax;
                OnAttentionMax.Invoke();
            }
        } else {
            attention -= attentionDecayRate * Time.deltaTime;
            OnAttentionChanged.Invoke();
            if (Attention <= 0f) {
                attention = 0f;
                OnAttentionZero.Invoke();
            }
        }
        */
    }
}
