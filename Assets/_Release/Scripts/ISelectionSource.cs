using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EFS
{
    public interface ISelectionSource
    {
        GameObject GetSelection();
        void DeselectAll();
    }
}
