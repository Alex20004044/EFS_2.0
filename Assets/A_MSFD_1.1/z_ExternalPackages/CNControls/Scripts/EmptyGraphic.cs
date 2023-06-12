using UnityEngine.UI;
namespace MSFD
{
	namespace CnControls
	{
		public class EmptyGraphic : Graphic
		{
			protected override void OnPopulateMesh(VertexHelper vh)
			{
				vh.Clear();
			}
		}
	}
}