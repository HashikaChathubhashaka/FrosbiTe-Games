using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingRotate : MonoBehaviour
{
    public Vector3 Axis;
    // Update is called once per frame
    void Update() {
        transform.Rotate(Axis * Time.deltaTime);
    }
}
