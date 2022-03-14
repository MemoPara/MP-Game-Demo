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

        if (Input.GetMouseButtonDown(1) && Time.timeScale != 0)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f))
            {
                TryToTurnOff(hit);
            }
        }
    }

    private void TryToTurnOff(RaycastHit hit)
    {
        TvController tvController = hit.collider.GetComponent<TvController>();
        if (hit.collider && tvController)
        {
            tvController.TurnOnOff();
        }
    }

    private void TryToInteract(RaycastHit hit)
    {
        FurnitureUpload furniture = hit.collider.GetComponent<FurnitureUpload>();
        if (hit.collider && furniture)
        {
            furniture.OpenFurnitureUpload();
        }
    }
}
