namespace TaskManager.Api;

public static class ApiEndpoints
{
    public static class Project
    {
        private const string Base = "projects";
        private const string BaseTasks = $"{Base}/{{projectId}}/tasks";
        public const string Create = $"{Base}";
        public const string CreateTask = $"{BaseTasks}";
        public const string Delete = $"{Base}/{{id}}";
        public const string DeleteTask = $"{BaseTasks}{{taskId}}";
        public const string GetAll = $"{Base}";
        public const string GetById = $"{Base}/{{id}}";
        public const string GetTask = $"{BaseTasks}/{{taskId}}";
    }

    public static class Auth
    {
        private const string Base = "member";
        public const string CreateTrustedMember = $"{Base}/register";
        public const string GetTrustedMember = $"{Base}/login";
        public const string UpdateTrustedMember = $"{Base}/update";
    }
}