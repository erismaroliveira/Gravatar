namespace Gravatar
{
    public static class GravatarExtension
    {
        public static string ToGravatar(this string email, int size = 80, GravatarDefaults defaultImage = GravatarDefaults.None, string customDefaultImage = null)
            => Gravatar.Generate(email, size, defaultImage, customDefaultImage);
    }
}
