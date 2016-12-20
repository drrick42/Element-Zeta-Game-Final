using UnityEngine;
using System.Collections;

public class DarkSolidBehavior : LightReactiveBlockBehavior {

    protected override void UpdateLit()
    {
        var rend = GetComponent<Renderer>();
        bool lit = LitByLight;
        if (Settings.RENDER_MATERIALIZE)
        {
            rend.material.SetFloat("_Cutoff",
            Mathf.Clamp(rend.material.GetFloat("_Cutoff") + Settings.MATERIALIZE_SPEED * Time.deltaTime *
            (lit ? 1 : -1), 0, 1));
        }
        else
            rend.enabled = !lit;
        GetComponent<Collider>().enabled = !lit;

    }
}
