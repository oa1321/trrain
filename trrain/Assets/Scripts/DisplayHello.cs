using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayHello : MonoBehaviour
{
    public TextMeshProUGUI helloText; // Assign your TextMeshPro in the Inspector
    public RawImage img;
    private bool isPlayerInside = false;

    private void Start()
    {
        helloText.gameObject.SetActive(false); // Hide text at the start
        img.gameObject.SetActive(false);
    }

    private void Update()
    {
        // If player is inside the trigger and presses the E key
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            helloText.gameObject.SetActive(true); // Show text
            img.gameObject.SetActive(true);
            StartCoroutine(ChangeText()); // Start changing text
        }
    }

    private IEnumerator ChangeText()
    {
        helloText.text = "Hello";
        yield return new WaitForSeconds(2);
        helloText.text = "Welcome to my world";
        yield return new WaitForSeconds(2);
        helloText.text = "the house is in your left";
        yield return new WaitForSeconds(2);
        helloText.gameObject.SetActive(false); // Hide text after displaying
        img.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that triggered the event is player
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object that left the trigger is player
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInside = false;
            helloText.gameObject.SetActive(false); // Hide text
            img.gameObject.SetActive(false);
        }
    }
}