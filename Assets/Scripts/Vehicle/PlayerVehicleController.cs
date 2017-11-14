using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

[RequireComponent(typeof(VehicleBase))]
public class PlayerVehicleController : MonoBehaviour 
{
	VehicleActionSet vehicleActions;
	VehicleBase myVehicle;

	public enum ControlType { Keyboard, Keyboard2, Controller1, Controller2 }
	public ControlType controlType = ControlType.Keyboard;
    public bool CanMove = false;

	void Start()
	{
		myVehicle = GetComponent<VehicleBase>();
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
        else if(controlType == ControlType.Keyboard2)
        {
            vehicleActions.Left.AddDefaultBinding(new KeyBindingSource(Key.LeftArrow));
            vehicleActions.Right.AddDefaultBinding(new KeyBindingSource(Key.RightArrow));
            vehicleActions.Forward.AddDefaultBinding(new KeyBindingSource(Key.UpArrow));
            vehicleActions.Backward.AddDefaultBinding(new KeyBindingSource(Key.DownArrow));
            vehicleActions.Boost.AddDefaultBinding(new KeyBindingSource(Key.RightControl));
            vehicleActions.Action.AddDefaultBinding(new KeyBindingSource(Key.RightShift));
            vehicleActions.NextGear.AddDefaultBinding(new KeyBindingSource(Key.M));
            vehicleActions.PreviousGear.AddDefaultBinding(new KeyBindingSource(Key.N));
        }
        else if (controlType == ControlType.Controller1 )
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
        if (CanMove == true)
        {


            myVehicle.SetSteering(vehicleActions.Steering);
            myVehicle.SetThrottle(vehicleActions.Throttle);

            if (vehicleActions.Boost.WasPressed)
            {
                myVehicle.BoostStart();
            }

            if (vehicleActions.Boost.WasReleased)
            {
                myVehicle.BoostStop();
            }

            if (vehicleActions.Action.WasPressed)
            {
                myVehicle.ActionStart();
            }

            if (vehicleActions.Action.WasReleased)
            {
                myVehicle.ActionStop();
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
}
