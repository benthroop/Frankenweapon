using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class FWPlayerActionSet : PlayerActionSet 
{
	public PlayerAction Left;
	public PlayerAction Right;
	public PlayerOneAxisAction MoveHorizontal;
	
	public PlayerAction Forward;
	public PlayerAction Backward;
	public PlayerOneAxisAction MoveVertical;

	public PlayerAction LookLeft;
	public PlayerAction LookRight;
	public PlayerOneAxisAction LookHorizontal;

	public PlayerAction LookUp;
	public PlayerAction LookDown;
	public PlayerOneAxisAction LookVertical;

	public PlayerAction FirePrimary;
	public PlayerAction FireSecondary;
	public PlayerAction Reload;
	public PlayerAction Jump;

	public PlayerAction NextWeapon;
	public PlayerAction PreviousWeapon;

	public FWPlayerActionSet()
	{
		Left = CreatePlayerAction("Move Left");
		Right = CreatePlayerAction("Move Right");
		MoveHorizontal = CreateOneAxisPlayerAction(Left, Right);

		Forward = CreatePlayerAction("Move Forward");
		Backward = CreatePlayerAction("Move Backward");
		MoveVertical = CreateOneAxisPlayerAction(Backward, Forward);

		LookLeft = CreatePlayerAction("Look Left");
		LookRight = CreatePlayerAction("Look Right");
		LookHorizontal = CreateOneAxisPlayerAction(LookLeft, LookRight);

		LookUp = CreatePlayerAction("Look Up");
		LookDown = CreatePlayerAction("Look Down");
		LookVertical = CreateOneAxisPlayerAction(LookDown, LookUp);

		FirePrimary = CreatePlayerAction("Fire Primary");
		FireSecondary = CreatePlayerAction("Fire Secondary");
		Reload = CreatePlayerAction("Reload");
		Jump = CreatePlayerAction("Jump");

		NextWeapon = CreatePlayerAction("Next Weapon");
		PreviousWeapon = CreatePlayerAction("Previous Weapon");
	}
}
