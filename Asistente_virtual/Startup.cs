using Microsoft.Extensions.Configuration;
namespace Asistente_virtual
{
    public static class Startup
    {
       static IConfigurationBuilder Builder { get; set; }

        public static string CognitiveServiceKey { get; set; }

        public  static string CognitiveServiceRegion { get; set; }

        public static void Initialize()
        {
            Builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            IConfigurationRoot configuration = Builder.Build();

            CognitiveServiceKey = configuration["CognitiveServiceKey"];
            CognitiveServiceRegion = configuration["CognitiveServiceRegion"];
        }
        public static Dictionary<string,string> BindDictionary()
        {
            IConfigurationRoot configuration = Builder.Build();
           return configuration.GetSection("TerminosDePrograma").Get<Dictionary<string,string>>();
        }
    }
}
