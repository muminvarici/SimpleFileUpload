using Elasticsearch.Net;
using Nest;
using SimpleFileUpload.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SimpleFileUpload.DataAccess
{
	public abstract class BaseIndex<T> : IIndexRequest<T> where T : class, IElasticIndexModel
	{
		public T Document { get; set; }

		public IndexName Index { get; set; }

		public IndexRequestParameters RequestParameters { get; set; }

		public string ContentType { get; set; }

		public HttpMethod HttpMethod { get; set; }

		public RouteValues RouteValues { get; set; }

		Id IIndexRequest<T>.Id => this.Document.Id;

		IRequestParameters IRequest.RequestParameters => this.RequestParameters;

		public string GetUrl(IConnectionSettingsValues settings)
		{
			throw new NotImplementedException();
		}

		public void WriteJson(IElasticsearchSerializer sourceSerializer, Stream stream, SerializationFormatting serializationFormatting)
		{
			sourceSerializer.Serialize(Document, stream, serializationFormatting);
		}
	}
}
