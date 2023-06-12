using System.Collections;
using System.Collections.Generic;
using MSFD;
using MSFD.AS;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EFS
{
    public class DragMover : InitBehaviour, IDragHandler, IBeginDragHandler
    {
        ISelectionSource selectionSource;
        Vector3 startPressPos;
        [Inject]
        public void Init(ISelectionSource selectionSource)
        {
            CheckInit();

            this.selectionSource = selectionSource;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            //selectionSource.DeselectAll();
            startPressPos = eventData.pointerPressRaycast.worldPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Transform target = selectionSource.GetSelection().transform;
            //target.position += (transform.position - target.position) + (eventData.pointerPressRaycast.worldPosition - startPressPos);
          
        }

        
    }
}
