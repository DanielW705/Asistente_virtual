using Asistente_virtual.Model;
using Asistente_virtual.Views;
using Microsoft.CognitiveServices.Speech;
using System.Diagnostics;
using SpeechSynthesizer = System.Speech.Synthesis.SpeechSynthesizer;

namespace Asistente_virtual
{
    public partial class Form_asistente : Form
    {
        SpeecherListener speecherListener = new SpeecherListener();
        SpeechSynthesizer speecher = new SpeechSynthesizer();
        Dictionary<string, string> aplicaciones = Startup.BindDictionary();
        public Form_asistente()
        {
            InitializeComponent();
        }

        private async void Form_asistente_Load(object sender, EventArgs e)
        {
            speecherListener.speechRecognizer.Recognized += OnAudioRecognized;

            await speecherListener.Initialize();
        }

        private async void OnAudioRecognized(object sender, SpeechRecognitionEventArgs e)
        {
            await speecherListener.speechRecognizer.StopContinuousRecognitionAsync();
            switch (e.Result.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    {
                        Invoke((MethodInvoker)(() => txtbx_prueba.Text += $"Usted dijo: {e.Result.Text}\r\n"));
                        switch (e.Result.Text.ToLower())
                        {
                            case string s when s.Contains("hola") || s.Contains("buenos días") || s.Contains("buenas"):
                                {
                                    speecher.Speak("Hola usuario, en que le puedo ayudar");
                                    break;
                                }
                            case string s when s.Contains("edad") && (s.Contains("dame") || s.Contains("tienes")):
                                {
                                    DateTime fechaNacimiento = new DateTime(2022, 4, 18);
                                    speecher.Speak($"Mi edad es de {DateTime.Now.Day - fechaNacimiento.Day} dias con {DateTime.Now.Month - fechaNacimiento.Month} meses y {DateTime.Now.Year - fechaNacimiento.Year} años");
                                    break;
                                }
                            case string s when s.Contains("correos") && (s.Contains("dame") || s.Contains("muéstrame")):
                                {
                                    Ver_correos correos = new Ver_correos(speecher);
                                    correos.ShowDialog();
                                    break;
                                }
                            case string s when s.Contains("hora") && (s.Contains("dame") || s.Contains("muéstrame") || s.Contains("tienes") || s.Contains("que")):
                                {
                                    DateTime tiempo = DateTime.Now;
                                    speecher.Speak($"La hora es: {tiempo.Hour} hrs con {tiempo.Minute} minutos");
                                    break;
                                }
                            case string s when s.StartsWith("quiero que me busques") || s.StartsWith("búscame"):
                                {
                                    string busqueda = s.StartsWith("quiero que me busques") ?
                                        s.Substring("quiero que me busques".Length) :
                                        s.Substring("búscame".Length);
                                    busqueda = busqueda.Remove(busqueda.Length - 1, 1);
                                    speecher.Speak($"Buscando {busqueda}");
                                    Process.Start(new ProcessStartInfo
                                    {
                                        FileName = $"https://www.google.com/search?q={busqueda}",
                                        UseShellExecute = true,
                                    });
                                    break;
                                }
                            case string s when s.StartsWith("abre la aplicación de") || s.StartsWith("abre la") || s.StartsWith("abre"):
                                {
                                    string applicacionAbuscar =
                                        s.StartsWith("abre la aplicación de") ?
                                        s.Substring("abre la aplicación de ".Length).Replace(".", ""):
                                        s.StartsWith("abre la")?
                                        s.Substring("abre la ".Length).Replace(".", ""):
                                        s.Substring("abre ".Length).Replace(".", "");
                                    if (aplicaciones.ContainsKey(applicacionAbuscar))
                                    {
                                        try
                                        {
                                            Process.Start(new ProcessStartInfo
                                            {
                                                FileName = aplicaciones[applicacionAbuscar],
                                                UseShellExecute = true,
                                            });
                                        }
                                        catch (System.ComponentModel.Win32Exception ex)
                                        {
                                            speecher.Speak($"La aplicacion que usted busca no esta instalada");
                                        }
                                    }
                                    else
                                        speecher.Speak($"No se cual es {applicacionAbuscar}, repita el comando porfavor");
                                    break;
                                }
                            case string s when s.Contains("cerrar aplicación") || s.Contains("adiós") || s.Contains("cerrar"):
                                {
                                    DateTime horaActual = DateTime.Now;
                                    speecher.Speak($"Gracias por usar el Asistente virtual, que tenga una linda {(horaActual.TimeOfDay >= new TimeSpan(6, 0, 0) &&
                                        horaActual.TimeOfDay < new TimeSpan(12, 0, 0) ?
                                        "mañana" :
                                        horaActual.TimeOfDay >= new TimeSpan(12, 0, 0)
                                        && horaActual.TimeOfDay < new TimeSpan(18, 0, 0) ?
                                        "tarde" :
                                        "noche"
                                        )}");
                                    Invoke((MethodInvoker)(() => this.Close()));
                                    break;
                                }
                            default:
                                speecher.Speak("No conozco esa instruccion");
                                break;
                        }
                        break;
                    }
                case ResultReason.NoMatch:
                    throw new Exception("No se entendio lo que quiso decir");
                case ResultReason.Canceled:
                    {
                        CancellationDetails cancellation = CancellationDetails.FromResult(e.Result);
                        throw new Exception(cancellation.ErrorDetails);
                    }
            }
            await speecherListener.speechRecognizer.StartContinuousRecognitionAsync();
        }

        private async void Form_asistente_FormClosing(object sender, FormClosingEventArgs e)
        {
            await speecherListener.speechRecognizer.StopContinuousRecognitionAsync();
        }
    }
}
