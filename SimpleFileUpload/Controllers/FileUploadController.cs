using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleFileUpload.AppLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleFileUpload.Controllers
{
	public class FileUploadController : Controller
	{

		public async Task<IActionResult> UploadAsync()
		{
			var files = ((Microsoft.AspNetCore.Http.FormCollection)Request.Form).Files;

			long size = files.Sum(f => f.Length);
			string filePath = null;
			var startDate = DateTime.Now;
			foreach (var formFile in files)
			{
				if (formFile.Length > 0)
				{
					filePath = Path.GetTempFileName();

					using (var stream = System.IO.File.Create(filePath))
					{
						await formFile.CopyToAsync(stream);
					}
					UserAppLayer.SaveUsers(filePath);
				}
			}

			int waitDuration = 3000 - (int)(DateTime.Now - startDate).TotalMilliseconds;
			waitDuration = Math.Min(3000, waitDuration);
			Thread.Sleep(waitDuration);

			// Process uploaded files
			// Don't rely on or trust the FileName property without validation.
			return Ok(new { count = files.Count, size, filePath });
		}
	}
}
