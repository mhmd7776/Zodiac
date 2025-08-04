using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService(DiscountDbContext discountDbContext) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<GetAllDiscountsResponse> GetAllDiscounts(Empty request, ServerCallContext context)
        {
            var coupons = await discountDbContext.Coupons.OrderByDescending(x => x.Id).ToListAsync();
            if (coupons.Count == 0)
                return new GetAllDiscountsResponse();

            var couponModels = coupons.Adapt<List<CouponModel>>();

            var response = new GetAllDiscountsResponse();
            response.Coupons.AddRange(couponModels);
            return response;
        }

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            // get or initial non-coupon
            var coupon = await discountDbContext.Coupons.FirstOrDefaultAsync(x => x.ProductId == new Guid(request.ProductId));
            coupon ??= new Coupon { ProductId = Guid.NewGuid(), Amount = 0 };

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<Int32Value> CreateDiscount(CouponModel request, ServerCallContext context)
        {
            var coupon = request.Adapt<Coupon>() ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "request must be a valid CouponModel"));

            discountDbContext.Coupons.Add(coupon);
            await discountDbContext.SaveChangesAsync();

            return new Int32Value { Value = coupon.Id };
        }

        public override async Task<BoolValue> UpdateDiscount(CouponModel request, ServerCallContext context)
        {
            var coupon = await discountDbContext.Coupons.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (coupon == null)
                return new BoolValue { Value = false };

            request.Adapt(coupon);
            discountDbContext.Coupons.Update(coupon);
            await discountDbContext.SaveChangesAsync();

            return new BoolValue { Value = true };
        }

        public override async Task<Empty> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountDbContext.Coupons.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (coupon == null) 
                return new Empty();

            discountDbContext.Coupons.Remove(coupon);
            await discountDbContext.SaveChangesAsync();
            return new Empty();
        }
    }
}
