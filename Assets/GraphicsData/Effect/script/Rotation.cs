using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour {       
	public float myRotationSpeed = 100.0f;

	public bool isRotateX = false;
	public bool isRotateY = false;
	public bool isRotateZ = false;

	private bool positiveRotation = true;

	private int posOnNeg = 1;

	void Start () {

				if (positiveRotation == false) {
						posOnNeg = -1;

				}
		}

	void Update () {

				if (isRotateX) {
						transform.Rotate (myRotationSpeed * Time.deltaTime * posOnNeg, 0, 0);
				} else if (isRotateY) {
						transform.Rotate (0, myRotationSpeed * Time.deltaTime * posOnNeg, 0);
				} else if (isRotateZ) {
						transform.Rotate (0, 0, myRotationSpeed * Time.deltaTime * posOnNeg);
				}
		}
}

