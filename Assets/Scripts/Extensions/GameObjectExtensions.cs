using UnityEngine;

namespace Tetris.Extensions
{
    public static class GameObjectExtensions 
    {
        public static void AlignCenter(this GameObject obj)
        {
            var center = obj.GetBounds().center;
            obj.transform.position += obj.transform.position - center;
        }

        private static Bounds GetBounds(this GameObject obj)
        {
            var bounds = GetRenderBounds(obj);
            if((int)bounds.extents.x == 0)
            {
                bounds = new Bounds(obj.transform.position,Vector3.zero);
                foreach (Transform child in obj.transform)
                {
                    var childRender = child.GetComponent<Renderer>();
                    if (childRender) 
                    {
                        bounds.Encapsulate(childRender.bounds);
                    }
                    else
                    {
                        bounds.Encapsulate(GetBounds(child.gameObject));
                    }
                }
            }
            return bounds;
        }

        private static Bounds GetRenderBounds(GameObject obj)
        {
            var bounds = new  Bounds(Vector3.zero,Vector3.zero);
            var render = obj.GetComponent<Renderer>();
            return render != null? render.bounds : bounds;
        }
    }
}

