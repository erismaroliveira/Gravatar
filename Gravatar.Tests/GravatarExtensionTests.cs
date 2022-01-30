using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Gravatar.Tests
{
    [TestClass]
    public class GravatarExtensionTests
    {
        [TestCategory("Gravatar Tests")]
        [TestMethod("Should validate Gravatar extension")]
        [DataRow(null, 200, null, null, false)]
        [DataRow("", 200, null, null, false)]
        [DataRow(" ", 200, null, null, false)]
        [DataRow("a@a", 200, null, null, false)]
        [DataRow("a@erismar.com", 200, null, null, false)]
        [DataRow("a@erismar.com", 200, GravatarDefaults.NotFound, null, false)]
        [DataRow("erismarpro@hotmail.com", null, null, null, true)]
        [DataRow("erismarpro@hotmail.com", 200, null, null, true)]
        [DataRow("erismarpro@hotmail.com", 200, GravatarDefaults.IdentIcon, null, true)]
        [DataRow("erismarpro@hotmail.com", 200, GravatarDefaults.Custom, "https://via.placeholder.com/80.png/ddd/999", true)]
        public void ShouldValidateGravatar(string email, int? size, GravatarDefaults? defaultImage, string customDefaultImage, bool status)
        {
            var imageSize = size.HasValue ? size.Value.ToString() : "80";
            var result = $"https://www.gravatar.com/avatar/d49e76c58821b3c341167d32d9d48747?s={imageSize}";
            result = defaultImage switch
            {
                GravatarDefaults.NotFound => $"{result}&d=404",
                GravatarDefaults.IdentIcon => $"{result}&d=identicon",
                GravatarDefaults.Custom => $"{result}&d={Uri.EscapeUriString(customDefaultImage)}",
                _ => result
            };

            Assert.AreEqual((email.ToGravatar(size ?? 80, defaultImage ?? GravatarDefaults.None, customDefaultImage) == result), status);
        }
    }
}
