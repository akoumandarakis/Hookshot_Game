using System;
using UnityEngine;
using Prime31;

public class CameraFollow2D : MonoBehaviour
{
	public Transform target;

	public float damping = 0.00f;
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

	public bool ZoomOut = true;
	private bool ZoomedOut;

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
				float angleOfWeapon = targetObject.GetComponent<PlayerScript> ().GetDirectionAiming ().z;
				Vector3 mousePos = Input.mousePosition;
				Vector3 vectorToMouse = targetObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, transform.position.z - Camera.main.transform.position.z));
				Vector2 vectorToMouse2D = new Vector2 (vectorToMouse.x, vectorToMouse.y);
				float distanceToMouse = vectorToMouse2D.magnitude;

				if (distanceToMouse < offset) 
				{
					newPos = Vector3.SmoothDamp (transform.position, new Vector3 (aheadTargetPos.x + (distanceToMouse * Mathf.Cos(Mathf.Deg2Rad * angleOfWeapon)), aheadTargetPos.y + (distanceToMouse * Mathf.Sin(Mathf.Deg2Rad * angleOfWeapon)), aheadTargetPos.z), ref m_CurrentVelocity, damping * cameraSpeed);
				} 
				else 
				{
					newPos = Vector3.SmoothDamp (transform.position, new Vector3 (aheadTargetPos.x + (offset * Mathf.Cos(Mathf.Deg2Rad * angleOfWeapon)), aheadTargetPos.y + (offset * Mathf.Sin(Mathf.Deg2Rad * angleOfWeapon)), aheadTargetPos.z), ref m_CurrentVelocity, damping * cameraSpeed);
				}


			}

			if (ZoomOut && !ZoomedOut) {
				this.gameObject.GetComponent<Camera>().orthographicSize += 0.005f;

				if (this.gameObject.GetComponent<Camera>().orthographicSize >= 3)
				{
					ZoomedOut = true;
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
        m_OffsetZ = transform.position.z - 1;
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

