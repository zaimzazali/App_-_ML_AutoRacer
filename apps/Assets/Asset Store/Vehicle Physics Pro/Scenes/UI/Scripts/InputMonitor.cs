//--------------------------------------------------------------
//      Vehicle Physics Pro: advanced vehicle physics kit
//          Copyright © 2011-2019 Angel Garcia "Edy"
//        http://vehiclephysics.com | @VehiclePhysics
//--------------------------------------------------------------

// InputMonitor: display live inputs and main states of a vehicle


using UnityEngine;
using UnityEngine.UI;


namespace VehiclePhysics.UI
{

public class InputMonitor : MonoBehaviour
	{
	public VehicleBase vehicle;

	// All these bars will be controlled via Image.fillAmount

	public Image throttleBar;
	public Image brakeBar;
	public Image clutchBar;
	public Image steerLeftBar;
	public Image steerRightBar;
	public Image handbrakeBar;

	public Image engineLoadBar;
	public Image clutchLockBar;
	public Image aidedSteerLeftBar;
	public Image aidedSteerRightBar;


	// Custom Edit 
	[Header("Custom Edit")]
	[SerializeField]
	private GameObject agent = null;
	//[SerializeField]
	private float currentThrottleInput = 0f;
	//[SerializeField]
	private float currentBrakeInput = 0f;
	//[SerializeField]
	private float currentSteerInput = 0f;
	//[SerializeField]
	private float currentSteerInputDegree = 0f;

	public float getCurrentThrottleInput() {
		return currentThrottleInput;
	}
	public float getCurrentBrakeInput() {
		return currentBrakeInput;
	}
	public float getCurrentSteerInput() {
		return currentSteerInput;
	}
	public float getCurrentSteerInputDegree() {
		return currentSteerInputDegree;
	}


	void OnEnable ()
		{
		if (vehicle != null)
			vehicle.onBeforeIntegrationStep += UpdateInput;
		}


	void OnDisable ()
		{
		if (vehicle != null)
			vehicle.onBeforeIntegrationStep -= UpdateInput;
		}


	void UpdateInput ()
		{
		if (vehicle == null) return;

		// Read bus values & translate them to bar positions

		int[] inputData = vehicle.data.Get(Channel.Input);
		int[] vehicleData = vehicle.data.Get(Channel.Vehicle);

		float throttleInput = inputData[InputData.Throttle] / 10000.0f;
		float brakeInput = inputData[InputData.Brake] / 10000.0f;
		float clutchInput = inputData[InputData.Clutch] / 10000.0f;
		float steerInput = inputData[InputData.Steer] / 10000.0f;
		float handbrakeInput = inputData[InputData.Handbrake] / 10000.0f;

		float engineLoad = vehicleData[VehicleData.EngineLoad] / 1000.0f;
		float clutchLock = vehicleData[VehicleData.ClutchLock] / 1000.0f;
		float aidedSteer = vehicleData[VehicleData.AidedSteer] / 10000.0f;

		currentThrottleInput = throttleInput;
		currentBrakeInput = brakeInput;
		currentSteerInput = steerInput;
		currentSteerInputDegree = agent.GetComponent<VPVisualEffects>().degreesOfRotation/2 * currentSteerInput;

		SetBar(throttleBar, throttleInput);
		SetBar(brakeBar, brakeInput);
		SetBar(clutchBar, clutchInput);
		SetBar(steerLeftBar, -steerInput);
		SetBar(steerRightBar, steerInput);
		SetBar(handbrakeBar, handbrakeInput);

		SetBar(engineLoadBar, engineLoad);
		SetBar(clutchLockBar, clutchLock >= 0.0f? 1.0f - clutchLock : 0.0f);
		SetBar(aidedSteerLeftBar, -aidedSteer);
		SetBar(aidedSteerRightBar, aidedSteer);
		}


	void SetBar (Image image, float value)
		{
		if (image != null) image.fillAmount = value;
		}
	}
}