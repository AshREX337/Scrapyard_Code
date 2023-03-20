using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float dissapearTimer;
    private Color textColor;

    

    // Update is called once per frame
    void Awake()
    {
        textMesh= GetComponent<TextMeshPro>();
    }

    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        textColor = textMesh.color;
        dissapearTimer = 1f;
    }

    void Update()
    {
        float moveYSpeed = 20f;
        transform.position += new Vector3(0, moveYSpeed, 0) * Time.deltaTime;

        dissapearTimer -= Time.deltaTime;

        if(dissapearTimer < 0)
        {
            float dissapearSpeed = 3f;
            textColor.a -= dissapearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
