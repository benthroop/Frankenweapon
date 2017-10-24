﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FWHeadBob : MonoBehaviour 
{
    public Camera Camera;
    public CurveControlledBob motionBob = new CurveControlledBob();
    public LerpControlledBob jumpAndLandingBob = new LerpControlledBob();
    public FWPlayerController playerController;
    public float StrideInterval;
    [Range(0f, 1f)] public float RunningStrideLengthen;

    // private CameraRefocus m_CameraRefocus;
    private bool m_PreviouslyGrounded;
    private Vector3 m_OriginalCameraPosition;


    private void Start()
    {
        motionBob.Setup(Camera, StrideInterval);
        m_OriginalCameraPosition = Camera.transform.localPosition;
    //     m_CameraRefocus = new CameraRefocus(Camera, transform.root.transform, Camera.transform.localPosition);
    }


    private void Update()
    {
        //  m_CameraRefocus.GetFocusPoint();
        Vector3 newCameraPosition;
        if (playerController.Velocity.magnitude > 0 && playerController.Grounded)
        {
            Camera.transform.localPosition = motionBob.DoHeadBob(playerController.Velocity.magnitude*(playerController.Running ? RunningStrideLengthen : 1f));
            newCameraPosition = Camera.transform.localPosition;
            newCameraPosition.y = Camera.transform.localPosition.y - jumpAndLandingBob.Offset();
        }
        else
        {
            newCameraPosition = Camera.transform.localPosition;
            newCameraPosition.y = m_OriginalCameraPosition.y - jumpAndLandingBob.Offset();
        }
        Camera.transform.localPosition = newCameraPosition;

        if (!m_PreviouslyGrounded && playerController.Grounded)
        {
            StartCoroutine(jumpAndLandingBob.DoBobCycle());
        }

        m_PreviouslyGrounded = playerController.Grounded;
        //  m_CameraRefocus.SetFocusPoint();
    }
}

[System.Serializable]
public class CurveControlledBob
{
	public float HorizontalBobRange = 0.33f;
	public float VerticalBobRange = 0.33f;
	public AnimationCurve Bobcurve = new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(0.5f, 1f),
														new Keyframe(1f, 0f), new Keyframe(1.5f, -1f),
														new Keyframe(2f, 0f)); // sin curve for head bob
	public float VerticaltoHorizontalRatio = 1f;

	private float m_CyclePositionX;
	private float m_CyclePositionY;
	private float m_BobBaseInterval;
	private Vector3 m_OriginalCameraPosition;
	private float m_Time;


	public void Setup(Camera camera, float bobBaseInterval)
	{
		m_BobBaseInterval = bobBaseInterval;
		m_OriginalCameraPosition = camera.transform.localPosition;

		// get the length of the curve in time
		m_Time = Bobcurve[Bobcurve.length - 1].time;
	}


	public Vector3 DoHeadBob(float speed)
	{
		float xPos = m_OriginalCameraPosition.x + (Bobcurve.Evaluate(m_CyclePositionX) * HorizontalBobRange);
		float yPos = m_OriginalCameraPosition.y + (Bobcurve.Evaluate(m_CyclePositionY) * VerticalBobRange);

		m_CyclePositionX += (speed * Time.deltaTime) / m_BobBaseInterval;
		m_CyclePositionY += ((speed * Time.deltaTime) / m_BobBaseInterval) * VerticaltoHorizontalRatio;

		if (m_CyclePositionX > m_Time)
		{
			m_CyclePositionX = m_CyclePositionX - m_Time;
		}
		if (m_CyclePositionY > m_Time)
		{
			m_CyclePositionY = m_CyclePositionY - m_Time;
		}

		return new Vector3(xPos, yPos, 0f);
	}
}

[System.Serializable]
public class LerpControlledBob
{
	public float BobDuration;
	public float BobAmount;

	private float m_Offset = 0f;


	// provides the offset that can be used
	public float Offset()
	{
		return m_Offset;
	}


	public IEnumerator DoBobCycle()
	{
		// make the camera move down slightly
		float t = 0f;
		while (t < BobDuration)
		{
			m_Offset = Mathf.Lerp(0f, BobAmount, t / BobDuration);
			t += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}

		// make it move back to neutral
		t = 0f;
		while (t < BobDuration)
		{
			m_Offset = Mathf.Lerp(BobAmount, 0f, t / BobDuration);
			t += Time.deltaTime;
			yield return new WaitForFixedUpdate();
		}
		m_Offset = 0f;
	}
}