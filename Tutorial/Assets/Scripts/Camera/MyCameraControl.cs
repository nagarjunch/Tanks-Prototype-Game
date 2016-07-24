using UnityEngine;
using System.Collections;

public class MyCameraControl : MonoBehaviour {

    public float m_DampTime = 0.2f;
    public float m_MinZoomSize = 5f;
    public float m_ScreenEdgeBuffer = 4f;
    public ArrayList m_Targets = new ArrayList();

    private Camera m_Camera;
    private Vector3 m_AverageTargetPosition;
    private Vector3 m_MoveVelocity;
    private float m_ZoomSpeed;


    void Awake () {
        m_Camera = GetComponentInChildren<Camera>();
	}
	
	void FixedUpdate () {
        Move();
        Zoom();
	}

    void Move()
    {
        m_AverageTargetPosition = FindAverageTargetPosition();

        if (!float.IsNaN(m_AverageTargetPosition.x) && !float.IsNaN(m_AverageTargetPosition.y) && !float.IsNaN(m_AverageTargetPosition.z))
        {
            transform.position = Vector3.SmoothDamp(transform.position, m_AverageTargetPosition, ref m_MoveVelocity, m_DampTime);
        }
    }

    Vector3 FindAverageTargetPosition()
    {
        Vector3 averagePostion = new Vector3();
        foreach(Transform target in m_Targets) {

            if (!target.gameObject.activeSelf)
                continue;

            averagePostion += target.position;
        }

        averagePostion = averagePostion / m_Targets.Count;
        return averagePostion;
    }

    void Zoom()
    {
        float targetSize = FindTargetSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, targetSize, ref m_ZoomSpeed, m_DampTime);
    }

    float FindTargetSize()
    {
        float size = 0f;
        Vector3 averageLocalPosition = transform.InverseTransformPoint(m_AverageTargetPosition);

        foreach(Transform target in m_Targets)
        {
            if (!target.gameObject.activeSelf)
                continue;

            Vector3 targetLocalPosition = transform.InverseTransformPoint(target.position);
            Vector3 camDesiredPosition = targetLocalPosition - averageLocalPosition;

            size = Mathf.Max(size, camDesiredPosition.y);
            size = Mathf.Max(size, camDesiredPosition.x);
        }

        size += m_ScreenEdgeBuffer;

        size = Mathf.Max(size, m_MinZoomSize);

        return size;
    }

    public void SetStartPositionAndSize()
    {
        FindAverageTargetPosition();

        transform.position = m_AverageTargetPosition;

        m_Camera.orthographicSize = FindTargetSize();
    }
}
