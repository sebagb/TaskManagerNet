namespace TaskManager.Api;

public static class ApiEndpoints
{
    public static class Project
    {
        private const string Base = "projects";
        public const string Create = $"{Base}";
        public const string Get = $"{Base}/{{id}}";
    }

    public static class Auth
    {
        private const string Base = "auth";
        public const string TrustedMemberToken = $"{Base}/trustedToken";
        public const string CreateTrustedMember = $"{Base}/register";
        public const string GetTrustedMember = $"{Base}/login";
    }
}