using Requiem.Facts.Web.Data;
using Requiem.Facts.Web.ViewModels;

namespace Requiem.Facts.Web.Infrastructure.Mappers.Base;

public class TagMapperConfiguration : MapperConfigurationBase
{
    public TagMapperConfiguration()
    {
        CreateMap<Tag, TagViewModel>();
        CreateMap<Tag, TagUpdateViewModel>();
        CreateMap<TagUpdateViewModel, Tag>()
            .ForMember(x => x.Facts, o => o.Ignore());
    }
}