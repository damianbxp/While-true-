using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler {
    private Canvas canvas;

    private RectTransform rectTransform;
    private Transform programPanel;

    public int commandSlot = 0;
    private float snapY;
    private int hoveringAddedCommandId;
    public int commandTypeId;

    private Vector3 basePosition;
    float refY;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start() {
        canvas = transform.parent.transform.parent.transform.parent.GetComponent<Canvas>();
        programPanel = transform.parent.transform.parent.transform.parent.GetChild(0).transform;
        basePosition = rectTransform.anchoredPosition;
        refY = programPanel.GetChild(0).transform.GetChild(0).transform.position.y;
    }

    public void OnPointerDown(PointerEventData eventData) {

    }

    public void OnBeginDrag(PointerEventData eventData) {

    }

    public void OnEndDrag(PointerEventData eventData) {
    }

    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //Debug.DrawLine(rectTransform.anchoredPosition, rectTransform.anchoredPosition + new Vector2(50, 0), Color.green);
        hoveringAddedCommandId = Mathf.FloorToInt((refY - rectTransform.transform.position.y) / 30);
    }

    public void OnDrop(PointerEventData eventData) {

        int commandsCount = programPanel.GetChild(0).childCount - 2;
        if(commandsCount < 0) Debug.LogError("Command Count Errror");

        if(rectTransform.transform.position.x > 80 && transform.parent.transform.parent.name == "Program") {
            programPanel.GetComponent<Program>().deleteCommand(commandSlot);
        }

        hoveringAddedCommandId = Mathf.Clamp(hoveringAddedCommandId, 0, commandsCount);

        if(transform.parent.transform.parent.name == "AvailableCommands") { // adding command to program
            programPanel.GetComponent<Program>().addCommand(commandTypeId);
            programPanel.GetComponent<Program>().changeOrder(commandsCount, hoveringAddedCommandId);
            commandSlot = hoveringAddedCommandId;
            rectTransform.anchoredPosition = basePosition;
        } else if(transform.parent.transform.parent.name == "Program") { // modifing commands
            programPanel.GetComponent<Program>().changeOrder(commandSlot, hoveringAddedCommandId);
            commandSlot = hoveringAddedCommandId;
        }

    }
}
