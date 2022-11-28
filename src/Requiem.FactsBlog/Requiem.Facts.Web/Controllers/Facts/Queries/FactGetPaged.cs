
using Requiem.Facts.Web.Data;
using Requiem.Facts.Web.ViewModels;

namespace Requiem.Facts.Web.Controllers.Facts.Queries;

class FactGetPagedRequest: OperationResultRequestBase<IPagedList<FactViewModel>>
{
    public FactGetPagedRequest(int pageIndex, string? tag, string? search)
    {
        PageIndex = pageIndex;
        Tag = tag;
        Search = search;
    }

    public int PageIndex { get; }
    public int PageSize { get; } = 20;

    public string? Tag { get;}

    public string? Search { get; }
}

class FactGetPagedRequestHandler : OperationResultRequestHandlerBase<FactGetPagedRequest, IPagedList<FactViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FactGetPagedRequestHandler(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;   
        _unitOfWork = unitOfWork;
    }

    public override async Task<OperationResult<IPagedList<FactViewModel>>> Handle(FactGetPagedRequest request, CancellationToken cancellationToken)
    {
        var operation = OperationResult.CreateResult<IPagedList<FactViewModel>>();

        var items = await _unitOfWork.GetRepository<Fact>()
            .GetPagedListAsync(
                include: i => i.Include(x => x.Tags),
                orderBy: o => o.OrderByDescending(x => x.CreatedAt),
                pageIndex: request.PageIndex, 
                pageSize: request.PageSize,
                cancellationToken: cancellationToken);

        var mapped = _mapper.Map<IPagedList<FactViewModel>>(items);

        operation.Result = mapped;
        operation.AddSuccess("Success");

        return operation;
    }
}