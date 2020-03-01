using Microsoft.AspNetCore.Mvc;
using SimpleFileUpload.AppLayer;
using SimpleFileUpload.Message;

namespace SimpleFileUpload.Controllers
{
	public class UserController : Controller
	{
		private readonly UserAppLayer UserAppLayer;
		public UserController(UserAppLayer userAppLayer)
		{
			UserAppLayer = userAppLayer;
		}

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
