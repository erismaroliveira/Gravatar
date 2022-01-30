using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gravatar.Tests
{
    [TestClass]
    public class GravatarExtensionTests
    {
        [TestCategory("Gravatar Tests")]
        [TestMethod("Should validate Gravatar extension")]
        [DataRow(null, 200, false)]
        [DataRow("", 200, false)]
        [DataRow(" ", 200, false)]
        [DataRow("a@a", 200, false)]
        [DataRow("a@erismar.com", 200, false)]
        [DataRow("erismarpro@hotmail.com", null, true)]
        [DataRow("erismarpro@hotmail.com", 200, true)]
        public void ShouldValidateGravatar(string email, int? size, bool status)
        {
            var imageSize = size.HasValue ? size.Value.ToString() : "80";
            var result = $"https://www.gravatar.com/avatar/d49e76c58821b3c341167d32d9d48747?s={imageSize}";
            Assert.AreEqual((email.ToGravatar(size ?? 80) == result), status);
        }
    }
}
