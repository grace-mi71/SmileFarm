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
    public void SelectBlueBrush() {PlayClick(); currentBrushMaterial = blueMaterial; ApplyMaterialToAllFlowers();}
    public void SelectPinkBrush() {PlayClick(); currentBrushMaterial = pinkMaterial; ApplyMaterialToAllFlowers();}
    public void SelectWhiteBrush() {PlayClick(); currentBrushMaterial = whiteMaterial; ApplyMaterialToAllFlowers();}

    // Click Sound
    private void PlayClick()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlayButtonClick();
            
        // Close Shop Panel
        MainMenuUIManager.Instance.OnShopBtnClicked();
    }

    private void ApplyMaterialToAllFlowers()
    {
        // Find all tage "Flower"
        GameObject[] flowers = GameObject.FindGameObjectsWithTag("Flower");
        foreach (GameObject flower in flowers)
        {
            MeshRenderer[] renderers = flower.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer render in renderers)
            {
                render.material = currentBrushMaterial;
            }
        }
    }
}