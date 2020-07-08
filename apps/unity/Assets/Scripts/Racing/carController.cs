using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carController : MonoBehaviour
{
    /*
    [SerializeField]
    private inputManager inputManager = null;
    [SerializeField]
    private List<WheelCollider> throttleWheels = null;
    [SerializeField]
    private List<WheelCollider> steerWheels = null;
    [SerializeField]
    private float strengthCoef = 0f;
    [SerializeField]
    private float maxTurn = 0f;
    [SerializeField]
    private Transform centerOfMass = null;

    private void Awake() {
        inputManager = gameObject.GetComponent<inputManager>();
        gameObject.GetComponent<Rigidbody>().centerOfMass = centerOfMass.position;
    }

    private void FixedUpdate() {
        foreach (WheelCollider wheel in throttleWheels) {
            wheel.motorTorque = strengthCoef * Time.deltaTime * inputManager.getThrottle();
        }

        foreach (WheelCollider wheel in steerWheels) {
            wheel.steerAngle = maxTurn * inputManager.getSteer();
        }
    }
    */

    private float m_horizontalInput;
	private float m_verticalInput;
	private float m_steeringAngle;

	public WheelCollider frontDriverW, frontPassengerW;
	public WheelCollider rearDriverW, rearPassengerW;
	public Transform frontDriverT, frontPassengerT;
	public Transform rearDriverT, rearPassengerT;
	public float maxSteerAngle = 30;
	public float motorForce = 50;

    public void GetInput()
	{
		m_horizontalInput = Input.GetAxis("Horizontal");
		m_verticalInput = Input.GetAxis("Vertical");
	}

	private void Steer()
	{
		m_steeringAngle = maxSteerAngle * m_horizontalInput;
		frontDriverW.steerAngle = m_steeringAngle;
		frontPassengerW.steerAngle = m_steeringAngle;
	}

	private void Accelerate()
	{
		rearDriverW.motorTorque = m_verticalInput * motorForce;
		rearPassengerW.motorTorque = m_verticalInput * motorForce;
	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontDriverW, frontDriverT);
		UpdateWheelPose(frontPassengerW, frontPassengerT);
		UpdateWheelPose(rearDriverW, rearDriverT);
		UpdateWheelPose(rearPassengerW, rearPassengerT);
	}

	private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
	{
		Vector3 _pos = _transform.position;
		Quaternion _quat = _transform.rotation;

		_collider.GetWorldPose(out _pos, out _quat);

		_transform.position = _pos;
		_transform.rotation = _quat;
	}

	private void FixedUpdate()
	{
		GetInput();
		Steer();
		Accelerate();
		UpdateWheelPoses();
	}
}
