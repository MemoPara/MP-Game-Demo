using UnityEngine;

public class UIUploadToFurniture : MonoBehaviour
{
    public FurnitureUpload currentFurniture;

    public void InitData(FurnitureUpload furnitureUpload)
    {
        this.currentFurniture = furnitureUpload;
    }

    public void OnClickExportData()
    {
        this.currentFurniture?.ExportData();
    }

    public void OnClickUploadData()
    {
        this.currentFurniture?.UploadData();
    }
}
