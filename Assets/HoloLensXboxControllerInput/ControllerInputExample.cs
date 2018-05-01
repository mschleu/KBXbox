using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;
using System.Text;
using HoloLensKeyboard;
// Needed //////////////////////////////////////////////////
using HoloLensXboxController;
////////////////////////////////////////////////////////////

public class ControllerInputExample : MonoBehaviour
{
    public GameObject myCreator;
    GameObject mKey;
    GameObject prev_mKey;

    // Needed //////////////////////////////////////////////////
    private ControllerInput controllerInput;
    ///////////////////////////////////////////////////////////
    public GameObject Manager;
    public GameObject KeyboardMaster;

    float maxRayDistance = 10.0f;
    Vector3 vector = new Vector3(0, 0, -0.1f);

    // Use this for initialization
    void Start()
    {
        // Needed //////////////////////////////////////////////////
        controllerInput = new ControllerInput(0, 0.19f);
        // First parameter is the number, starting at zero, of the controller you want to follow.
        // Second parameter is the default “dead” value; meaning all stick readings less than this value will be set to 0.0.
        ///////////////////////////////////////////////////////////
    }

    void Update()
    {
        // Needed //////////////////////////////////////////////////
        controllerInput.Update();
        ///////////////////////////////////////////////////////////

        translateRotateScale();
    }

    public float RotateAroundYSpeed = 2.0f;
    public float RotateAroundXSpeed = 2.0f;
    public float RotateAroundZSpeed = 2.0f;

    public float MoveHorizontalSpeed = 0.01f;
    public float MoveVerticalSpeed = 0.01f;

    public float ScaleSpeed = 1f;

    private string lastButtonDown = string.Empty;
    private string lastButtonUp = string.Empty;

    private void translateRotateScale()
    {
        float moveHorizontal = MoveHorizontalSpeed * controllerInput.GetAxisLeftThumbstickX();
        float moveVertical = MoveVerticalSpeed * controllerInput.GetAxisLeftThumbstickY();
        transform.Translate(moveHorizontal, moveVertical, 0);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, maxRayDistance))
        {
            transform.Translate(0, 0, hit.distance - 0.01f);
            mKey = hit.transform.gameObject;

            if (mKey.tag == "Keys")
            {
                if(mKey != prev_mKey && prev_mKey.tag == "Keys")
                {
                    prev_mKey.GetComponent<KeyboardItem>().r.material = mKey.GetComponent<KeyboardItem>().theme.GetThemeValue(KeyState.Default);
                }
                mKey.GetComponent<KeyboardItem>().r.material = mKey.GetComponent<KeyboardItem>().theme.GetThemeValue(KeyState.Focused);
            }
            
            if (controllerInput.GetButtonDown(ControllerButton.A))
            {
                if (mKey == GameObject.Find("Start"))
                {
                    Manager.GetComponent<Controller>().startClicked();
                    Manager.GetComponent<PhraseSet>().listStart();
                }
                else if (mKey == GameObject.Find("Submit"))
                {
                    Manager.GetComponent<Controller>().submitClicked();
                    Manager.GetComponent<PhraseSet>().listSubmit();
                }
                else if (mKey.tag == "Keys")
                {
                    mKey.GetComponent<KeyboardItem>().r.material = mKey.GetComponent<KeyboardItem>().theme.GetThemeValue(KeyState.Pressed);
                    KeyboardMaster.GetComponent<KeyboardCreator>().HandleTap(mKey.GetComponent<KeyboardItem>());
                }
            }
            prev_mKey = mKey;
        }
        else
        {
            transform.Translate(0, 0, 2.0f - transform.position.z);
        }
    }

    private void setLastButtonDown()
    {
        if (controllerInput.GetButtonDown(ControllerButton.A))
            lastButtonDown = "A";

        if (controllerInput.GetButtonDown(ControllerButton.B))
            lastButtonDown = "B";

        if (controllerInput.GetButtonDown(ControllerButton.X))
            lastButtonDown = "X";

        if (controllerInput.GetButtonDown(ControllerButton.Y))
            lastButtonDown = "Y";

        if (controllerInput.GetButtonDown(ControllerButton.LeftShoulder))
            lastButtonDown = "LB";

        if (controllerInput.GetButtonDown(ControllerButton.RightShoulder))
            lastButtonDown = "RB";

        if (controllerInput.GetButtonDown(ControllerButton.View))
            lastButtonDown = "SHOW ADDRESS";

        if (controllerInput.GetButtonDown(ControllerButton.Menu))
            lastButtonDown = "SHOW MENU";

        if (controllerInput.GetButtonDown(ControllerButton.LeftThumbstick))
            lastButtonDown = "LEFT STICK CLICK";

        if (controllerInput.GetButtonDown(ControllerButton.RightThumbstick))
            lastButtonDown = "RIGHT STICK CLICK";

        if (controllerInput.GetButtonDown(ControllerButton.DPadDown))
            lastButtonDown = "DPadDown";

        if (controllerInput.GetButtonDown(ControllerButton.DPadUp))
            lastButtonDown = "DPadUp";

        if (controllerInput.GetButtonDown(ControllerButton.DPadLeft))
            lastButtonDown = "DPadLeft";

        if (controllerInput.GetButtonDown(ControllerButton.DPadRight))
            lastButtonDown = "DPadRight";
    }

    private void setLastButtonUp()
    {
        if (controllerInput.GetButtonUp(ControllerButton.A))
            lastButtonUp = "A";

        if (controllerInput.GetButtonUp(ControllerButton.B))
            lastButtonUp = "B";

        if (controllerInput.GetButtonUp(ControllerButton.X))
            lastButtonUp = "X";

        if (controllerInput.GetButtonUp(ControllerButton.Y))
            lastButtonUp = "Y";

        if (controllerInput.GetButtonUp(ControllerButton.LeftShoulder))
            lastButtonUp = "LB";

        if (controllerInput.GetButtonUp(ControllerButton.RightShoulder))
            lastButtonUp = "RB";

        if (controllerInput.GetButtonUp(ControllerButton.View))
            lastButtonUp = "SHOW ADDRESS";

        if (controllerInput.GetButtonUp(ControllerButton.Menu))
            lastButtonUp = "SHOW MENU";

        if (controllerInput.GetButtonUp(ControllerButton.LeftThumbstick))
            lastButtonUp = "LEFT STICK CLICK";

        if (controllerInput.GetButtonUp(ControllerButton.RightThumbstick))
            lastButtonUp = "RIGHT STICK CLICK";

        if (controllerInput.GetButtonUp(ControllerButton.DPadDown))
            lastButtonUp = "DPadDown";

        if (controllerInput.GetButtonUp(ControllerButton.DPadUp))
            lastButtonUp = "DPadUp";

        if (controllerInput.GetButtonUp(ControllerButton.DPadLeft))
            lastButtonUp = "DPadLeft";

        if (controllerInput.GetButtonUp(ControllerButton.DPadRight))
            lastButtonUp = "DPadRight";
    }
}