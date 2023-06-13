using System.Collections;
using System.Collections.Generic;
using MSFD;
using UnityEngine;

namespace EFS
{
    public class DeleteSelectable : MonoBehaviour, IActivatable
    {
        ISelectionSource selectableSource;

        [Inject]
        public void Init(ISelectionSource selectableSource)
        {
            this.selectableSource = selectableSource;
        }
        public void Activate()
        {
            GameObject selection = selectableSource.GetSelection();
            if (selection == null) return;
            //TODO: Need to implement proper interface
            if (selection.TryGetComponent<TesterForce>(out var testerForce))
                testerForce.transform.position = Vector3.zero;
            else
                InstantiateCore.Despawn(selection);
        }

    }
}
