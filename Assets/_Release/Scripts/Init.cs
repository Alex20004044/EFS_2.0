using System.Collections;
using System.Collections.Generic;
using CorD.SparrowInterfaceField;
using MSFD;
using UnityEngine;

namespace EFS
{
    public class Init : MonoBehaviour
    {
        [SerializeField]
        InterfaceField<ISelectionSource> selectionSource;

        void Awake()
        {
            ServiceLocator.Register<ISelectionSource>(selectionSource.i);
        }
    }
}
