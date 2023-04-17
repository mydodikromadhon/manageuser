using CRUD.ManagementUser.Application.Common.Extensions;

namespace CRUD.ManagementUser.Bsui.Pages
{
	public partial class Index
    {
		private string _greetings = default!;

		protected override void OnInitialized()
		{

			_greetings = $"Good {DateTimeOffset.Now.ToFriendlyTimeDisplayText()}";
		}
	}
}