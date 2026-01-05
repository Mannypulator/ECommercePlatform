using System;
using Microsoft.AspNetCore.Routing;

namespace BuildingBlocks.Slices;

public interface ISlice
{
    void AddEndpoint(IEndpointRouteBuilder endpointRouteBuilder);
}
