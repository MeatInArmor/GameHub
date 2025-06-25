
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
    

public class DragNDropPicture : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event System.Action doInventoryOpenClose;
    [SerializeField] public ItemObject parent;


    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(GetComponentInParent<CanvasController>().gameObject.transform);
        transform.SetAsLastSibling();
        doInventoryOpenClose?.Invoke();

    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }
    public void OnEndDrag(PointerEventData eventData)
    {

        transform.SetParent(parent.transform);
        transform.position = parent.transform.position;

        SoldierStand solStand = GetComponentInParent<CanvasController>().soldierStand;
        {
            if (solStand.IsPointerOverUI())
            {
                parent.PictureDrop();

            }
            if (parent.dragged)
                transform.SetAsFirstSibling();
            else transform.SetSiblingIndex(1);

        }


    }
}

