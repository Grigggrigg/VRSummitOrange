using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientToCamera : MonoBehaviour {
    private Transform mainCam;

    private void OnEnable() {
        mainCam = Camera.main.transform;
    }

    private void LateUpdate() {
        transform.LookAt(mainCam);
        transform.RotateAround(transform.position, transform.up, 180f);
    }
}
