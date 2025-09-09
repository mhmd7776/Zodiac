using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Data.Extensions
{
    internal class InitialData
    {
        public static IEnumerable<Customer> Customers =>
            [Customer.Create(CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")), "mohammad", "mohammad@gmail.com"),
            Customer.Create(CustomerId.Of(new Guid("17001286-3ec9-4a2b-bbb0-81b9dc705149")), "amir", "amir@gmail.com"),
            Customer.Create(CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")), "mehrzad", "mehrzad@gmail.com")];

        public static IEnumerable<Product> Products =>
            [Product.Create(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), "IPhone X", 500),
            Product.Create(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), "Samsung 10", 400),
            Product.Create(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), "Huawei Plus", 650),
            Product.Create(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), "Xiaomi Mi", 450)];

        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var address1 = Address.Of("mohammad", "mahdavi", "mohammad@gmail.com", "Rey No:30", "Rey", "Tehran", "Iran", "1000000000");
                var address2 = Address.Of("mehrzad", "akhoundi", "mehrzad@gmail.com", "15 khordad No:2", "Teharn", "Teharn", "Iran", "2000000000");

                var payment1 = Payment.Of("saman", "1111222233334444", "12/28", "355", "payment.saman");
                var payment2 = Payment.Of("mellat", "5555666677778888", "06/30", "222", "payment.mellat");

                var order1 = Order.Create(
                                OrderId.Of(new Guid("b4b530cb-97eb-4684-b187-e4dadf7e96b8")),
                                CustomerId.Of(new Guid("58c49479-ec65-4de2-86e7-033c546291aa")),
                                address1,
                                payment1);
                order1.AddItem(ProductId.Of(new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61")), 500, 2);
                order1.AddItem(ProductId.Of(new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914")), 400, 1);

                var order2 = Order.Create(
                                OrderId.Of(new Guid("2b4c2b7d-67e4-4e47-97f8-1fded54a1db3")),
                                CustomerId.Of(new Guid("189dc8dc-990f-48e0-a37b-e6f2b60b9d7d")),
                                address2,
                                payment2);
                order2.AddItem(ProductId.Of(new Guid("4f136e9f-ff8c-4c1f-9a33-d12f689bdab8")), 650, 1);
                order2.AddItem(ProductId.Of(new Guid("6ec1297b-ec0a-4aa1-be25-6726e3b51a27")), 450, 4);

                return [order1, order2];
            }
        }
    }
}
