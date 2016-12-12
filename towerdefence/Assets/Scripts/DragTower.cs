using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragTower : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public static GameObject itemBeingDragged;
    public Camera camera;
    public Vector3 startPosition;
    public Mesh floor;
    public GameObject tower;

	public void OnBeginDrag(PointerEventData eventData)
    {
        itemBeingDragged = gameObject;
        startPosition = transform.position;
    }

    public void OnDrag (PointerEventData eventData)
    {
        RectTransform rectTransform = itemBeingDragged.GetComponent<RectTransform>();

        transform.position = Input.mousePosition;
        if (transform.position.y > startPosition.y + 10)
        {
            Vector3 p = camera.ScreenToViewportPoint(rectTransform.localPosition);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        RaycastHit hit;
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            Vector3 hitPos = hit.point;
            print("xpint = " + hitPos.x + " ypoint=" + hitPos.y);
            GameObject newTower = Instantiate(tower) as GameObject;
            newTower.transform.position = new Vector3(hitPos.x, .4f, hitPos.z);
            AstarPath.active.Scan();
		}

        
        itemBeingDragged = null;
        transform.position = startPosition;
    }
}
