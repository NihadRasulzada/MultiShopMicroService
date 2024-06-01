﻿using MultiShop.Order.Application.Features.CQRS.Queries.OrderDetailQueries;
using MultiShop.Order.Application.Features.CQRS.Results.OrderDetaliResults;
using MultiShop.Order.Application.Interfaces;
using MultiShop.Order.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Order.Application.Features.CQRS.Handlers.OrderDetailHandlers
{
    public class GetOrderDetailByIdQueryHandler
    {
        private readonly IRepository<OrderDetail> _repository;
        public GetOrderDetailByIdQueryHandler(IRepository<OrderDetail> repository)
        {
            _repository = repository;
        }

        public async Task<GetOrderDetailByIdQueryResult> Handle(GetOrderDetailByIdQuery request)
        {
            OrderDetail? orderDetail = await _repository.GetByIdAsync(request.Id);
            return new GetOrderDetailByIdQueryResult
            {
                OrderDetailId = orderDetail.OrderDetailId,
                ProductAmount = orderDetail.ProductAmount,
                ProductId = orderDetail.ProductId,                    
                ProductName = orderDetail.ProductName,
                OrderingId = orderDetail.OrderingId,
                ProductPrice = orderDetail.ProductPrice,
                ProductTotalPrice = orderDetail.ProductTotalPrice,                
            };
        }
    }
}
