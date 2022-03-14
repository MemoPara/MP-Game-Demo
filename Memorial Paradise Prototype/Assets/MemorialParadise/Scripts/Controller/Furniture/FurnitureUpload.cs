using SimpleFileBrowser;
using System;
using System.IO;
using UnityEngine;

public class FurnitureUpload : MonoBehaviour
{
    public static Action<FurnitureUpload> OnShow;

    public enum DataType { Picture, Sound, Video, Document };
    public DataType dataType;
    private MeshRenderer meshRenderer;
    public bool isOpenningUI = false;
    private string pathFile;

    private void Start()
    {
        this.meshRenderer = GetComponent<MeshRenderer>();
        if (this.meshRenderer)
        {
            //this.meshRenderer.material = this.newMaterial;
            //Material newMat = new Material(Shader.Find("Diffuse"));
            //Material newMat = this.meshRenderer.material;
            //newMat.SetTexture("_MainTex", this.texture);
            //this.meshRenderer.material = newMat;
        }
    }

    public void OpenFurnitureUpload()
    {
        if (!this.isOpenningUI && Time.timeScale != 0)
        {
            OnShow?.Invoke(this);
        }
    }

    public void ExportData()
    {
        switch (this.dataType)
        {
            case DataType.Picture:
                ExportTextureToPng();
                break;
            case DataType.Sound:
                break;
            case DataType.Video:
                break;
            case DataType.Document:
                break;
            default:
                break;
        }
    }

    public void UploadData()
    {
        switch (this.dataType)
        {
            case DataType.Picture:
                UploadPngTexture();
                break;
            case DataType.Sound:
                break;
            case DataType.Video:
                UploadVideo();
                break;
            case DataType.Document:
                break;
            default:
                break;
        }
    }

    private void UploadVideo()
    {
        //string path = EditorUtility.OpenFilePanel("Find new video: ", "", "mp4");
        //if (File.Exists(path))
        //{
        //    //byte[] dataBytes = File.ReadAllBytes(path);
        //    TvController tvController = GetComponent<TvController>();
        //    if (tvController)
        //    {
        //        tvController.ChangeVideo(path);
        //    }
        //}

        FileBrowser.SetFilters(false, "mp4");
        FileBrowser.ShowLoadDialog(OnSuccessUploadVideo, OnCancelUploadVideo, FileBrowser.PickMode.Files, false, "", null, "Find new video", "Select");
    }

    private void OnCancelUploadVideo()
    {

    }

    private void OnSuccessUploadVideo(string[] paths)
    {
        if (paths.Length > 0)
        {
            this.pathFile = paths[0];
            if (File.Exists(this.pathFile))
            {
                //byte[] dataBytes = File.ReadAllBytes(this.pathFile);
                TvController tvController = GetComponent<TvController>();
                if (tvController)
                {
                    tvController.ChangeVideo(this.pathFile);
                }
            }
        }
    }

    private void UploadPngTexture()
    {

        //string path = EditorUtility.OpenFilePanel("Find new texture: ", "", "png");
        FileBrowser.SetFilters(false, "png");
        FileBrowser.ShowLoadDialog(OnSuccessLoadTextureFile, OnCancelLoadTextureFile, FileBrowser.PickMode.Files, false, Application.dataPath, "", "Find new texture file in png");
    }

    private void OnCancelLoadTextureFile()
    {

    }

    private void OnSuccessLoadTextureFile(string[] paths)
    {
        if (paths.Length > 0)
        {
            this.pathFile = paths[0];
            if (File.Exists(this.pathFile))
            {
                Texture2D texture = null;
                Texture oldTexture = this.meshRenderer.material.mainTexture;
                byte[] dataBytes = File.ReadAllBytes(this.pathFile);
                texture = new Texture2D(oldTexture.width, oldTexture.height);
                texture.LoadImage(dataBytes);
                this.meshRenderer.material.mainTexture = texture;
            }
        }
    }

    private void ExportTextureToPng()
    {
        FileBrowser.SetFilters(false, "png");
        FileBrowser.ShowSaveDialog(OnSuccessExportTexture, OnCancelExportTexture, FileBrowser.PickMode.Files, false, "", "DefaultTexture.png", "Export");
    }

    private void OnCancelExportTexture()
    {

    }

    private void OnSuccessExportTexture(string[] paths)
    {
        if (paths.Length > 0)
        {
            this.pathFile = paths[0];
            if (this.meshRenderer && !string.IsNullOrEmpty(this.pathFile))
            {
                Texture texture = this.meshRenderer.material.mainTexture;
                Texture2D texture2D = Decompress(texture);
                File.WriteAllBytes(this.pathFile, texture2D.EncodeToPNG());
            }
        }
    }

    private Texture2D Decompress(Texture source)
    {
        RenderTexture renderTexture = RenderTexture.GetTemporary(
            source.width,
            source.height,
            0,
            RenderTextureFormat.Default,
            RenderTextureReadWrite.Linear
            );
        Graphics.Blit(source, renderTexture);
        RenderTexture previous = RenderTexture.active;
        RenderTexture.active = renderTexture;
        Texture2D readableTexture = new Texture2D(source.width, source.height);
        readableTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        readableTexture.Apply();
        RenderTexture.active = previous;
        RenderTexture.ReleaseTemporary(renderTexture);
        return readableTexture;
    }
}
