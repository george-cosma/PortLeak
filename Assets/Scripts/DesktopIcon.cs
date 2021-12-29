using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DesktopIcon : MonoBehaviour, IPointerClickHandler
{
    public GameObject WindowPrefab;
    public GameObject BarTaskPrefab;

    public float DoubleClickIntreval = 1;
    private float m_clickTimer = 0;
    private bool m_clickedOnce = false;


	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (m_clickedOnce)
        {
            if (m_clickTimer < DoubleClickIntreval)
            {
                m_clickTimer += Time.deltaTime;
            }
            else
            {
                m_clickTimer = 0;
                m_clickedOnce = false;
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!m_clickedOnce) m_clickedOnce = true;
        else
        {
            m_clickedOnce = false;
            m_clickTimer = 0;
            WindowManager.instance.CreateWindow(WindowPrefab, BarTaskPrefab);
        }
    }
}
