using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AlbumItem : MonoBehaviour, IPointerClickHandler
{
    public static Action<AlbumItem> OnSelectAlbum;
    public static Action<AlbumItem> OnEditAlbum;


    [SerializeField]
    private Image selectBackground;
    private float clicked = 0;
    private float clicktime = 0;
    private float clickdelay = 0.7f;
    private float currentTime = 0f;

    [SerializeField]
    private Image albumNameInputImage;
    [SerializeField]
    private InputField albumNameInputField;
    private string albumName;


    public void InitData(string albumName)
    {
        this.albumName = albumName;
        this.albumNameInputField.text = albumName;
    }

    public void ShowSelectState()
    {
        this.selectBackground.enabled = true;
    }

    public void ShowDeSelectState()
    {
        this.selectBackground.enabled = false;
        this.clicked = 0;
        this.clicktime = 0;
    }

    public void OnClickEventAction()
    {
        this.clicked++;
        //print("Clicked Time: " + this.clicked);
        if (this.clicked == 1)
        {
            OnSelectAlbum?.Invoke(this);
            StopAllCoroutines();
            StartCoroutine(CountDownDoubleClick());
            this.clicktime = 0f;
        }

        if (this.clicked > 1 && this.currentTime - this.clicktime < this.clickdelay)
        {
            this.clicked = 0;
            this.clicktime = 0;

            print("Double Click");
        }
        else if (this.clicked > 2 || this.currentTime - this.clicktime > 1f)
        {
            this.clicked = 0;
        }
    }

    private IEnumerator CountDownDoubleClick()
    {
        this.currentTime = 0f;
        while (this.currentTime < 15f)
        {
            yield return new WaitForSecondsRealtime(Time.fixedDeltaTime);
            this.currentTime += Time.fixedDeltaTime;
            //print(this.currentTime);
            //print("Time long  = " + (this.currentTime - this.clicktime));
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnEditAlbum?.Invoke(this);
            this.clicked = 0;
            this.clicktime = 0;
        }
    }

    public void ShowEditAlbumNameState()
    {
        this.albumNameInputImage.enabled = true;
        this.albumNameInputField.interactable = true;
        try
        {
            this.albumNameInputField.Select();
            this.albumNameInputField.onEndEdit.AddListener(delegate { StopInput(this.albumNameInputField); });
        }
        catch (Exception)
        {

        }

    }

    private void StopInput(InputField albumNameInputField)
    {

    }

    public void ShowCompleteEditNameState()
    {
        this.albumNameInputImage.enabled = false;
        EventSystem.current.SetSelectedGameObject(null);
        this.albumNameInputField.interactable = false;
        this.albumName = this.albumNameInputField.text;
    }

}
