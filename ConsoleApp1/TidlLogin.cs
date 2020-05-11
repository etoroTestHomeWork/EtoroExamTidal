using core.DTO;
using core.logger;
using log4net;
using OpenTidl;
using OpenTidl.Transport;
using System;
using System.Net;
using System.Threading.Tasks;

namespace core
{
    public class TidlLogin
    {
        private readonly OpenTidlClient openTidlClient;
        private static readonly ILog log = LogManager.GetLogger("LOGGER");


        public NetworkCredential NetworkCredential { get; } = 
            new NetworkCredential(@"etoro.test.home.work@gmail.com", @"Etoro!102");


        public TidlLogin()
        {
            Logger.InitLogger();
            openTidlClient = new OpenTidlClient(ClientConfiguration.Default);
        }


        public async Task<TidlDTO> LoginWithDefaultCredentials()
        {
            return await Login(NetworkCredential);
        }




        public async Task<TidlDTO> Login(NetworkCredential credentials)
        {
            var tidlDTO = new TidlDTO();
            try
            {
                Logger.Log.Info($"Login details:{credentials.UserName}/{credentials.Password}");
                tidlDTO.OpenTidlSession = await openTidlClient.LoginWithUsername(
                    credentials.UserName, credentials.Password);
                return tidlDTO;
            }
            catch (OpenTidlException ex)
            {
                tidlDTO.TidlErrorMessage = ex.OpenTidlError?.UserMessage;
                tidlDTO.TidlErrorCode = ex.OpenTidlError?.Status;
                Logger.Log.Error($"Exception ({tidlDTO.TidlErrorCode}) occured - {tidlDTO.TidlErrorMessage}");
                return tidlDTO;
            }
        }

        public OpenTidlClient GetOpenTidlClient()
        {
            return openTidlClient;
        }
    }

}
