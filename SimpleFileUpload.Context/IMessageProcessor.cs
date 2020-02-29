using Newtonsoft.Json.Linq;
using SimpleFileUpload.Message;
using System;

namespace SimpleFileUpload.Context
{
	public interface IMessageProcessor
	{
		BaseRequest GetRequestMessage(JObject source);

		BaseResponse Process(BaseRequest request, string accessToken = null);

		string Prefix { get; }
	}
}
