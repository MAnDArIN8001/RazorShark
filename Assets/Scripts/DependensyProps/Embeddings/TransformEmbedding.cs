using UnityEngine;

public static class TransformEmbedding
{
    public static void Flip(this Transform transform)
    {
        Vector3 flipedScale = new Vector3() 
        {
            x = -transform.localScale.x,
            y = transform.localScale.y,
            z = transform.localScale.z
        };

        transform.localScale = flipedScale;
    }
}
