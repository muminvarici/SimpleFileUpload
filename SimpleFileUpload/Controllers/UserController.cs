using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SimpleFileUpload.AppLayer;
using SimpleFileUpload.Message;
using System;
using System.Threading.Tasks;

namespace SimpleFileUpload.Controllers
{
	public class UserController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult List()
		{
			return View();
		}
		public IActionResult Upload()
		{
			return View();
		}

		[HttpPost]
		[Produces("application/json")]
		public BaseResponse FilterByName([FromBody]UserFilterByNameRequest request)
		{
			return UserAppLayer.FilterByName(request);
		}

		[HttpPost]
		[Produces("application/json")]
		public BaseResponse FilterByPhone([FromBody]UserFilterByPhoneRequest request)
		{
			return UserAppLayer.FilterByPhone(request);
		}

	}
}
