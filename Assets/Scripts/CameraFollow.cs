using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private Transform _target;

	[Header("Properties")]
	[SerializeField] private Vector3 _offset = new Vector3(0f, 2f, -5f);
	[SerializeField] private float _followSpeed = 2f;

	private void LateUpdate()
	{
		Follow();
	}

	private void Follow()
	{
		Vector3 followPosition = _target.position + _target.rotation * _offset;

		transform.position = Vector3.Lerp(transform.position, followPosition, _followSpeed);

		//transform.rotation = Quaternion.LookRotation(_target.forward, Vector3.up);

		transform.LookAt(_target);
	}
}
