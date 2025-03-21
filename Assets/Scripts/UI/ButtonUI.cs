using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Sprite defaultSprite;
    [SerializeField]
    private Color defaultColor;
    [SerializeField]
    private Sprite highlightedSprite;
    [SerializeField]
    private Color highlightedColor;
    [SerializeField]
    private Sprite clickedSprite;
    [SerializeField]
    private Color clickedColor;
    [SerializeField]
    private float clickedColorDuration;
    [SerializeField]
    private UnityEvent OnClick;

    Image image;
    // used for saving color state after click color duration
    private Color savedColor;
    private Sprite savedSprite;
    private bool clicking;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        savedSprite = highlightedSprite;
        savedColor = highlightedColor;
        if (!clicking)
        {
            image.sprite = savedSprite;
            image.color = savedColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        savedSprite = defaultSprite;
        savedColor = defaultColor;
        if (!clicking)
        {
  
            image.sprite = savedSprite;
            image.color = savedColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.color = clickedColor;
        image.sprite = clickedSprite;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(ClickEffects());
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick.Invoke();

    }

    private IEnumerator ClickEffects()
    {
        clicking = true;
        yield return new WaitForSeconds(clickedColorDuration);
        image.color = savedColor;
        image.sprite = savedSprite;
        clicking = false;
    }
}
