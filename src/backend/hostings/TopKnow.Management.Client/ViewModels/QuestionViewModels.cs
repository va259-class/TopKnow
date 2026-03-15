namespace TopKnow.Management.Client.ViewModels;

public class QuestionListViewModelWrappper
{
    public List<QuestionListItemViewModel> Questions { get; set; }
    public int Page { get; set; }
}

public class QuestionListItemViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string TypeName { get; set; }
}

public class QuestionTypeItemViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

public class AnswerItemViewModel
{
    public string Title { get; set; }
    public bool IsCorrect { get; set; }
}

public class QuestionDetailViewModel
{
    public QuestionDetailViewModel()
    {
        Answers = new List<AnswerItemViewModel>();
    }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid TypeId { get; set; }
    public List<AnswerItemViewModel> Answers { get; set; }
}

public class CreateQuestionViewModel
{
    public string Title { get; set; }
    public Guid TypeId { get; set; }
    public List<AnswerItemViewModel> Answers { get; set; } = new()
    {
        new AnswerItemViewModel(),
        new AnswerItemViewModel(),
        new AnswerItemViewModel(),
        new AnswerItemViewModel()
    };
}

public class EditQuestionViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid TypeId { get; set; }
    public List<AnswerItemViewModel> Answers { get; set; } = new();
}

// API request DTOs (camelCase serialization)
public class AnswerItemDto
{
    public string Title { get; set; }
    public bool IsCorrect { get; set; }
}

public class CreateQuestionRequestDto
{
    public string Title { get; set; }
    public Guid TypeId { get; set; }
    public List<AnswerItemDto> Answers { get; set; } = new();
}

public class UpdateQuestionRequestDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Guid TypeId { get; set; }
    public List<AnswerItemDto> Answers { get; set; } = new();
}
