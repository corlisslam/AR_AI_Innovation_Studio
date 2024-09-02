using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TestTools;
using TMPro;

[TestFixture]
public class HomeControllerTests
{
    private HomeController homeController;
    private GameObject homePageButtonGameObject;
    private GameObject homePageInputFieldGameObject;
    private GameObject canvas;

    [SetUp]
    public void SetUp()
    {
        homePageInputFieldGameObject = new GameObject("HomePageInputFieldGameObject");

        RectTransform rectTransform = homePageInputFieldGameObject.AddComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(300, 60); // Set the size of the input field

        TMP_InputField homePageInputField = homePageInputFieldGameObject.AddComponent<TMP_InputField>();

        GameObject textAreaObject = new GameObject("TextArea");
        textAreaObject.transform.SetParent(homePageInputFieldGameObject.transform, false);

        TextMeshProUGUI actualText = textAreaObject.AddComponent<TextMeshProUGUI>();
        actualText.text = "Visitor";
        actualText.fontSize = 24;
        actualText.color = Color.black;
        homePageInputField.textComponent = actualText;

        Image background = homePageInputFieldGameObject.AddComponent<Image>();
        background.color = Color.white;
        homePageInputField.targetGraphic = background;

        homePageButtonGameObject = new GameObject("HomePageButtonGameObject");
        homePageButtonGameObject.AddComponent<Button>();

        canvas = new GameObject("Canvas");

        homePageInputFieldGameObject.transform.SetParent(canvas.transform, false);
        homePageButtonGameObject.transform.SetParent(canvas.transform, false);

        homeController = canvas.AddComponent<HomeController>();
    }

    [Test]
    public void WhenHitScanImage_SetPlayerPref()
    {
        // 1. Arrange

        homeController.homePageInputField = canvas.transform.Find("HomePageInputFieldGameObject").GetComponent<TMP_InputField>();

        // 2. Act

        homeController.SetPlayerPref();

        // 3. Assert

        Assert.AreEqual("Visitor", PlayerPrefs.GetString("PlayerName"));
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(homePageInputFieldGameObject);
        Object.DestroyImmediate(homeController);
        Object.DestroyImmediate(homePageButtonGameObject);
        Object.DestroyImmediate(canvas);
    }
}