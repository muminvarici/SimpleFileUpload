using Nest;
using SimpleFileUpload.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFileUpload.DataAccess
{
	public static class ElasticSearchHelper
	{
		private static readonly ConnectionSettings connectionSettings = new ConnectionSettings(new Uri("http://localhost:9200/"));
		private static readonly ElasticClient elasticClient = new ElasticClient(connectionSettings);

		public static void Index<T>(T item, string indexName) where T : class, IElasticIndexModel
		{
			var status = elasticClient.Index<T>(item, indexDescriptor =>
			{
				indexDescriptor.Index(indexName);
				indexDescriptor.Id(item.Id);
				return indexDescriptor;
			});
		}

		public static void IndexBulk<T>(IEnumerable<T> items, string indexName) where T : class, IElasticIndexModel
		{
			var descriptor = new BulkDescriptor();
			foreach (var item in items)
			{
				descriptor.Index<T>(op => op.Document(item));
			}
			var result = elasticClient.Bulk(descriptor);
		}

		public static T Find<T>(string index, int id) where T : class, IElasticIndexModel
		{
			var response = elasticClient.Get<T>(id, finder =>
			{
				finder.Index(index);
				return finder;
			});
			return response.Source;
		}
	}
}
