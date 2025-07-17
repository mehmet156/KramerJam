using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyableObject : MonoBehaviour
{
    public float duration = 2f;

    public void DestroyEffect()
    {

        if (transform.parent != null&&transform.parent.gameObject.CompareTag("TrueObj"))
        {
            transform.parent.DOScale(Vector3.zero, duration).SetEase(Ease.InBack);

            var renderer = GetComponent<Renderer>();
            if (renderer != null && renderer.material.HasProperty("_Color"))
            {
                Color startColor = renderer.material.color;
                renderer.material.DOColor(new Color(startColor.r, startColor.g, startColor.b, 0f), duration);
            }

            DOVirtual.DelayedCall(duration, () => Destroy(transform.parent.gameObject));
        }
        else
        {
            transform.DOScale(Vector3.zero, duration).SetEase(Ease.InBack);

            var renderer = GetComponent<Renderer>();
            if (renderer != null && renderer.material.HasProperty("_Color"))
            {
                Color startColor = renderer.material.color;
                renderer.material.DOColor(new Color(startColor.r, startColor.g, startColor.b, 0f), duration);
            }

            DOVirtual.DelayedCall(duration, () => Destroy(gameObject));
        }



        


    }
}
