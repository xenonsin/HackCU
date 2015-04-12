using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public GameObject DummyRoller;
    public GameObject LeftFootReference;
    public GameObject RightFootReference;
    public float RotationIncrement = 1.0f;
    public float RotationIncrementSmall = 0.5f;
    public LeadingFootEnum LeadingFoot = LeadingFootEnum.Neutral;

    public GameObject feet;

    public float NeutralSweetSpot = 0.08f;
    public float FacingDownHillDegrees = 100f;

    //private Vector3 HitPoint;
    private Vector3 BoardFacingPoint;

    public Vector3 GetBoardFacingPoint()
    {
        return BoardFacingPoint;
    }

    public enum LeadingFootEnum
    {
        Left,
        Right,
        Neutral,
    }
	// Use this for initialization
	void Start ()
	{
	    transform.position = DummyRoller.transform.position;
        for(int i = 0; i < 25; i++)
	    RotateRight();
	    //transform.rotation = DummyRoller.transform.rotation;

	}

    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }


	
	// Update is called once per frame
	void Update ()
	{


        if(Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Space))
            Application.LoadLevel(0);
	    BoardFacingPoint = ForceController.Instance.transform.position + feet.transform.forward;

        Debug.DrawLine(ForceController.Instance.transform.position, BoardFacingPoint, Color.blue);

	    transform.position = DummyRoller.transform.position;
	    RaycastHit hit;
	    Physics.Raycast(transform.position, Vector3.down, out hit);
	    if (Physics.Raycast(transform.position, Vector3.down, 1.0f))
	    {
            //HitPoint = hit.point;
            //transform.up = hit.normal;
	        Vector3 myUp = transform.up;
	        myUp = Vector3.Lerp(myUp, hit.normal, 0.05f);
	        transform.up = myUp;

	        //transform.up = new Vector3((hit.normal.x + transform.up.x) / 3.0f, (hit.normal.y + transform.up.y) / 3f, (hit.normal.z + transform.up.z) / 3f);
	    }
        //desiredRotation = hit.normal;
        //Vector3.Lerp(transform.position, DummyRoller.transform.position, 0.5f);
	    UpdateLeadingFoot();
	    CheckJoystick();
        //transform.rotation = Quaternion.FromToRotation(transform.up, desiredRotation) * transform.rotation;
	}

    private void UpdateLeadingFoot()
    {

        float leftDistance = Vector3.Distance(LeftFootReference.transform.position, ForceController.Instance.GetDirectionPoint());
        float rightDistance = Vector3.Distance(RightFootReference.transform.position, ForceController.Instance.GetDirectionPoint());

        if (LeftFootReference != null && RightFootReference != null)
        {

            if (Mathf.Abs(rightDistance - leftDistance) < NeutralSweetSpot)
                LeadingFoot = LeadingFootEnum.Neutral;
            else if (rightDistance > leftDistance)
                LeadingFoot = LeadingFootEnum.Left;
            else if (leftDistance > rightDistance)
                LeadingFoot = LeadingFootEnum.Right;
        }

        //Debug.Log(LeadingFoot);
    }

    private void CheckJoystick()
    {
        if (joystick.Instance != null)
        {
            joystick stick = joystick.Instance;
            int failCount = 0;
            if (stick.TL < 20.0f && stick.TL > -20.0f) failCount++;
            if (stick.TR < 20.0f && stick.TR > -20.0f) failCount++;
            if (stick.BL < 20.0f && stick.BL > -20.0f) failCount++;
            if (stick.BR < 20.0f && stick.BR > -20.0f) failCount++;
            if (failCount > 2) return;
            //Debug.Log("Leading Foot: " + LeadingFoot);
            switch (LeadingFoot)
            {
                case LeadingFootEnum.Right:
                case LeadingFootEnum.Left:
                case LeadingFootEnum.Neutral:
                    {
                        if ((stick.TL > stick.BL) && (stick.TR < stick.BR)) // valid turning form
                        {
                            RotateRight();
                            //Debug.Log("Rotate Right");
                        }
                        else if((stick.BL > stick.TL) && (stick.BR < stick.TR)) // valid turning form
                        {
                            RotateLeft();
                            //Debug.Log("Rotate Left");
                        }
                        else // this means we're on an edge, find out which edge and either slow down or flip
                        {
                            if ((stick.BL > stick.TL) && (stick.BR > stick.TR)) // heel edge
                            {
                                SlowDown();
                            }
                            else if((stick.TL > stick.BL) && (stick.TR > stick.BR)) // toe edge
                            {
                                SlowDown();
                            }
                        }
                    }
                    break;
            }
        }
    }

    //private bool FacingDownHill
    //{
    //    get
    //    {
    ////        float angleBetweenVelocityAndBoardForward = Vector3.Angle(BoardFacingPoint, ForceController.Instance.GetDirectionPoint());
    ////        Debug.Log("Angle: " + angleBetweenVelocityAndBoardForward);
    //     Vector3 cross = Vector3.Cross(camPos, parent);

    //    if (cross.z > 0)
    //        enemyAngle = 360 - enemyAngle;
    ////        if (angleBetweenVelocityAndBoardForward < FacingDownHillDegrees)
    //        {
    //            Debug.Log("Facing Down Hill");
    //            return true;
    //        }
    //        else
    //        {
    //            Debug.Log("Facing Up Hill");
    //            return false;
    //        }
    //    }
    //}

    private void RotateLeft()
    {
        //desiredRotation = new Vector3(desiredRotation.x, desiredRotation.y - RotationIncrement, desiredRotation.z);
        transform.GetChild(0).Rotate(0.0f, -RotationIncrement, 0.0f, Space.Self);
    }

    private void RotateRight()
    {
        //desiredRotation = new Vector3(desiredRotation.x, desiredRotation.y + RotationIncrement, desiredRotation.z);
        transform.GetChild(0).Rotate(0.0f, RotationIncrement, 0.0f, Space.Self);
    }

    private void RotateLeftSmall()
    {
        //desiredRotation = new Vector3(desiredRotation.x, desiredRotation.y - RotationIncrementSmall, desiredRotation.z);
        transform.GetChild(0).Rotate(0.0f, -RotationIncrementSmall, 0.0f, Space.Self);
    }

    private void RotateRightSmall()
    {
        //desiredRotation = new Vector3(desiredRotation.x, desiredRotation.y + RotationIncrementSmall, desiredRotation.z);
        transform.GetChild(0).Rotate(0.0f, RotationIncrement, 0.0f, Space.Self);
    }

    private void Flip()
    {
        Debug.LogError("FLIP!");
        // restart after delay
    }

    private void SlowDown()
    {
        
    }

    private void SlowDownSmall()
    {
        
    }

}
