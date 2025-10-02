namespace TaskManager.Api;

public static class ApiEndpoints
{
    public static class Project
    {
        private const string Base = "projects";
        public const string Create = $"{Base}";
        public const string Delete = $"{Base}/{{id}}";
        public const string GetAll = $"{Base}";
        public const string GetById = $"{Base}/{{id}}";
    }

    public static class Auth
    {
        private const string Base = "member";
        public const string CreateTrustedMember = $"{Base}/register";
        public const string GetTrustedMember = $"{Base}/login";
        public const string UpdateTrustedMember = $"{Base}/update";
    }
}