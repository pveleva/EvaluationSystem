namespace EvaluationSystem.Application.Models.Modules
{
    public class CreateModelDto
    {
        public int idForm { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Position { get; set; } = 1;
    }
}
