using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace MSFD.Data
{
    public interface ICompositeDataContainer
    {
        bool TryCreateCompositeData(string path, string leftPathPart, string rightPathPart);
        bool TryChangeCompositeData(string path, string newLeftPathPart, string rightPathPart);
        bool TryDeleteCompositeData(string path);
    }
}
