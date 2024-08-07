using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using Application = UnityEngine.Application;

public class VoiceRecorder : MonoBehaviour
{
    public Button recordButton;
    public MediaTypeNames.Text _responseText;
    public Dropdown microphoneDropdown;

    private AudioClip audioClip;
    private bool isRecording = false;
    private string selectedMicrophone;

    void Start()
    {
        recordButton.onClick.AddListener(OnRecordButtonClick);
        PopulateMicrophoneDropdown();
    }

    void PopulateMicrophoneDropdown()
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

    void OnMicrophoneSelected(Dropdown dropdown)
    {
        selectedMicrophone = dropdown.options[dropdown.value].text;
    }

    public void OnRecordButtonClick()
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

    public void StartRecording()
    {
        isRecording = true;
        audioClip = Microphone.Start(selectedMicrophone, false, 10, 44100);
        recordButton.GetComponentInChildren<MediaTypeNames.Text>().text = "Stop Recording";
    }

    public void StopRecording()
    {
        isRecording = false;
        Microphone.End(selectedMicrophone);
        recordButton.GetComponentInChildren<MediaTypeNames.Text>().text = "Start Recording";
    }

    public IEnumerator SendAudioToServer()
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

    public IEnumerator GetGPTResponse(string transcribedText)
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
                responseText.text = response;
            }
        }
    }

    public void SaveWavFile(string filePath, AudioClip clip)
    {
        byte[] wavFile = WavUtility.FromAudioClip(clip);
        File.WriteAllBytes(filePath, wavFile);
        Debug.Log("Audio file saved at: " + filePath);
    }
}