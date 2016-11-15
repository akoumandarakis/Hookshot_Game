using System;
using UnityEngine;
using Prime31;

public class CameraFollow2D : MonoBehaviour
{
	public Transform target;

	public float damping = 0.1f;
	public float lookAheadFactor = 0.5f;
	public float lookAheadReturnSpeed = 0.5f;
	public float lookAheadMoveThreshold = 0.1f;

	public float offset = 1;
	public float cameraSpeed = 2;

	public bool platformSnap = true;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;

	private Transform currentTarget;

	private GameObject targetObject;


    // Use this for initialization
    private void Start()
    {
		
    }

    // Update is called once per frame
    private void Update()
    {
        if (target != null)
        {
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (currentTarget.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = currentTarget.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
			Vector3 newPos = new Vector3 ();

			if (targetObject != null) 
			{
				if (targetObject.GetComponent<PlayerScript> ().GetDirectionAiming () == "Right") 
				{
					newPos = Vector3.SmoothDamp (transform.position, new Vector3 (aheadTargetPos.x + offset, aheadTargetPos.y, aheadTargetPos.z), ref m_CurrentVelocity, damping * cameraSpeed);
				} 
				else
				{
					newPos = Vector3.SmoothDamp(transform.position, new Vector3 (aheadTargetPos.x - offset, aheadTargetPos.y, aheadTargetPos.z), ref m_CurrentVelocity, damping * cameraSpeed);
				}
			}

            transform.position = newPos;

            m_LastTargetPosition = currentTarget.position;
        }
    }

	public void startCameraFollow (GameObject newTarget){
		currentTarget = newTarget.transform;
		targetObject = newTarget;
		m_LastTargetPosition = currentTarget.position;
        m_OffsetZ = transform.position.z;
        transform.parent = null;
	}
	
	
	public void stopCameraFollow (){
		currentTarget = this.transform;
		m_LastTargetPosition = currentTarget.position;
		m_OffsetZ = 0;
	}

	public void setDamping (float value){
		damping = value;
	}

	public void setTarget (GameObject newTarget){
		currentTarget = newTarget.transform;
	}
}

