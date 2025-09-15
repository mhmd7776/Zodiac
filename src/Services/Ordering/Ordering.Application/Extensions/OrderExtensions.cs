using Ordering.Application.DTOs;
using Ordering.Domain.Models;

namespace Ordering.Application.Extensions
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderDto> ToOrderDto(this IEnumerable<Order> orders)
        {
            var dtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                var dto = new OrderDto
                {
                    Id = order.Id.Value,
                    CustomerId = order.CustomerId.Value,
                    Status = order.Status,

                    Address = new AddressDto
                    {
                        FirstName = order.Address.FirstName,
                        LastName = order.Address.LastName,
                        Email = order.Address.Email,
                        AddressLine = order.Address.AddressLine,
                        City = order.Address.City,
                        State = order.Address.State,
                        Country = order.Address.Country,
                        ZipCode = order.Address.ZipCode
                    },

                    Payment = new PaymentDto
                    {
                        CardName = order.Payment.CardName,
                        CardNumber = order.Payment.CardNumber,
                        ExpirationDate = order.Payment.ExpirationDate,
                        CVV2 = order.Payment.CVV2,
                        PaymentMethod = order.Payment.PaymentMethod
                    },

                    OrderItems = [.. order.OrderItems.Select(x => new OrderItemDto
                    {
                        Id = x.Id.Value,
                        OrderId = x.OrderId.Value,
                        ProductId = x.ProductId.Value,
                        Price = x.Price,
                        Quantity = x.Quantity
                    })]
                };

                dtos.Add(dto);
            }

            return dtos;
        }

        public static OrderDto ToOrderDto(this Order order)
        {
            var dto = new OrderDto
            {
                Id = order.Id.Value,
                CustomerId = order.CustomerId.Value,
                Status = order.Status,

                Address = new AddressDto
                {
                    FirstName = order.Address.FirstName,
                    LastName = order.Address.LastName,
                    Email = order.Address.Email,
                    AddressLine = order.Address.AddressLine,
                    City = order.Address.City,
                    State = order.Address.State,
                    Country = order.Address.Country,
                    ZipCode = order.Address.ZipCode
                },

                Payment = new PaymentDto
                {
                    CardName = order.Payment.CardName,
                    CardNumber = order.Payment.CardNumber,
                    ExpirationDate = order.Payment.ExpirationDate,
                    CVV2 = order.Payment.CVV2,
                    PaymentMethod = order.Payment.PaymentMethod
                },

                OrderItems = [.. order.OrderItems.Select(x => new OrderItemDto
                    {
                        Id = x.Id.Value,
                        OrderId = x.OrderId.Value,
                        ProductId = x.ProductId.Value,
                        Price = x.Price,
                        Quantity = x.Quantity
                    })]
            };

            return dto;
        }
    }
}
