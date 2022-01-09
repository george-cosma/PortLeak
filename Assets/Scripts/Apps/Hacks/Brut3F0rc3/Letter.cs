using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Letter : MonoBehaviour
{
    [SerializeField]
    protected float speed = 1f;

    public bool correct_letter = false;
    public RectTransform rt { get; private set; }
    public TMP_Text Label { get; private set; }

    private float? despawnHeight = null;

    // Start is called before the first frame update
    public void Start()
    {
        rt = GetComponent<RectTransform>();
        Label = rt.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (despawnHeight == null)
        {
            despawnHeight = rt.anchoredPosition.y - transform.parent.GetComponent<RectTransform>().rect.height - rt.rect.height;
        }

        if (rt.anchoredPosition.y < despawnHeight)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector2 newPosition = rt.anchoredPosition;
        newPosition.y -= speed * Time.fixedDeltaTime;

        rt.anchoredPosition = newPosition;
    }
}
