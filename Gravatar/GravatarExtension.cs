using System.Text;
using System.Security.Cryptography;
using System;

namespace Gravatar
{
    public static class GravatarExtension
    {
        public static string ToGravatar(this string email, int size = 80, GravatarDefaults defaultImage = GravatarDefaults.None, string customDefaultImage = null)
        {
            if (string.IsNullOrWhiteSpace(email))
                return string.Empty;

            using var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(email);
            var hashBytes = md5.ComputeHash(inputBytes);

            var sb = new StringBuilder();
            foreach (var t in hashBytes)
                sb.Append(t.ToString("X2"));

            string d = defaultImage switch
            {
                GravatarDefaults.NotFound => "&d=404",
                GravatarDefaults.MP or
                GravatarDefaults.IdentIcon or
                GravatarDefaults.MonsterId or
                GravatarDefaults.Wavatar or
                GravatarDefaults.RoboHash or
                GravatarDefaults.Blank => $"&d={Enum.GetName(typeof(GravatarDefaults), defaultImage).ToLower()}",
                GravatarDefaults.Custom when customDefaultImage != null => $"&d={Uri.EscapeUriString(customDefaultImage)}",
                _ => null
            };

            return $"https://www.gravatar.com/avatar/{sb.ToString().ToLower()}?s={size}{d}";
        }
    }
}
