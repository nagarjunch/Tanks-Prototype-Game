using UnityEngine;
using System.Collections;

public class NetworkedCameraManagement : MonoBehaviour {

    private MyCameraControl m_CameraControl;

    private void Awake()
    {
        GameObject cameraRig = GameObject.FindGameObjectWithTag("CameraRig");

        if (cameraRig != null)
        {
            m_CameraControl = cameraRig.GetComponent<MyCameraControl>();
        }
    }

        // Use this for initialization
     void Start () {
        
	}

    private void OnEnable()
    {
        if(m_CameraControl.m_Targets == null)
        {
            m_CameraControl.m_Targets = new ArrayList();
            m_CameraControl.m_Targets.Add(transform);
        } else if(!m_CameraControl.m_Targets.Contains(transform))
        {
            m_CameraControl.m_Targets.Add(transform);
        }
    }

    private void OnDisable()
    {
        if (m_CameraControl.m_Targets.Contains(transform))
        {
            m_CameraControl.m_Targets.Remove(transform);
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
