using UnityEngine;

public class ShopColorManager : MonoBehaviour
{
    [SerializeField] private Material currentBrushMaterial = null;

    public Material blueMaterial;
    public Material pinkMaterial;
    public Material whiteMaterial;

    public void SelectBlueBrush() { currentBrushMaterial = blueMaterial; }
    public void SelectPinkBrush() { currentBrushMaterial = pinkMaterial; }
    public void SelectWhiteBrush() { currentBrushMaterial = whiteMaterial; }

    void Update()
    {
        if (currentBrushMaterial != null && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Flower"))
                { 
                    MeshRenderer[] renderers = hit.collider.GetComponentsInChildren<MeshRenderer>();
                    foreach (MeshRenderer r in renderers)
                    {
                        r.material = currentBrushMaterial;
                    }
                }
            }
        }
    }
}