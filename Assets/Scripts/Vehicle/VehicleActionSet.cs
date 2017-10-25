using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class VehicleActionSet : PlayerActionSet 
{
	public PlayerAction Left;
	public PlayerAction Right;
	public PlayerOneAxisAction Steering;
	
	public PlayerAction Forward;
	public PlayerAction Backward;
	public PlayerOneAxisAction Throttle;

	public PlayerAction LookLeft;
	public PlayerAction LookRight;
	public PlayerOneAxisAction LookHorizontal;

	public PlayerAction LookUp;
	public PlayerAction LookDown;
	public PlayerOneAxisAction LookVertical;

	public PlayerAction Boost;
	public PlayerAction Action;

	public PlayerAction NextGear;
	public PlayerAction PreviousGear;

	public VehicleActionSet()
	{
		Left = CreatePlayerAction("Steer Left");
		Right = CreatePlayerAction("Steer Right");
		Steering = CreateOneAxisPlayerAction(Left, Right);

		Forward = CreatePlayerAction("Throttle Forward");
		Backward = CreatePlayerAction("Throttle Backward");
		Throttle = CreateOneAxisPlayerAction(Backward, Forward);

		LookLeft = CreatePlayerAction("Look Left");
		LookRight = CreatePlayerAction("Look Right");
		LookHorizontal = CreateOneAxisPlayerAction(LookLeft, LookRight);

		LookUp = CreatePlayerAction("Look Up");
		LookDown = CreatePlayerAction("Look Down");
		LookVertical = CreateOneAxisPlayerAction(LookDown, LookUp);

		Boost = CreatePlayerAction("Boost");
		Action = CreatePlayerAction("Action");
		NextGear = CreatePlayerAction("Next Gear");
		PreviousGear = CreatePlayerAction("Previous Gear");
	}
}
