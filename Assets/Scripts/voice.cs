using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Application = UnityEngine.Application;

public class Voice : MonoBehaviour // Changed the class name to Voice
{
    public Button recordButton; // Added
    public Text responseText; // Added
    public Dropdown microphoneDropdown; // Added

    private AudioClip audioClip; // Added
    private bool isRecording = false; // Added
    private string selectedMicrophone; // Added

    void Start()
    {
        recordButton.onClick.AddListener(OnRecordButtonClick); // Added
        PopulateMicrophoneDropdown(); // Added
    }

    void PopulateMicrophoneDropdown() // Added method
    {
        microphoneDropdown.ClearOptions();
        List<string> options = new List<string>(Microphone.devices);
        microphoneDropdown.AddOptions(options);
        if (options.Count > 0)
        {
            selectedMicrophone = options[0];
        }
        microphoneDropdown.onValueChanged.AddListener(delegate { OnMicrophoneSelected(microphoneDropdown); });
    }

    void OnMicrophoneSelected(Dropdown dropdown) // Added method
    {
        selectedMicrophone = dropdown.options[dropdown.value].text;
    }

    public void OnRecordButtonClick() // Added method
    {
        if (!isRecording)
        {
            StartRecording();
        }
        else
        {
            StopRecording();
            StartCoroutine(SendAudioToServer());
        }
    }

    public void StartRecording() // Added method
    {
        isRecording = true;
        audioClip = Microphone.Start(selectedMicrophone, false, 10, 44100);
        recordButton.GetComponentInChildren<Text>().text = "Stop Recording"; // Added
    }

    public void StopRecording() // Added method
    {
        isRecording = false;
        Microphone.End(selectedMicrophone);
        recordButton.GetComponentInChildren<Text>().text = "Start Recording"; // Added
    }

    public IEnumerator SendAudioToServer() // Added method
    {
        string filePath = Path.Combine(Application.persistentDataPath, "audio.wav");
        SaveWavFile(filePath, audioClip);

        byte[] audioData = File.ReadAllBytes(filePath);
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", audioData, "audio.wav", "audio/wav");

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000/transcribe", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                StartCoroutine(GetGPTResponse(response));
            }
        }

        File.Delete(filePath);
        Debug.Log("Temporary audio file deleted: " + filePath);
    }

    public IEnumerator GetGPTResponse(string transcribedText) // Added method
    {
        WWWForm form = new WWWForm();
        form.AddField("prompt", transcribedText);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000/gpt", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                responseText.text = response; // Added
            }
        }
    }

    public void SaveWavFile(string filePath, AudioClip clip) // Added method
    {
        byte[] wavFile = WavUtility.FromAudioClip(clip);
        File.WriteAllBytes(filePath, wavFile);
        Debug.Log("Audio file saved at: " + filePath);
    }
}
