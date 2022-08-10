using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] GameObject teleportationArea;
    [SerializeField] float movementRate = 10f;

    public Transform vrCamera;

    public SteamVR_Action_Vector2 a_strafe;
    public SteamVR_Action_Vector2 a_upDown;
    public SteamVR_Action_Boolean a_rotate;

    public string strafeHint = "Strafe";

    public Hand lHand;
    public Hand rHand;

    public float jumpAngle = 45;
    public float minHeight = 2f;

    public Plane ground;

    Coroutine strafeHintCoroutine;
    Coroutine upDownHintCoroutine;

    float startY;
    float teleportAreaMinHeight;
    public float angularSpeed = 1;
    public bool strafeEnabled = true;

    bool dpad_right = false;

    // Start is called before the first frame update
    void Start()
    {
        if (lHand == null)
            lHand = this.GetComponent<Hand>();

        if (rHand == null)
            rHand = this.GetComponent<Hand>();

        if (a_strafe == null)
        {
            Debug.LogError("No strafe action assigned");
            return;
        }

        a_strafe.AddOnChangeListener(OnStrafeActionChange, lHand.handType);

        if (a_upDown == null)
        {
            Debug.LogError("No up_down action assigned");
            return;
        }

        a_upDown.AddOnChangeListener(OnUpDownActionChange, rHand.handType);

        //if (strafeEnabled)
        //    strafeHintCoroutine = StartCoroutine(StrafeHintCoroutine());
        //upDownHintCoroutine = StartCoroutine(UpDownHintCoroutine());

        startY = transform.position.y;
        teleportAreaMinHeight = teleportationArea.transform.position.y;

        a_rotate.AddOnStateDownListener(OnRotateActionChange, rHand.handType);

        
    }

    private void OnRotateActionChange(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if(a_upDown[rHand.handType].axis.x > 0)
            transform.Rotate(0, jumpAngle, 0);
        else
            transform.Rotate(0, -jumpAngle, 0);
    }

    private IEnumerator StrafeHintCoroutine()
    {
        //while (true)
        //{
            ControllerButtonHints.ShowTextHint(lHand, a_strafe, strafeHint);
            yield return null;
        //}
    }

    private IEnumerator UpDownHintCoroutine()
    {
        //while (true)
        //{
            ControllerButtonHints.ShowTextHint(rHand, a_upDown, "Up/Down and Rotate");
            yield return null;
        //}
    }

    private void OnUpDownActionChange(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {      
        UpDown(axis);
        /*leave hints always on
        if (upDownHintCoroutine != null)
        {
            StopCoroutine(upDownHintCoroutine);
            ControllerButtonHints.HideTextHint(rHand, a_upDown);
            upDownHintCoroutine = null;
        }
        */
    }

    private void OnStrafeActionChange(SteamVR_Action_Vector2 fromAction, SteamVR_Input_Sources fromSource, Vector2 axis, Vector2 delta)
    {
        
        //todo add a conditional that axis are non-zero
        if(strafeEnabled)
            Strafe(axis);
        /*leave hints always on
        if (strafeHintCoroutine != null)
        {
            StopCoroutine(strafeHintCoroutine);
            ControllerButtonHints.HideTextHint(lHand, a_strafe);
            strafeHintCoroutine = null;
        }
        */
    }


    private void Strafe(Vector2 axis)
    {
        //transform.position = new Vector3(transform.position.x + (axis.x * movementRate), transform.position.y, transform.position.z + (axis.y * movementRate));
        float y = transform.position.y;
        Vector3 startPos = transform.position;
        transform.Translate(axis.x * movementRate * Time.deltaTime, 0f, axis.y * movementRate * Time.deltaTime, vrCamera.transform);
        /*int layerMask = 1 << 11;
        if(Physics.Raycast(transform.position, -transform.up, layerMask))
        {
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(startPos.x, y, startPos.z);
        }*/
        transform.position = new Vector3(transform.position.x, y, transform.position.z);
    }

    private void UpDown(Vector2 axis)
    {
        
        float yUpdate = axis.y * movementRate * Time.deltaTime;
        //if (transform.position.y + yUpdate < startY)
        if(transform.position.y + yUpdate < minHeight)
            yUpdate = 0;
                
        transform.position = new Vector3(transform.position.x, transform.position.y + yUpdate, transform.position.z);
                
        if (transform.position.y < startY)
            yUpdate = 0;
        teleportationArea.transform.position = new Vector3(teleportationArea.transform.position.x, teleportationArea.transform.position.y + yUpdate, teleportationArea.transform.position.z);
        //transform.Rotate(0, axis.x * angularSpeed,0);
    }



    // Update is called once per frame
    void Update()
    {
        //if (strafeEnabled)
        //    strafeHintCoroutine = StartCoroutine(StrafeHintCoroutine());
        
        //Vector3 pointOnGround = ground.ClosestPointOnPlane(transform.position);
        //transform.position = new Vector3(pointOnGround.x, startY, pointOnGround.z);
        //if (transform.position.y < startY)
        //   transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        /*if (Input.GetKey(KeyCode.UpArrow))//get key stroke
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
            teleportationArea.transform.position = new Vector3(teleportationArea.transform.position.x, transform.position.y, teleportationArea.transform.position.z);
            //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.1f, transform.localPosition.z);
            print("Up");
            //print(transform.position);
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
            //transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 0.1f, transform.localPosition.z);
            teleportationArea.transform.position = new Vector3(teleportationArea.transform.position.x, transform.position.y, teleportationArea.transform.position.z);
            print("Down");
            //print(transform.position);
        }
        */
    }


}
