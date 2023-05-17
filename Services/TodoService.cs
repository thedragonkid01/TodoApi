using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TodoListApi.DTOs;
using TodoListApi.DTOs.Requests;
using TodoListApi.DTOs.Responses;
using TodoListApi.Enums;
using TodoListApi.Models;
using TodoListApi.Repositories.IRepositories;
using TodoListApi.Services.IServices;

namespace TodoListApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepo;
        private readonly ITagRepository _tagRepo;
        private readonly ITodoTagRepository _todoTagRepo;
        private readonly IMapper _mapper;

        public TodoService(ITodoRepository todoRepo, ITagRepository tagRepo, ITodoTagRepository todoTagRepo, IMapper mapper)
        {
            _todoRepo = todoRepo;
            _tagRepo = tagRepo;
            _todoTagRepo = todoTagRepo;
            _mapper = mapper;
        }

        public async Task<GetAllTodoResponse> GetAll()
        {
            GetAllTodoResponse response = new GetAllTodoResponse(true);
            GetAllTodoData data = new GetAllTodoData();

            var lstETodo = await _todoRepo.GetAll();
            data.TodoList = _mapper.Map<List<TodoDTO>>(lstETodo);
            response.Data = data;
            return response;
        }

        public async Task<SearchTodoResponse> Search(SearchTodoRequest request)
        {
            SearchTodoResponse response = new SearchTodoResponse(true);
            SearchTodoData data = new SearchTodoData();

            var todos = _todoRepo.GetAllAsQueryable();

            if (request.Status.HasValue)
            {
                todos = todos.Where(o => o.Status == request.Status);
            }

            if (request.DateFrom.HasValue)
            {
                todos = todos.Where(o => o.CreatedDate.Date >= request.DateFrom.Value.Date);
            }

            if (request.DateTo.HasValue)
            {
                todos = todos.Where(o => o.CreatedDate.Date <= request.DateTo.Value.Date);
            }

            data.TodoList = await todos
                .Select(o => new TodoDTO
            {
                Id = o.Id,
                Title = o.Title,
                Status = o.Status,
                StatusName = ((EStatus)o.Status).ToString(),
                DueDate = o.DueDate,
                Comments = _mapper.Map<List<CommentDTO>>(o.Comments),
                Tags = _mapper.Map<List<TagDTO>>(o.TodoTags.Select(o => o.Tag).ToList())
            }).ToListAsync();
            response.Data = data;
            return response;
        }

        public async Task<CreateTodoResponse> Add(CreateTodoRequest request)
        {
            CreateTodoResponse response = new CreateTodoResponse(true);

            Todo eTodo = _mapper.Map<Todo>(request.Todo);
            _todoRepo.Add(eTodo);

            if (await _todoRepo.Save() <= 0)
            {
                response.Success = false;
                response.Message = "Unable to add todo";
            }

            if (request.Todo.Tags != null && request.Todo.Tags.Count > 0)
            {
                foreach (var tag in request.Todo.Tags)
                {
                    var tagExisted = await _tagRepo.GetByName(tag.Name);
                    long tagId;
                    if (tagExisted == null)
                    {
                        var eTag = _mapper.Map<Tag>(tag);
                        _tagRepo.Add(eTag);
                        await _tagRepo.Save();
                        tagId = eTag.Id;


                    }
                    else
                    {
                        tagId = tagExisted.Id;
                    }

                    var eTodoTag = new TodoTag()
                    {
                        TodoId = eTodo.Id,
                        TagId = tagId
                    };
                    _todoTagRepo.Add(eTodoTag);
                    await _todoTagRepo.Save();
                }
            }

            CreateTodoData data = new CreateTodoData();
            data.Todo = _mapper.Map<TodoDTO>(eTodo);
            data.Todo.StatusName = ((EStatus)eTodo.Status).ToString();
            data.Todo.Tags = _mapper.Map<List<TagDTO>>(eTodo.TodoTags.Select(o => o.Tag).ToList());
            response.Data = data;

            return response;
        }

        public async Task<ResponseBase> UpdateStatus(UpdateTodoStatusRequest request)
        {
            ResponseBase response = new ResponseBase();

            var eTodo = await _todoRepo.GetById(request.Id);
            if (eTodo == null)
            {
                response.Success = false;
                response.Message = "Unable to find Todo";
                return response;
            }

            eTodo.Status = request.Status;
            eTodo.UpdatedDate = DateTime.Now;
            _todoRepo.Update(eTodo);
            if (await _todoRepo.Save() <= 0)
            {
                response.Success = false;
                response.Message = "Unable to update Todo";
                return response;
            }

            response.Success = true;
            response.Message = "Update successful";
            return response;
        }

        public async Task<UpdateTodoResponse> Update(UpdateTodoRequest request)
        {
            UpdateTodoResponse response = new UpdateTodoResponse();

            var eTodo = await _todoRepo.GetById(request.Todo.Id);
            if (eTodo == null)
            {
                response.Success = false;
                response.Message = "Unable to find Todo";
                return response;
            }
            eTodo.Title = request.Todo.Title;
            eTodo.Status = request.Todo.Status;
            eTodo.DueDate = request.Todo.DueDate;
            eTodo.UpdatedDate = DateTime.Now;
            _todoRepo.Update(eTodo);
            if (await _todoRepo.Save() <= 0)
            {
                response.Success = false;
                response.Message = "Unable to update Todo";
                return response;
            }

            await _todoTagRepo.DeleteByTodoId(eTodo.Id);
            if (request.Todo.Tags != null && request.Todo.Tags.Count > 0)
            {
                foreach (var tag in request.Todo.Tags)
                {
                    var tagExisted = await _tagRepo.GetByName(tag.Name);
                    if (tagExisted == null)
                    {
                        var eTag = _mapper.Map<Tag>(tag);
                        _tagRepo.Add(eTag);
                        await _tagRepo.Save();

                        var eTodoTag = new TodoTag()
                        {
                            TodoId = eTodo.Id,
                            TagId = eTag.Id
                        };
                        _todoTagRepo.Add(eTodoTag);
                        await _todoTagRepo.Save();
                    }
                    else
                    {
                        if (!await _todoTagRepo.Existed(eTodo.Id, tagExisted.Id))
                        {
                            var eTodoTag = new TodoTag()
                            {
                                TodoId = eTodo.Id,
                                TagId = tagExisted.Id
                            };
                            _todoTagRepo.Add(eTodoTag);
                            await _todoTagRepo.Save();
                        }
                    }
                }
            }

            response.Success = true;
            response.Message = "Update successful";
            return response;
        }

        public async Task<ResponseBase> Delete(long id)
        {
            ResponseBase response = new ResponseBase();

            var eTodo = await _todoRepo.GetById(id);
            if (eTodo == null)
            {
                response.Success = false;
                response.Message = "Unable to find Todo";
                return response;
            }

            _todoRepo.Delete(eTodo);
            if (await _todoRepo.Save() <= 0)
            {
                response.Success = false;
                response.Message = "Unable to delete Todo";
                return response;
            }

            response.Success = true;
            response.Message = "Delete successful";
            return response;
        }
    }
}
