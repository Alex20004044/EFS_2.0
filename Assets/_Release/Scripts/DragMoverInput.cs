using System.Collections;
using System.Collections.Generic;
using MSFD;
using MSFD.Data;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EFS
{
    public class DragMoverInput : InitBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField]
        DataField<bool> isSnapActive = new DataField<bool>("Settings/IsSnapActive");


        bool isActivated = false;
        Vector3 offset;

        ISelectionSource selectionSource;
        IDataStreamProvider dataStreamProvider;
        [Inject]
        public void Init(ISelectionSource selectionSource, IDataStreamProvider dataStreamProvider)
        {
            CheckInit();

            this.selectionSource = selectionSource;
            this.dataStreamProvider = dataStreamProvider;
        }

        void Start()
        {
            isSnapActive.Subscribe(dataStreamProvider).AddTo(this);
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            isActivated = true;
            offset = selectionSource.GetSelection().transform.position - Camera.main.ScreenToWorldPoint(transform.position);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            isActivated = false;
        }

        void Update()
        {
            if (!isActivated) return;
            var target = selectionSource.GetSelection().transform;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.position = offset + new Vector3(mousePosition.x, target.position.y, mousePosition.z);
            if (isSnapActive.Value)
                target.position = Vector3Int.RoundToInt(target.position);
        }

    }
}
