using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject item_dragged;
	public static Transform start_parent;
	public Vector3 start_position;
	public Transform Canvas;

	void Awake(){
		Canvas = GameObject.Find ("Canvas").GetComponent< Transform> ();
	}

	#region IBeginDragHandler implementation

	public void OnBeginDrag (PointerEventData eventData)
	{
		item_dragged = gameObject;
		item_dragged.transform.localScale = new Vector3 ( 1.25f, 1.25f, 1.25f);
		start_position = transform.position;
		start_parent = transform.parent;
		transform.SetParent( Canvas);
		GetComponent< CanvasGroup> ().blocksRaycasts = false;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		item_dragged.transform.localScale = new Vector3 ( 1f,1f,1f);
		item_dragged = null;
		GetComponent< CanvasGroup> ().blocksRaycasts = true;
		if (transform.parent == Canvas) {
			transform.SetParent( start_parent);
			transform.position = start_position;
		}
	}


	#endregion

}
