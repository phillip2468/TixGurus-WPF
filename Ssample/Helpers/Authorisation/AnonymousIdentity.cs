namespace Ssample.Helpers.Authorisation
{
    /// <summary>
    /// A class which represents a user without authorization
    /// </summary>
    public class AnonymousIdentity : CustomIdentity
    {
        public AnonymousIdentity()
            : base(string.Empty, string.Empty, new string[] { })
        { }
    }
}
