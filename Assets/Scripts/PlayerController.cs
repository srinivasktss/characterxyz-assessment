using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
	[Header("Behaviour References")]
	[SerializeField] private CharacterController _characterController;
	[SerializeField] private Transform _followUpCameraTransform;
	[SerializeField] private Animator _playerAnimator;

	[Space(20)]
	[Header("Properties")]
	[SerializeField] private float _moveSpeed = 10f;
	[SerializeField] private float _rotateSpeed = 45f;
	[SerializeField] private float _jumpSpeed = 4f;
	[SerializeField] private float _gravity = -9.81f;

	private float _horizontal;
	private float _vertical;

	private Vector3 _forward;
	private Vector3 _right;
	private float _mouseX;
	private bool _jumpPressed;

	private Vector3 _movementInput;
	private Vector3 _rotationInput;
	private Vector3 _jumpVelocity;

	private void Update()
	{
		HandleRotation();
		HandleJump();
		HandleMovement();
	}

	private void HandleRotation()
	{
		_mouseX = Input.GetAxis("Mouse X");
		_rotationInput.Set(0f, _mouseX, 0f);

		_characterController.transform.Rotate(_rotationInput * _rotateSpeed * Time.deltaTime);
	}

	private void HandleMovement()
	{
		_horizontal = _characterController.isGrounded ? Input.GetAxis("Horizontal"): 0f;
		_vertical = _characterController.isGrounded ? Input.GetAxis("Vertical"): 0f;

		_forward = _characterController.transform.forward;
		_right = _characterController.transform.right;

		_movementInput = (_forward * _vertical) + (_right * _horizontal);

		_playerAnimator.SetFloat("MoveX", _horizontal);
		_playerAnimator.SetFloat("MoveY", _vertical);
		_playerAnimator.SetFloat("Speed", _movementInput.normalized.magnitude);

		_movementInput += _jumpVelocity;

		_characterController.Move(_movementInput * _moveSpeed * Time.deltaTime);
	}

	private void HandleJump()
	{
		_jumpPressed = Input.GetButtonDown("Jump");
		if (_jumpPressed && _characterController.isGrounded)
		{
			_playerAnimator.SetTrigger("Jump");
			_jumpVelocity.y = _jumpSpeed;
		}
		else
		{
			_jumpVelocity.y += _gravity * Time.deltaTime;
		}

		_jumpVelocity.x = 0;
		_jumpVelocity.z = 0;
	}
}
