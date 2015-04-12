using UnityEngine;
using System.Collections;

public class ForceController : MonoBehaviour
{
    public static ForceController Instance;
    public float ReverseVelocity = 0.5f;
    private float ForwardVelocity;
    public float ForwardVelocityGrounded = 2.0f;
    public float ForwardVelocityInAir = 0.5f;
    public float ForwardVelocityTurning = 4.0f;
    public float DragGrounded = 0.1f;
    public float DragTurning = 0.5f;
    public float DragInAir = 0f;

    public float turningAngleSweetSpot = 30f;
    private Rigidbody rb;
    private Collider col;

    public Vector3 directionPoint;
    public float boardToVelocityAngle;
    public float distToGround;



    void Awake()
    {
        Instance = this;
    }

    void OnDestroy()
    {
        Instance = null;
    }
	// Use this for initialization
	void Start ()
	{

	    col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
	    distToGround = col.bounds.extents.y;

	}

        public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }

    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    public Vector3 GetDirectionPoint()
    {
        return directionPoint;
    }

   

	// Update is called once per frame
	void Update ()
	{
	    


	    directionPoint = transform.position + rb.velocity.normalized;

        //Debug.DrawLine(transform.position, transform.forward, Color.blue);
        //Draws a line to find the dirction of movement.
	    Debug.DrawLine(transform.position, directionPoint, Color.red);

        //Get's the angle between the Board facing point and the velocity.
	    Vector3 line1 = (directionPoint - transform.position).normalized;
	    Vector3 line2 = (Player.Instance.GetBoardFacingPoint() - transform.position).normalized;
	    boardToVelocityAngle = Vector3.Angle(line1, line2);
        //        Debug.Log("Angle: " + angleBetweenVelocityAndBoardForward);

        //Debug.Log("angle: " + boardToVelocityAngle);



	    Player p = Player.Instance;
	    if (p != null)
	    {
	        if (p.LeadingFoot == Player.LeadingFootEnum.Neutral)
	        {
	            if (rb != null)
	            {
                    rb.drag = DragTurning;
                    rb.AddForce(-rb.velocity * ReverseVelocity);
                    //Debug.Log("Applying reverse force");
	            }
	        }
	        else
	        {
	            if (rb != null)
	            {
	                if (p.LeadingFoot == Player.LeadingFootEnum.Left)
	                {
	                    if (IsGrounded())
	                    {

	                        if (boardToVelocityAngle <= (90 - turningAngleSweetSpot) ||
	                            boardToVelocityAngle >= (90 + turningAngleSweetSpot)) // Put More Force left
	                        {
	                            ForwardVelocity = ForwardVelocityTurning;
	                            rb.drag = DragTurning;
	                        }
	                        else // Put more force right
	                        {
	                            ForwardVelocity = ForwardVelocityGrounded;
	                            rb.drag = DragGrounded;
	                        }
	                    }
	                    else
	                    {
                            ForwardVelocity = ForwardVelocityInAir;
                            rb.drag = DragInAir;

	                    }

	                    rb.AddForce(p.LeftFootReference.transform.forward * ForwardVelocity);
	                    
	                }
                    else //Leading Foot Right
	                {
                        if (IsGrounded())
                        {

                            if (boardToVelocityAngle <= (90 - turningAngleSweetSpot) ||
                                boardToVelocityAngle >= (90 + turningAngleSweetSpot)) // Put More Force left
                            {
                                ForwardVelocity = ForwardVelocityTurning;
                                rb.drag = DragTurning;
                            }
                            else // Put more force right
                            {
                                ForwardVelocity = ForwardVelocityGrounded;
                                rb.drag = DragGrounded;
                            }
                        }
                        else
                        {
                            ForwardVelocity = ForwardVelocityInAir;
                            rb.drag = DragInAir;

                        }
	                    rb.AddForce(p.RightFootReference.transform.forward * ForwardVelocity);
	                }
                        

                    //Debug.Log("Applying " + p.LeadingFoot.ToString() + " force!");
	            }
	        }
	    }
	}
}
