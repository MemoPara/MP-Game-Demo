using System;
using UnityEngine;

public class AlbumEdit : MonoBehaviour
{
    public static Action<AlbumItem> OnDeleteAlbumItem;

    [SerializeField]
    private AlbumItem currentAlbumItem;
    private int albumIndex = -1;

    public void InitData(AlbumItem albumItem, int albumIndex)
    {
        this.currentAlbumItem = albumItem;
        this.albumIndex = albumIndex;
    }

    public void OnClickDelete()
    {
        OnDeleteAlbumItem?.Invoke(this.currentAlbumItem);
    }

    public void OnClickEdit()
    {
        this.currentAlbumItem.ShowEditAlbumNameState();
    }
}
