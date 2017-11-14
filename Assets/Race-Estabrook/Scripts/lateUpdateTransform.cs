using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lateUpdateTransform : MonoBehaviour {

    public float yVal;

    private void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, yVal, transform.position.z);
    }
}
