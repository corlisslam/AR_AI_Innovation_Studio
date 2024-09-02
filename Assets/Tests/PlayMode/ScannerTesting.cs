//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;
//using System;

// Test for error handling
[TestFixture]
public class ScannerTesting
{
    private GameObject testerObject;
    private GameObject arSessionObject;
    private GameObject cameraObject;


    [SetUp]
    public void SetUp()
    {
        testerObject = new GameObject("TesterObject");

        testerObject.AddComponent<ARTrackedImageManager>();

        arSessionObject = new GameObject("ARSessionObject");
        arSessionObject.AddComponent<ARSession>();

        cameraObject = new GameObject("ARCamera");
        Camera camera = cameraObject.AddComponent<Camera>();
        cameraObject.tag = "MainCamera";

        testerObject.AddComponent<Scanner>();
    }

    [Test]
    public void WhenInvalidImageName()
    {
        Scanner scanner = testerObject.GetComponent<Scanner>();

        bool isLoaded = false;
      
        scanner.LoadSceneBasedOnTrackedImage("MockReferenceImageName", success => isLoaded = success);

        Assert.That(isLoaded, Is.False);
    }

    [Test]
    public void WhenInvalidSceneIndex()
    {
        Scanner scanner = testerObject.GetComponent<Scanner>();

        bool isLoaded = false;
        scanner.LoadSceneBasedOnTrackedImage("GetInvalidIndex", success => isLoaded = success);

        Assert.That(isLoaded, Is.False);
    }

    [Test]
    public void WhenCannotLoadScene()
    {
        Scanner scanner = testerObject.GetComponent<Scanner>();

        bool isLoaded = false;
        scanner.LoadScene(5, "MockSceneName", "MockReferenceImageName", success => isLoaded = success, 11);

        Assert.That(isLoaded, Is.False);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(testerObject);
        Object.DestroyImmediate(arSessionObject);
        Object.DestroyImmediate(cameraObject);
    }

}
