using UnityEngine.UI;

namespace CookieNoir.VDay
{
    public class NonDrawingGraphic : Graphic
    {
        public override void SetMaterialDirty() { }

        public override void SetVerticesDirty() { }

        protected override void OnPopulateMesh(VertexHelper vh)
        {
            vh.Clear();
        }
    }
}
