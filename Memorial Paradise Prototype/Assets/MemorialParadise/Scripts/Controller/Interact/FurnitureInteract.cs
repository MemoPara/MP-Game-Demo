using UnityEngine;

public class FurnitureInteract : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                TryToInteract(hit);
            }
        }
    }

    private void TryToInteract(RaycastHit hit)
    {
        FurnitureUpload furniture = hit.collider.GetComponent<FurnitureUpload>();
        if (hit.collider && hit.collider.GetComponent<FurnitureUpload>())
        {
            furniture.OpenFurnitureUpload();
        }
    }
}
