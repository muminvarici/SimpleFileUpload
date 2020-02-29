using Nest;
using SimpleFileUpload.DataAccess;
using SimpleFileUpload.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleFileUpload.DataAccess
{
	public class UserElasticSearch : ElasticSearchBase<UserModel>
	{
		public UserElasticSearch() : base("user_index")
		{
		}

		public IEnumerable<UserModel> FilterByName(int pageNumber, int pageSize, string filter)
		{
			int startIndex = (pageNumber - 1) * pageSize;

			var response = ElasticClient.Search<UserModel>(s => s
							.Index(IndexName)
							.Query(q =>
								q.Bool(b =>
									b.Should(sd => sd.MatchPhrasePrefix(c => c
										  .Field(p => p.Name)
										  .Analyzer("standard")
										  .Boost(1.1)
										  .Query(filter)
										  .Slop(2)
								)))
							)
							.From(startIndex)
							.Size(pageSize));

			return response.Documents;

		}

		public IEnumerable<UserModel> FilterByPhone(int pageNumber, int pageSize, string filter)
		{
			int startIndex = (pageNumber - 1) * pageSize;

			var response = ElasticClient.Search<UserModel>(s => s
							.Index(IndexName)
							.Query(q =>
								q.Bool(b =>
									b.Should(sd => sd.MatchPhrasePrefix(c => c
										  .Field(p => p.MobileNo)
										  .Analyzer("standard")
										  .Boost(1.1)
										  .Query(filter)
										  .Slop(2)
								)))
							)
							.From(startIndex)
							.Size(pageSize));

			return response.Documents;

		}
	}
}
