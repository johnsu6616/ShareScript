using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrailSequenceAnimation : MonoBehaviour
{
    public int tilingX = 1;
    public int tilingY = 1;
    public float scrollSpeed = 0.5f;
    public bool randomSwitch = false;
    public string textureName = "_MainTex";

    ParticleSystemRenderer rend;
    private float rand = 0;
    private int texCount;
    private float timeOffset;
    void OnEnable()
    {
        rend = GetComponent<ParticleSystemRenderer>();
        texCount = tilingX * tilingY;
        rand = Random.Range(texCount, 0);
    }

    void FixedUpdate()
    {
        texFrameUpdate();
    }

    void texFrameUpdate()
    {
        if (rend == null)
        {
            rend = GetComponent<ParticleSystemRenderer>();
        }

        if (randomSwitch == false)
        {
            timeOffset = Mathf.Round((Time.fixedTime * scrollSpeed) % (float)texCount);
        }
        else
        {
            timeOffset = Mathf.Round(((Time.fixedTime + (float)rand) * scrollSpeed) % (float)texCount);
        }

        float UVXOffset = (timeOffset / (float)tilingX) % 1;

        float UVYCarry = timeOffset - (timeOffset % (float)tilingX);
        float UVYOffset = 1 - ((UVYCarry / (float)tilingY) / (float)tilingX) % 1 - (1 / (float)tilingY);

        rend.trailMaterial.SetTextureScale(textureName, new Vector2((1 / (float)tilingX), (1 / (float)tilingY)));
        rend.trailMaterial.SetTextureOffset(textureName, new Vector2(UVXOffset, UVYOffset));
    }
}