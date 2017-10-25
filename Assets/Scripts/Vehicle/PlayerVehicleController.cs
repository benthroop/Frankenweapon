using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerVehicleController : MonoBehaviour 
{
	VehicleActionSet vehicleActions;
	[SerializeField] VehicleBase myVehicle;

	public enum ControlType { Keyboard, Controller1, Controller2 }
	public ControlType controlType = ControlType.Keyboard;

	void Start()
	{
		vehicleActions = new VehicleActionSet();

		if (controlType == ControlType.Keyboard)
		{
			vehicleActions.Left.AddDefaultBinding(new KeyBindingSource(Key.A));
			vehicleActions.Right.AddDefaultBinding(new KeyBindingSource(Key.D));
			vehicleActions.Forward.AddDefaultBinding(new KeyBindingSource(Key.W));
			vehicleActions.Backward.AddDefaultBinding(new KeyBindingSource(Key.S));
			vehicleActions.Boost.AddDefaultBinding(new KeyBindingSource(Key.Space));
			vehicleActions.Action.AddDefaultBinding(new KeyBindingSource(Key.E));
			vehicleActions.NextGear.AddDefaultBinding(new KeyBindingSource(Key.C));
			vehicleActions.PreviousGear.AddDefaultBinding(new KeyBindingSource(Key.V));
		}
		else
		{
			vehicleActions.Left.AddDefaultBinding(InputControlType.LeftStickLeft);
			vehicleActions.Right.AddDefaultBinding(InputControlType.LeftStickRight);
			vehicleActions.Forward.AddDefaultBinding(InputControlType.RightTrigger);
			vehicleActions.Backward.AddDefaultBinding(InputControlType.LeftTrigger);
			vehicleActions.LookLeft.AddDefaultBinding(InputControlType.RightStickLeft);
			vehicleActions.LookRight.AddDefaultBinding(InputControlType.RightStickRight);
			vehicleActions.LookUp.AddDefaultBinding(InputControlType.RightStickUp);
			vehicleActions.LookDown.AddDefaultBinding(InputControlType.RightStickDown);
			vehicleActions.Boost.AddDefaultBinding(InputControlType.Action2);
			vehicleActions.Action.AddDefaultBinding(InputControlType.Action1);
			vehicleActions.NextGear.AddDefaultBinding(InputControlType.DPadUp);
			vehicleActions.PreviousGear.AddDefaultBinding(InputControlType.DPadDown);
		}
	}

	void Update()
	{
		myVehicle.SetSteering(vehicleActions.Steering);
		myVehicle.SetThrottle(vehicleActions.Throttle);

		if (vehicleActions.Boost.WasPressed)
		{
			myVehicle.Boost();
		}

		if (vehicleActions.Action.WasPressed)
		{
			myVehicle.Action();
		}

		if (vehicleActions.NextGear.WasPressed)
		{
			myVehicle.NextGear();
		}

		if (vehicleActions.PreviousGear.WasPressed)
		{
			myVehicle.PreviousGear();
		}
	}
}
