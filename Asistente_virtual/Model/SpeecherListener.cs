using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace Asistente_virtual.Model
{
    public class SpeecherListener
    {
        SpeechConfig speechConfig { get; }
        AudioConfig audioConfig { get; }

        public SpeechRecognizer speechRecognizer { get; }

        public SpeecherListener()
        {
            speechConfig = SpeechConfig.FromSubscription(Startup.CognitiveServiceKey, Startup.CognitiveServiceRegion);
            speechConfig.SpeechRecognitionLanguage = "es-MX";
            audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
        }

        public async Task Initialize()
        {
            await speechRecognizer.StartContinuousRecognitionAsync();
        }
    }
}
