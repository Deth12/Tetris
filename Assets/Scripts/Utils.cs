using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour {

    public static Bounds GetRenderBounds(GameObject obj)
    {
        var bounds = new  Bounds(Vector3.zero,Vector3.zero);
        var render = obj.GetComponent<Renderer>();
        return render != null? render.bounds : bounds;
    }

    public static Bounds GetBounds(GameObject obj){
        Bounds bounds;
        Renderer childRender;
        bounds = GetRenderBounds(obj);
        if((int)bounds.extents.x == 0){
            bounds = new Bounds(obj.transform.position,Vector3.zero);
            foreach (Transform child in obj.transform) {
                childRender = child.GetComponent<Renderer>();
                if (childRender) {
                    bounds.Encapsulate(childRender.bounds);
                }else{
                    bounds.Encapsulate(GetBounds(child.gameObject));
                }
            }
        }
        return bounds;
    }

    public static Vector3 Center (GameObject obj) 
    {
        return Utils.GetBounds(obj).center;
    }
    
    public static int Mod (int n, int m)
    {
        return ((n % m) + m) % m;
    }
}
