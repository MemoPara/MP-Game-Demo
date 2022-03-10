using System.IO;
using UnityEditor;
using UnityEngine;

public class FurnitureUpload : MonoBehaviour
{
    private MeshRenderer meshRenderer;

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
        Debug.Log("Upload UI...");
        //string path = EditorUtility.OpenFilePanel("Select image for upload...", "", "png");
        string path = EditorUtility.SaveFilePanel("Save file", Application.dataPath, "TVTexture", "png");
        if (this.meshRenderer)
        {
            Texture texture = this.meshRenderer.material.mainTexture;
            Texture2D texture2D = Decompress(texture);
            File.WriteAllBytes(path, texture2D.EncodeToPNG());
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
