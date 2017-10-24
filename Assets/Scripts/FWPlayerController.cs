using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using InControl;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(WeaponManager))]
public class FWPlayerController : MonoBehaviour
{
	[Serializable]
	public class MovementSettings
	{
		public float ForwardSpeed = 8.0f;   // Speed when walking forward
		public float BackwardSpeed = 4.0f;  // Speed when walking backwards
		public float StrafeSpeed = 4.0f;    // Speed when walking sideways
		public float RunMultiplier = 2.0f;   // Speed when sprinting
		public KeyCode RunKey = KeyCode.LeftShift;
		public float JumpForce = 30f;
		public AnimationCurve SlopeCurveModifier = new AnimationCurve(new Keyframe(-90.0f, 1.0f), new Keyframe(0.0f, 1.0f), new Keyframe(90.0f, 0.0f));
		[HideInInspector]
		public float CurrentTargetSpeed = 8f;
		private bool m_Running;

		public void UpdateDesiredTargetSpeed(Vector2 input)
		{
			if (input == Vector2.zero) return;
			if (input.x > 0 || input.x < 0)
			{
				//strafe
				CurrentTargetSpeed = StrafeSpeed;
			}
			if (input.y < 0)
			{
				//backwards
				CurrentTargetSpeed = BackwardSpeed;
			}
			if (input.y > 0)
			{
				//forwards
				//handled last as if strafing and moving forward at the same time forwards speed should take precedence
				CurrentTargetSpeed = ForwardSpeed;
			}
			if (Input.GetKey(RunKey))
			{
				CurrentTargetSpeed *= RunMultiplier;
				m_Running = true;
			}
			else
			{
				m_Running = false;
			}
		}

		public bool Running
		{
			get { return m_Running; }
		}
	}

	public FWPlayerActionSet actionSet;
	private WeaponManager weaponManager;

	public enum ControlType { Keyboard, Controller1, Controller2 }
	public ControlType controlType = ControlType.Keyboard;

	[Serializable]
	public class AdvancedSettings
	{
		public float groundCheckDistance = 0.01f; // distance for checking if the controller is grounded ( 0.01f seems to work best for this )
		public float stickToGroundHelperDistance = 0.5f; // stops the character
		public float slowDownRate = 20f; // rate at which the controller comes to a stop when there is no input
		public bool airControl; // can the user control the direction that is being moved in the air
		[Tooltip("set it to 0.1 or more if you get stuck in wall")]
		public float shellOffset; //reduce the radius by that ratio to avoid getting stuck in wall (a value of 0.1f is nice)
	}


	public Camera cam;
	public MovementSettings movementSettings = new MovementSettings();
	public FWPlayerLookController mouseLook = new FWPlayerLookController();
	public AdvancedSettings advancedSettings = new AdvancedSettings();


	private Rigidbody m_RigidBody;
	private CapsuleCollider m_Capsule;
	private float m_YRotation;
	private Vector3 m_GroundContactNormal;
	private bool m_Jump, m_PreviouslyGrounded, m_Jumping, m_IsGrounded;


	public Vector3 Velocity
	{
		get { return m_RigidBody.velocity; }
	}

	public bool Grounded
	{
		get { return m_IsGrounded; }
	}

	public bool Jumping
	{
		get { return m_Jumping; }
	}

	public bool Running
	{
		get
		{
			return movementSettings.Running;
		}
	}

	private void Start()
	{
		m_RigidBody = GetComponent<Rigidbody>();
		m_Capsule = GetComponent<CapsuleCollider>();
		mouseLook.Init(transform, cam.transform);
		weaponManager = GetComponent<WeaponManager>();

		actionSet = new FWPlayerActionSet();

		if (controlType == ControlType.Keyboard)
		{
			actionSet.Left.AddDefaultBinding(new KeyBindingSource(Key.A));
			actionSet.Right.AddDefaultBinding(new KeyBindingSource(Key.D));
			actionSet.Forward.AddDefaultBinding(new KeyBindingSource(Key.W));
			actionSet.Backward.AddDefaultBinding(new KeyBindingSource(Key.S));
			actionSet.Jump.AddDefaultBinding(new KeyBindingSource(Key.Space));
			actionSet.FirePrimary.AddDefaultBinding(new MouseBindingSource(Mouse.LeftButton));
			actionSet.FireSecondary.AddDefaultBinding(new MouseBindingSource(Mouse.RightButton));
			actionSet.Reload.AddDefaultBinding(new KeyBindingSource(Key.R));
			actionSet.NextWeapon.AddDefaultBinding(new KeyBindingSource(Key.C));
			actionSet.PreviousWeapon.AddDefaultBinding(new KeyBindingSource(Key.V));
		}
		else
		{
			actionSet.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
			actionSet.Right.AddDefaultBinding(InputControlType.LeftStickRight);
			actionSet.Forward.AddDefaultBinding(InputControlType.LeftStickUp);
			actionSet.Backward.AddDefaultBinding(InputControlType.LeftStickDown);
			actionSet.LookLeft.AddDefaultBinding(InputControlType.RightStickLeft);
			actionSet.LookRight.AddDefaultBinding(InputControlType.RightStickRight);
			actionSet.LookUp.AddDefaultBinding(InputControlType.RightStickUp);
			actionSet.LookDown.AddDefaultBinding(InputControlType.RightStickDown);
			actionSet.Jump.AddDefaultBinding(InputControlType.Action2);
			actionSet.FirePrimary.AddDefaultBinding(InputControlType.RightTrigger);
			actionSet.FireSecondary.AddDefaultBinding(InputControlType.RightBumper);
			actionSet.Reload.AddDefaultBinding(InputControlType.Action3);
			actionSet.NextWeapon.AddDefaultBinding(InputControlType.DPadUp);
			actionSet.PreviousWeapon.AddDefaultBinding(InputControlType.DPadDown);
		}

		if (controlType == ControlType.Controller1)
		{
			if (InputManager.Devices.Count > 0)
			{
				actionSet.Device = InputManager.Devices[0];
			}
		}
		else if (controlType == ControlType.Controller2)
		{
			if (InputManager.Devices.Count > 1)
			{
				actionSet.Device = InputManager.Devices[1];
			}
			else if (InputManager.Devices.Count > 0)
			{
				actionSet.Device = InputManager.Devices[0];
				Debug.LogWarning("There's only one controller. Assigning Player 2 to Device 0");
			}
			else
			{
				controlType = ControlType.Keyboard;
				Debug.LogWarning("There are no controllers. Reverting to keyboard for everyone!");
			}
		}
	}

	private void Update()
	{
		RotateView();

		if (actionSet.Jump.WasPressed && !m_Jump)
		{
			m_Jump = true;
		}

        if (actionSet.FirePrimary.WasPressed)
        {
            weaponManager.CurrentWeaponPrimaryFireStart();
        }

        if (actionSet.FirePrimary.WasReleased)
        {
            weaponManager.CurrentWeaponPrimaryFireStop();
        }

		if (actionSet.FireSecondary.WasPressed)
		{
			weaponManager.CurrentWeaponSecondaryFireStart();
		}

		if (actionSet.FireSecondary.WasReleased)
		{
			weaponManager.CurrentWeaponSecondaryFireStop();
		}

		if (actionSet.Reload.WasPressed)
		{
			weaponManager.CurrentWeaponReload();
		}

		if (actionSet.PreviousWeapon.WasPressed)
		{
			weaponManager.PreviousWeapon();
		}

		if (actionSet.NextWeapon.WasPressed)
		{
			weaponManager.NextWeapon();
		}
  	}


	private void FixedUpdate()
	{
		GroundCheck();
		Vector2 input = GetInput();

		// always move along the camera forward as it is the direction that it being aimed at
		Vector3 desiredMove = cam.transform.forward * input.y + cam.transform.right * input.x;
		desiredMove = Vector3.ProjectOnPlane(desiredMove, m_GroundContactNormal).normalized;
		desiredMove.x = desiredMove.x * movementSettings.CurrentTargetSpeed;
		desiredMove.z = desiredMove.z * movementSettings.CurrentTargetSpeed;
		desiredMove.y = desiredMove.y * movementSettings.CurrentTargetSpeed;

		if (m_IsGrounded)
		{
			if (m_RigidBody.velocity.sqrMagnitude < (movementSettings.CurrentTargetSpeed * movementSettings.CurrentTargetSpeed))
			{
				m_RigidBody.AddForce(desiredMove * SlopeMultiplier(), ForceMode.Impulse);
			}

			//m_RigidBody.AddForce(desiredMove, ForceMode.Impulse);

			m_RigidBody.drag = 5f;

			if (m_Jump)
			{
				m_RigidBody.drag = 0f;
				m_RigidBody.velocity = new Vector3(m_RigidBody.velocity.x, 0f, m_RigidBody.velocity.z);
				m_RigidBody.AddForce(new Vector3(0f, movementSettings.JumpForce, 0f), ForceMode.Impulse);
				m_Jumping = true;
			}

			if (!m_Jumping && Mathf.Abs(input.x) < float.Epsilon && Mathf.Abs(input.y) < float.Epsilon && m_RigidBody.velocity.magnitude < 1f)
			{
				m_RigidBody.Sleep();
			}
		}
		else
		{
			m_RigidBody.drag = 0f;
		}

		m_Jump = false;
	}


	private float SlopeMultiplier()
	{
		float angle = Vector3.Angle(m_GroundContactNormal, Vector3.up);
		return movementSettings.SlopeCurveModifier.Evaluate(angle);
	}


	private void StickToGroundHelper()
	{
		RaycastHit hitInfo;
		if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
								((m_Capsule.height / 2f) - m_Capsule.radius) +
								advancedSettings.stickToGroundHelperDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
		{
			if (Mathf.Abs(Vector3.Angle(hitInfo.normal, Vector3.up)) < 85f)
			{
				m_RigidBody.velocity = Vector3.ProjectOnPlane(m_RigidBody.velocity, hitInfo.normal);
			}
		}
	}


	private Vector2 GetInput()
	{
		Vector2 input = new Vector2
		{
			x = actionSet.MoveHorizontal.Value,
			y = actionSet.MoveVertical.Value
		};

		movementSettings.UpdateDesiredTargetSpeed(input);
		return input;
	}


	private void RotateView()
	{
		//avoids the mouse looking if the game is effectively paused
		if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

		// get the rotation before it's changed
		float oldYRotation = transform.eulerAngles.y;

		if (controlType == ControlType.Keyboard)
		{
			float yRot = CrossPlatformInputManager.GetAxis("Mouse X");
			float xRot = CrossPlatformInputManager.GetAxis("Mouse Y");
			mouseLook.LookRotation(transform, cam.transform, xRot * mouseLook.XSensitivity, yRot * mouseLook.YSensitivity);
		}
		else
		{
			float yRot = actionSet.LookHorizontal.Value * mouseLook.XSensitivity;
			float xRot = actionSet.LookVertical.Value * mouseLook.YSensitivity;
			mouseLook.LookRotation(transform, cam.transform, xRot, yRot);
		}

		if (m_IsGrounded || advancedSettings.airControl)
		{
			// Rotate the rigidbody velocity to match the new direction that the character is looking
			Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
			m_RigidBody.velocity = velRotation * m_RigidBody.velocity;
		}
	}

	/// sphere cast down just beyond the bottom of the capsule to see if the capsule is colliding round the bottom
	private void GroundCheck()
	{
		m_PreviouslyGrounded = m_IsGrounded;
		RaycastHit hitInfo;
		if (Physics.SphereCast(transform.position, m_Capsule.radius * (1.0f - advancedSettings.shellOffset), Vector3.down, out hitInfo,
								((m_Capsule.height / 2f) - m_Capsule.radius) + advancedSettings.groundCheckDistance, Physics.AllLayers, QueryTriggerInteraction.Ignore))
		{
			m_IsGrounded = true;
			m_GroundContactNormal = hitInfo.normal;
		}
		else
		{
			m_IsGrounded = false;
			m_GroundContactNormal = Vector3.up;
		}
		if (!m_PreviouslyGrounded && m_IsGrounded && m_Jumping)
		{
			m_Jumping = false;
		}
	}
}

