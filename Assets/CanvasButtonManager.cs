using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasButtonManager : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData) {
        print("enter");
    }

    public void OnPointerExit(PointerEventData eventData) {
        print("exit");
    }
}
