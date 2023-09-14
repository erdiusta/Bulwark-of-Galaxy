using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceFieldManager : MonoBehaviour
{
    [SerializeField] Color32 color1;
    [SerializeField] Color32 color2;
    [SerializeField] Color32 color3;
    [SerializeField] Color32 color4;
    [SerializeField] Color32 color5;

    SpriteRenderer[] spriteRenderers; 
    public GameObject forceField;
    public bool forceFieldEnabled;
    float timer;
    float timer2;

    void Awake() 
    {
        spriteRenderers = forceField.GetComponentsInChildren<SpriteRenderer>();
    }

    void Update()
    {
        Color32[] colors = new Color32[] {color1, color2, color3, color4, color5};
        foreach(SpriteRenderer sprite in spriteRenderers) 
        {
            ChangeSpriteColor(sprite, colors, Random.Range(0, colors.Length));    
        }

        float duration_time = 8f;
        timer += Time.deltaTime;
        if(duration_time < timer)
        {
            DisableForceField();
            timer = 0;
        }
        ControlForceField();
    } 

    void ChangeSpriteColor(SpriteRenderer sprite, Color32[] colors, int index) 
    {
        sprite.color = colors[index];
    }

    void ControlForceField()
    {
        if (forceFieldEnabled)
            forceField.gameObject.SetActive(true);
        else
            forceField.gameObject.SetActive(false);
    }

    void DisableForceField()
    {
       forceFieldEnabled = false; 
       forceField.gameObject.SetActive(false); 
    }
}
