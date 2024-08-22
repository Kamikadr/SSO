using AutoMapper;
using Grpc.Core;
using MediatR;
using SSO.ApiMessages;

namespace SSO.ApiServices;

public class AdminApiService(IMediator mediator, IMapper mapper): Admin.AdminBase
{
    public override Task<AddServiceResponse> AddService(AddServiceRequest request, ServerCallContext context)
    {
        return base.AddService(request, context);
    }

    public override Task<IsAdminResponse> IsAdmin(IsAdminRequest request, ServerCallContext context)
    {
        return base.IsAdmin(request, context);
    }
}