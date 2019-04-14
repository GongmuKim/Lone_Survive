using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_control : MonoBehaviour
{
    public Transform target;
    public float dist = 0f;
    public float height = 0f;
    public float smooth_rate = 5.0f;

    private Transform self_tr;

	// Use this for initialization
	void Start ()
    {
        self_tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        float current_angle = Mathf.LerpAngle(self_tr.eulerAngles.y, target.eulerAngles.y, smooth_rate * Time.deltaTime);

        Quaternion rot = Quaternion.Euler(0, current_angle, 0);

        self_tr.position = target.position - (rot * Vector3.forward * dist) + (Vector3.up * height);

        self_tr.LookAt(target);
	}
}
