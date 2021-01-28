using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetList : MonoBehaviour
{
    public RectTransform content;
    public Scrollbar scrollbar;

    public float scrollSize;
    public float scrollOffset;
    public Vector2 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = content.anchoredPosition;
        //RectTransform찾기
        RectTransform[] childs = content.GetComponentsInChildren<RectTransform>();

        float total = 0;
        
        foreach (RectTransform rect in childs)
        {
            if (content.GetInstanceID() != rect.GetInstanceID())
            {
                total += rect.rect.height;
            }
        }

        scrollOffset = total - content.rect.height;
        scrollSize = total / (childs.Length - 1);

        scrollbar.size = scrollSize / scrollOffset;
        scrollbar.numberOfSteps = Mathf.RoundToInt(scrollOffset / scrollSize);

        // Listener
        scrollbar.onValueChanged.AddListener(OnScrollValueChanged);

    }

    void OnScrollValueChanged(float _value)
    {
        float value = Mathf.Clamp(_value, 0.0f, 1.0f);

        content.anchoredPosition = new Vector2(initialPos.x, initialPos.y + (value * scrollOffset));
    }
}
