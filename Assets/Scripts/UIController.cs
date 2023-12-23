using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [Header("Interactable Info")]
    [SerializeField] private CanvasGroup interactableInfo;
    [SerializeField] private Image interactableIcon;
    [SerializeField] private TMP_Text interactableText;

    [Header("Crosshair")]
    [SerializeField] private Image crosshair;
    [SerializeField] private Sprite normalCrosshair;
    [SerializeField] private Sprite interactCrosshair;
    [SerializeField] private TMP_Text crosshairText;

    [Header("Inventory")]
    [SerializeField] private CanvasGroup inventory;

    private void Start() {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        interactableInfo.gameObject.SetActive(false);

        crosshair.sprite = normalCrosshair;
        crosshair.gameObject.SetActive(true);
        SetCrosshairText("");

    }

    public void ShowNormalCrosshair() {

        crosshair.sprite = normalCrosshair;

    }

    public void ShowInteractCrosshair() {

        crosshair.sprite = interactCrosshair;

    }

    public void HideCrosshair() {

        crosshair.gameObject.SetActive(false);

    }

    public void SetCrosshairText(string text) {

        crosshairText.text = text;

    }

    public void ShowInteractableInfo(Interactable interactable) {

        interactableIcon.sprite = interactable.GetIcon();
        interactableText.text = interactable.GetName();
        LayoutRebuilder.ForceRebuildLayoutImmediate(interactableInfo.GetComponent<RectTransform>());
        interactableInfo.gameObject.SetActive(true);

    }

    public void HideInteractableInfo() {

        interactableInfo.gameObject.SetActive(false);

    }
}
