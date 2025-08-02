using Discount.Grpc.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        public override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            return base.GetDiscount(request, context);
        }

        public override Task<Int32Value> CreateDiscount(CouponModel request, ServerCallContext context)
        {
            return base.CreateDiscount(request, context);
        }

        public override Task<BoolValue> UpdateDiscount(CouponModel request, ServerCallContext context)
        {
            return base.UpdateDiscount(request, context);
        }

        public override Task<Empty> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            return base.DeleteDiscount(request, context);
        }
    }
}
