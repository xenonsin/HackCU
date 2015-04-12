using UnityEngine;
using System.Collections;

public class LeadingFootEmitter : MonoBehaviour
{
    public bool Left = false;
    public bool Right = false;
    private ParticleSystem m_ps;
	// Use this for initialization
	void Start ()
	{
        m_ps = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (Right && Player.Instance != null && (Player.Instance.LeadingFoot == Player.LeadingFootEnum.Left))
	        m_ps.enableEmission = true;
	    else if (Left && Player.Instance != null && (Player.Instance.LeadingFoot == Player.LeadingFootEnum.Right))
            m_ps.enableEmission = true;
	    else
	    {
            m_ps.enableEmission = false;
	    }
	}
}
