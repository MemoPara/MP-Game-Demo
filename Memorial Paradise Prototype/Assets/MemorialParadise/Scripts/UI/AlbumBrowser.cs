using System.Collections.Generic;
using UnityEngine;

public class AlbumBrowser : MonoBehaviour
{
    //Reference
    [SerializeField]
    private GameObject emptyPanel;
    [SerializeField]
    private GameObject albumPanel;
    [SerializeField]
    private GameObject albumItemPrefab;
    [SerializeField]
    private GameObject albumEditPrefab;
    [SerializeField]
    private GameObject albumBrowserContent;

    private List<AlbumItem> albumItems = new List<AlbumItem>();
    private AlbumItem currentSelectAlbum;
    private GameObject currentAlbumEditObject;

    private void OnEnable()
    {
        AlbumItem.OnSelectAlbum += OnSelectAlbumHandler;
        AlbumItem.OnEditAlbum += OnEditAlbumHandler;
        AlbumEdit.OnDeleteAlbumItem += OnDeleteAlbumItemHandler;
    }

    private void OnDisable()
    {
        AlbumItem.OnSelectAlbum -= OnSelectAlbumHandler;
        AlbumItem.OnEditAlbum -= OnEditAlbumHandler;
        AlbumEdit.OnDeleteAlbumItem -= OnDeleteAlbumItemHandler;
    }

    private void Awake()
    {
        if (this.albumItems.Count > 0)
        {
            this.emptyPanel.SetActive(false);
            this.albumPanel.SetActive(true);
        }
        else
        {
            this.emptyPanel.SetActive(true);
            this.albumPanel.SetActive(false);
        }
    }

    private void OnDeleteAlbumItemHandler(AlbumItem albumItem)
    {
        Destroy(albumItem.gameObject);
        this.albumItems.Remove(albumItem);
        Destroy(this.currentAlbumEditObject);
    }


    private void OnEditAlbumHandler(AlbumItem album)
    {
        if (this.currentAlbumEditObject)
        {
            Destroy(this.currentAlbumEditObject);
        }
        //Get current album index
        int albumIndex = -1;
        for (int i = 0; i < this.albumBrowserContent.transform.childCount; i++)
        {
            if (this.albumBrowserContent.transform.GetChild(i) == album.transform)
            {
                albumIndex = i;
                break;
            }
        }
        this.currentAlbumEditObject = Instantiate(this.albumEditPrefab, this.albumBrowserContent.transform);
        this.currentAlbumEditObject.GetComponent<AlbumEdit>()?.InitData(album, albumIndex);
        if (albumIndex < this.albumBrowserContent.transform.childCount - 1)
        {
            this.currentAlbumEditObject.transform.SetSiblingIndex(albumIndex + 1);
        }
    }
    private void OnSelectAlbumHandler(AlbumItem album)
    {
        ShowSelectFolder(album);
    }

    public void ShowSelectFolder(AlbumItem folderItem)
    {
        if (this.currentSelectAlbum == null)
        {
            folderItem.ShowSelectState();
            this.currentSelectAlbum = folderItem;
        }
        else
        {
            if (this.currentSelectAlbum != folderItem)
            {
                this.currentSelectAlbum.ShowDeSelectState();
                folderItem.ShowSelectState();
                this.currentSelectAlbum = folderItem;
                Destroy(this.currentAlbumEditObject);
            }
        }
    }

    public void ClearSelectingFolder()
    {
        this.currentSelectAlbum?.ShowDeSelectState();
        this.currentSelectAlbum = null;
        Destroy(this.currentAlbumEditObject);
    }

    public void CreateNewAlbum()
    {
        if (this.albumItems.Count > 4)
        {
            return;
        }

        GameObject newAlbumObject = Instantiate(this.albumItemPrefab, this.albumBrowserContent.transform);
        AlbumItem album = newAlbumObject.GetComponent<AlbumItem>();
        album.InitData("New album");
        this.albumItems.Add(album);
        this.emptyPanel.SetActive(false);
        this.albumPanel.SetActive(true);
    }

    public void OnClickEventAction()
    {
        Debug.Log("SELECT");
    }


}
