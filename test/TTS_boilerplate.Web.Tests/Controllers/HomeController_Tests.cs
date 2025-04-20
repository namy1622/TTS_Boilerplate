using System.Threading.Tasks;
using TTS_boilerplate.Models.TokenAuth;
using TTS_boilerplate.Web.Controllers;
using Shouldly;
using Xunit;

namespace TTS_boilerplate.Web.Tests.Controllers
{
    public class HomeController_Tests: TTS_boilerplateWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}