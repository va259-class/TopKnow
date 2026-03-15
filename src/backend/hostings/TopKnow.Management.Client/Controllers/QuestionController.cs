using Microsoft.AspNetCore.Mvc;
using TopKnow.Management.Client.Helpers;
using TopKnow.Management.Client.HttpClients;
using TopKnow.Management.Client.ViewModels;

namespace TopKnow.Management.Client.Controllers;

public class QuestionController : Controller
{
    private readonly ManagementApi _managementApi;

    public QuestionController(ManagementApi managementApi)
    {
        _managementApi = managementApi;
    }

    public async Task<IActionResult> Index(int page = 1, int size = 15)
    {
        var result = await _managementApi.SendGetRequest<List<QuestionListItemViewModel>>($"api/questions?Page={page}&Size={size}");
        if (result == null)
        {
            return View(new Result<QuestionListViewModelWrappper> { IsSuccess = false, Error = new Error("0", "Bağlantı hatası") });
        }
        var wrapper = new QuestionListViewModelWrappper
        {
            Questions = result.Value,
            Page = page
        };

		return View(new Result<QuestionListViewModelWrappper> { Value = wrapper, IsSuccess = true });
    }

    public async Task<IActionResult> New()
    {
        var typesResult = await _managementApi.SendGetRequest<List<QuestionTypeItemViewModel>>("api/questions/types");
        ViewBag.Types = typesResult?.IsSuccess == true ? typesResult.Value : new List<QuestionTypeItemViewModel>();
        return View(new CreateQuestionViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateQuestionViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Title))
        {
            ModelState.AddModelError(nameof(model.Title), "Soru metni gereklidir.");
            await LoadTypesForNew();
            return View(nameof(New), model);
        }

        var input = new CreateQuestionRequestDto
        {
            Title = model.Title,
            TypeId = model.TypeId,
            Answers = model.Answers.Where(a => !string.IsNullOrWhiteSpace(a.Title))
                                   .Select(a => new AnswerItemDto { Title = a.Title, IsCorrect = a.IsCorrect })
                                   .ToList() ?? new List<AnswerItemDto>()
        };

        var result = await _managementApi.SendPostRequest<Guid, CreateQuestionRequestDto>("api/questions", input);
        if (result?.IsSuccess == true)
        {
            return RedirectToAction(nameof(Index));
        }
        ModelState.AddModelError("", result?.Error?.Message ?? "Kayıt oluşturulamadı.");
        await LoadTypesForNew();
        return View(nameof(New), model);
    }

    public async Task<IActionResult> Edit(Guid id)
    {
        var result = await _managementApi.SendGetRequest<QuestionDetailViewModel>($"api/questions/{id}");
        if (result?.IsSuccess != true || result?.Value == null)
            return RedirectToAction(nameof(Index));
        var typesResult = await _managementApi.SendGetRequest<List<QuestionTypeItemViewModel>>("api/questions/types");
        ViewBag.Types = typesResult?.IsSuccess == true ? typesResult.Value : new List<QuestionTypeItemViewModel>();
        return View(new EditQuestionViewModel
        {
            Id = result.Value.Id,
            Title = result.Value.Title,
            TypeId = result.Value.TypeId,
            Answers = result.Value.Answers?.Count > 0 ? result.Value.Answers : new List<AnswerItemViewModel> { new(), new() }
        });
    }

    [HttpPost]
    public async Task<IActionResult> Update(EditQuestionViewModel model)
    {
        if (string.IsNullOrWhiteSpace(model.Title))
        {
            ModelState.AddModelError(nameof(model.Title), "Soru metni gereklidir.");
            await LoadTypesForEdit(model.Id);
            return View(nameof(Edit), model);
        }

        var input = new UpdateQuestionRequestDto
        {
            Id = model.Id,
            Title = model.Title,
            TypeId = model.TypeId,
            Answers = model.Answers?
                .Where(a => !string.IsNullOrWhiteSpace(a.Title))
                .Select(a => new AnswerItemDto { Title = a.Title, IsCorrect = a.IsCorrect })
                .ToList() ?? new List<AnswerItemDto>()
        };

        var result = await _managementApi.SendPutRequest<bool, UpdateQuestionRequestDto>($"api/questions/{model.Id}", input);
        if (result?.IsSuccess == true)
            return RedirectToAction(nameof(Index));
        ModelState.AddModelError("", result?.Error?.Message ?? "Güncelleme yapılamadı.");
        await LoadTypesForEdit(model.Id);
        return View(nameof(Edit), model);
    }

    private async Task LoadTypesForNew()
    {
        var typesResult = await _managementApi.SendGetRequest<List<QuestionTypeItemViewModel>>("api/questions/types");
        ViewBag.Types = typesResult?.IsSuccess == true ? typesResult.Value : new List<QuestionTypeItemViewModel>();
    }

    private async Task LoadTypesForEdit(Guid id)
    {
        var typesResult = await _managementApi.SendGetRequest<List<QuestionTypeItemViewModel>>("api/questions/types");
        ViewBag.Types = typesResult?.IsSuccess == true ? typesResult.Value : new List<QuestionTypeItemViewModel>();
    }
}
