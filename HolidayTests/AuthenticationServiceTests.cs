using Lib;
using NSubstitute;
using NUnit.Framework;

namespace HolidayTests
{
    [TestFixture]
    public class AuthenticationServiceTests
    {
        private IToken _token;
        private IProfileDao _profileDao;
        private AuthenticationService _authenticationService;

        [SetUp]
        public void SetUp()
        {
            _token = Substitute.For<IToken>();
            _profileDao = Substitute.For<IProfileDao>();
            _authenticationService = new AuthenticationService(_profileDao,_token);
        }
        [Test()]
        public void is_valid()
        {

            GivenPassword("joey", "91");
            GivenRandom("000000");

            ShouldBeValid("joey", "91000000");
        }

        [Test()]
        public void is_not_valid()
        {

            GivenPassword("joey", "91");
            GivenRandom("000000");

            ShouldBeInvalid("joey", "wrong password");
        }

        private void ShouldBeInvalid(string account, string password)
        {
            Assert.IsFalse(_authenticationService.IsValid(account, password));
        }

        private void ShouldBeValid(string account, string password)
        {
            Assert.IsTrue(_authenticationService.IsValid(account, password));
        }

        private void GivenRandom(string token)
        {
            _token.GetRandom("").ReturnsForAnyArgs(token);
        }

        private void GivenPassword(string account, string password)
        {
            _profileDao.GetPassword(account).Returns(password);
        }
    }
}