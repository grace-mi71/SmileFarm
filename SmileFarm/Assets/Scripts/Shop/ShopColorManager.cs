// Owner: Choi Eun-young
// Description: Manages a "brush" system to repaint objects tagged as "Flower" using Raycasting and material swapping.

using UnityEngine;

public class ShopColorManager : MonoBehaviour
{
    // The currently active material to be applied when clicking
    [SerializeField] private Material currentBrushMaterial = null;

    [Header("Available Materials")]
    public Material blueMaterial;
    public Material pinkMaterial;
    public Material whiteMaterial;

    // Public methods to be linked to UI Buttons or selection events
    public void SelectBlueBrush() { currentBrushMaterial = blueMaterial; }
    public void SelectPinkBrush() { currentBrushMaterial = pinkMaterial; }
    public void SelectWhiteBrush() { currentBrushMaterial = whiteMaterial; }

    void Update()
    {
        // Executes only if a brush is selected and the left mouse button is clicked
        if (currentBrushMaterial != null && Input.GetMouseButtonDown(0))
        {
            // Shoots a ray from the camera through the mouse position into the 3D world
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Checks if the hit object is designated as a "Flower"
                if (hit.collider.CompareTag("Flower"))
                {
                    // Retrieves all MeshRenderers in the object and its children to ensure the whole model changes color
                    MeshRenderer[] renderers = hit.collider.GetComponentsInChildren<MeshRenderer>();
                    foreach (MeshRenderer r in renderers)
                    {
                        // Swaps the existing material with the selected brush material
                        r.material = currentBrushMaterial;
                    }
                }
            }
        }
    }
}