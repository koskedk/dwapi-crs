using System;
using System.Net;
using Dwapi.Crs.SharedKernel.Model;
using Dwapi.Crs.SharedKernel.Utils;

namespace Dwapi.Crs.Service.Application.Domain
{
    public class TransmissionLog : Entity<Guid>
    {
        public Registry Registry { get; private set; }
        public Response Response { get; private set; }
        public string ResponseInfo { get; private set; }
        public Guid RegistryManifestId { get; private set; }

        private TransmissionLog()
        {
        }

        private TransmissionLog(Registry registry, Response response, string responseInfo, Guid registryManifestId)
        {
            Registry = registry;
            Response = response;
            ResponseInfo = responseInfo;
            RegistryManifestId = registryManifestId;
        }

        public static TransmissionLog New(Registry registry, HttpStatusCode response, string responseInfo,
            Guid registryManifestId)
        {
            var resp = Generate(response);

            if (resp == Response.Sent)
                responseInfo = string.Empty;

            var trl = new TransmissionLog(
                registry, resp, responseInfo, registryManifestId
            );
            return trl;
        }

        private static Response Generate(HttpStatusCode response)
        {
            if (response == HttpStatusCode.BadRequest)
                return Response.Failed;

            return Response.Sent;
        }
    }
}