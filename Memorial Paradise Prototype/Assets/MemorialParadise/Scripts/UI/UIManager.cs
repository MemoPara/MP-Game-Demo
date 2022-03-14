using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject UploadFurniturePanel;

    private void OnEnable()
    {
        FurnitureUpload.OnShow += FurnitureUploadOnShowHandler;
    }
    private void OnDisable()
    {
        FurnitureUpload.OnShow -= FurnitureUploadOnShowHandler;
    }

    private void Start()
    {
        this.UploadFurniturePanel.SetActive(false);
    }

    private void FurnitureUploadOnShowHandler(FurnitureUpload furniture)
    {
        this.UploadFurniturePanel.SetActive(true);
        Time.timeScale = 0;
        this.UploadFurniturePanel.GetComponent<UIUploadToFurniture>().InitData(furniture);
        furniture.isOpenningUI = true;
    }

    public void CloseUI()
    {
        Time.timeScale = 1;
        this.UploadFurniturePanel.SetActive(false);
        this.UploadFurniturePanel.GetComponent<UIUploadToFurniture>().currentFurniture.isOpenningUI = false;
    }
}
