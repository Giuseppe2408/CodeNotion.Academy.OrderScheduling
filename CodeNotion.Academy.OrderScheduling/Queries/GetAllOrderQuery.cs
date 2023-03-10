﻿using CodeNotion.Academy.OrderScheduling.Models;
using MediatR;

namespace CodeNotion.Academy.OrderScheduling.Queries;

public class GetAllOrderQuery : IRequest<List<Order>>
{
    
}