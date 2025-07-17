using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class OutlineController : MonoBehaviour
{
    public string outlineMaterialName = "OutlinerMat";
    public Material outlineMaterial;
    public float maxRayDistance = 10f;
    public LayerMask targetLayers;
    public float outlineThickness;

    private GameObject lastOutlinedObject;
    private GameObject currentObj;

    
   
    
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, targetLayers))
        {
            currentObj = hit.collider.gameObject;

            // Eðer yeni bir objeye bakýyorsak outline güncelle
            if (currentObj != lastOutlinedObject)
            {
                SetOutlineSize(lastOutlinedObject, 0f); // Önceki objeyi kapat
                SetOutlineSize(currentObj, outlineThickness); // Yeni objeyi aç
                lastOutlinedObject = currentObj;
            }
        }
        else
        {
            SetOutlineSize(lastOutlinedObject, 0f);
            lastOutlinedObject = null;
            currentObj = null;
        }

        // Týklama kontrolü (her frame bakýlan objeye göre)
        if (Input.GetMouseButtonDown(0) && currentObj != null)
        {
            if (currentObj.CompareTag("ModernObj"))
            {
                currentObj.tag = "Untagged";
                
                    GameUIManager.Instance.IncreaseCollectbleObj();
                    Debug.Log("<color=green>doðru obje</color>");
                    currentObj.GetComponent<DestroyableObject>().DestroyEffect();
                // Destroy(currentObj);

                AudioClip chosenClip = Random.value < 0.5f ? AudioController.Instance.correct1 : AudioController.Instance.correct2;
                AudioController.Instance.effect.Stop(); // Eski efekt sesi varsa durdur
                AudioController.Instance.effect.clip = chosenClip;
                AudioController.Instance.effect.Play();


            }
            else if (currentObj.CompareTag("MedievalObj"))
            {
                currentObj.tag = "Untagged";
               
                
                    GameUIManager.Instance.DecreaseCollectbleObj();
                    Debug.Log("<color=red>yanlýþ obje</color>");
                    // Destroy(currentObj);
                    currentObj.GetComponent<DestroyableObject>().DestroyEffect();

                if (GameUIManager.Instance.health >= 1)
                {

                    AudioClip chosenClip = Random.value < 0.5f ? AudioController.Instance.wrong1 : AudioController.Instance.wrong2;
                    AudioController.Instance.effect.Stop(); // Eski efekt sesi varsa durdur
                    AudioController.Instance.effect.clip = chosenClip;
                    AudioController.Instance.effect.Play();
                }
               

            }
        }
    }

    void SetOutlineSize(GameObject obj, float size)
    {
        if (obj == null) return;

        MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
        if (renderer == null) return;

        foreach (var mat in renderer.materials)
        {
            if (mat.name.Contains(outlineMaterialName) || mat.shader.name.Contains("Outliner"))
            {
                if (mat.HasProperty("_Size"))
                {
                    mat.SetFloat("_Size", size);
                }
            }
        }
    }
    
}
