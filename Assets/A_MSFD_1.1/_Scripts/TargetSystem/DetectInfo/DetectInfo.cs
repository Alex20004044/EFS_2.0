using Sirenix.OdinInspector;
using UnityEngine;

namespace MSFD
{
    [System.Serializable]
    public struct DetectInfo
    {
        [SerializeField]
        ChooseLabelField targetTags;
        [SerializeField]
        LayerMask targetLayers;
        [FoldoutGroup("Extension")]
        [SerializeField]
        ChooseLabelField targetNames;
        [FoldoutGroup("Extension")]
        [SerializeField]
        ChooseColliderField targetColliders;
        [FoldoutGroup("Extension")]
        [Button]
        public bool IsTargetCorrect(Collider target)
        {
            return AS.Utilities.CompareLayers(target.gameObject.layer, targetLayers) && 
                targetTags.IsCorrect(target.tag) && 
                targetNames.IsCorrect(target.name) &&
                targetColliders.IsCorrect(target);
        }
        [FoldoutGroup("Extension")]
        [Button]
        /// <summary>
        /// Find correct item without cheking collider
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool IsTransormCorrect(Transform target)
        {
            return AS.Utilities.CompareLayers(target.gameObject.layer, targetLayers) &&
                targetTags.IsCorrect(target.tag) &&
                targetNames.IsCorrect(target.name);
        }
    }
}