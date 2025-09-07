namespace TaskManager.Api;

public static class ApiEndpoints
{
    public static class Project
    {
        private const string Base = "projects";
        public const string CreateProject = $"{Base}";
        public const string Get = $"{Base}/{{id:int}}";
    }
}