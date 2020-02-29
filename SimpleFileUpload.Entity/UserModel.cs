using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleFileUpload.Entity
{
	[Table("User")]
	public class UserModel: IElasticIndexModel
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string MobileNo { get; set; }
		public DateTime BirthDate { get; set; }
		public string LastLocation { get; set; }
	}
}
