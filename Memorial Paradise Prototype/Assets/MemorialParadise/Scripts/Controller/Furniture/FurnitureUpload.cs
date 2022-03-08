using UnityEngine;

public class FurnitureUpload : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    [SerializeField]
    private Material newMaterial;
    [SerializeField]
    private Texture texture;
    private void Start()
    {
        this.meshRenderer = GetComponent<MeshRenderer>();
        if (this.meshRenderer)
        {
            //this.meshRenderer.material = this.newMaterial;
            //Material newMat = new Material(Shader.Find("Diffuse"));
            Material newMat = this.meshRenderer.material;
            newMat.SetTexture("_MainTex", this.texture);
            this.meshRenderer.material = newMat;
        }
    }

    // Update is called once per frame
    private void Update()
    {

    }
}
