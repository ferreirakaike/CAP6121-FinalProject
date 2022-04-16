using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using UnityEngine.Windows.Speech;
using System.Linq;

public class VoiceRecognition : MonoBehaviour
{
    static string YourSubscriptionKey;
    static string YourServiceRegion;

    private UnityEngine.Windows.Speech.KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    static void OutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
    {
        switch (speechRecognitionResult.Reason)
        {
            case ResultReason.RecognizedSpeech:
                Debug.Log($"RECOGNIZED: Text={speechRecognitionResult.Text}");
                CheckCommand(speechRecognitionResult.Text.ToUpper());
                break;
            case ResultReason.NoMatch:
                Debug.Log($"NOMATCH: Speech could not be recognized.");
                break;
            case ResultReason.Canceled:
                var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Debug.Log($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Debug.Log($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    Debug.Log($"CANCELED: Double check the speech resource key and region.");
                }
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ReadInKey();

        actions.Add("command", GetCommand);

        keywordRecognizer = new UnityEngine.Windows.Speech.KeywordRecognizer(actions.Keys.ToArray(), ConfidenceLevel.High);
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void ReadInKey()
    {
        string path = "Assets/Resources/key.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);

        YourSubscriptionKey = reader.ReadLine();
        YourServiceRegion = reader.ReadLine();

        reader.Close();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("V key was pressed.");
            GetCommand();
        }
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
    {
        Debug.Log("Recognized " + speech.text + " keyword.");
        actions[speech.text].Invoke();
    }

    async static void GetCommand()
    {
        var speechConfig = SpeechConfig.FromSubscription(YourSubscriptionKey, YourServiceRegion);
        speechConfig.SpeechRecognitionLanguage = "en-US";

        //To recognize speech from an audio file, use `FromWavFileInput` instead of `FromDefaultMicrophoneInput`:
        //using var audioConfig = AudioConfig.FromWavFileInput("YourAudioFile.wav");
        using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
        using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

        Debug.Log("Speak into your microphone.");
        var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
        OutputSpeechRecognitionResult(speechRecognitionResult);
    }

    static void CheckCommand(string command)
    {
        if (command.ToUpper().Contains("CAPTURE"))
        {
            GameObject tempGameObject;
            tempGameObject = GameObject.Find("CaptureCamera");

            CameraCapture tempCameraCapture;
            tempCameraCapture = tempGameObject.GetComponent<CameraCapture>();

            tempCameraCapture.Capture();
        }
    }
}
