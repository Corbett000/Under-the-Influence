using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingController : MonoBehaviour
{
    private PostProcessVolume volume;
    private Bloom bloom;
    private Vignette vignette;
    private ChromaticAberration cAberration;

    float DEFAULT_BLOOM;
    public int EFFECT_WEAROUT_TIME = 20;
    private float DEFAULT_DRIFT_FACTOR;
    public float DRUNK_DRIFT_FACTOR = .95f;

    private float effectWearoutTimer;

    Queue currentEffects = new Queue();

    public carcontroller playerScript;
    public Camera myCamera;
    

    // Start is called before the first frame update
    void Start()
    {
        volume = GetComponent<PostProcessVolume>();
        volume.profile.TryGetSettings(out bloom);
        volume.profile.TryGetSettings(out vignette);
        volume.profile.TryGetSettings(out cAberration);

        DEFAULT_BLOOM = bloom.intensity.value;
        DEFAULT_DRIFT_FACTOR = playerScript.driftFactor;

        //bloom.intensity.value = 2f;
        //cAberration.intensity.value = 0f;
    }


    void Update()
    {
        if (effectWearoutTimer > 0) effectWearoutTimer -= Time.deltaTime;
        else
        {
            effectWearoutTimer += EFFECT_WEAROUT_TIME;

            if (currentEffects != null && currentEffects.Count > 0) EndEffect((int)currentEffects.Dequeue());
        }

        foreach(int effect in currentEffects)
        {
            ComputeEffect(effect);
        }
    }


    public void QueueRandomEffect(float intensity)
    {
        //Make sure the range is (0, # of effects)
        currentEffects.Enqueue((int)Random.Range(0, 5));
    }


    public void ComputeEffect(int effect)
    {
        switch (effect)
        {
            case 0:
                vignette.intensity.value = Mathf.Sin(Time.realtimeSinceStartup);
                break;
            case 1:
                cAberration.intensity.value = 2 * playerScript.drunkfactor;
                break;
            case 2:
                playerScript.driftFactor = DRUNK_DRIFT_FACTOR;
                break;
            case 3:
                playerScript.drunkSteering = 2 * playerScript.drunkfactor;
                break;
            case 4:
                myCamera.orthographicSize = 2 + Mathf.Abs((2)*(Mathf.Sin(Time.time)));
                break;
            default:
                Debug.Log("Undefined effect number " + effect + " in PostProcessingController.ComputeEffect()");
                break;
        }
    }

    public void EndEffect(int effect)
    {
        if (currentEffects.Contains(effect)) return;

        switch(effect)
        {
            case 0:
                vignette.intensity.value = 0;
                break;
            case 1:
                cAberration.intensity.value = 0;
                break;
            case 2:
                playerScript.driftFactor = DEFAULT_DRIFT_FACTOR;
                break;
            case 3:
                playerScript.drunkSteering = 0;
                break;
            case 4:
                myCamera.orthographicSize = 2;
                break;
            default:
                Debug.Log("Undefined effect number " + effect + " in PostProcessingController.EndEffect()");
                break;
        }
    }
}
