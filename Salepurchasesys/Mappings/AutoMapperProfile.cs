using AutoMapper;
using SalePurchasesys.DTOs;
using SalePurchasesys.Models;

namespace SalePurchasesys.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Product
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();

            // ProductCategory
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();

            // ProductSubCategory
            CreateMap<ProductSubCategory, ProductSubCategoryDto>().ReverseMap();

            // Purchase
            CreateMap<Purchase, PurchaseDto>().ReverseMap();
            CreateMap<CreatePurchaseDto, Purchase>();
            CreateMap<UpdatePurchaseDto, Purchase>();

            // PurchaseDetail
            CreateMap<PurchaseDetail, PurchaseDetailDto>().ReverseMap();
            CreateMap<CreatePurchaseDetailDto, PurchaseDetail>();
            CreateMap<PurchaseDetailUpdateDto, PurchaseDetail>();

            // Sale
            CreateMap<Sale, SaleDto>().ReverseMap();
            CreateMap<SaleCreateDto, Sale>();
            CreateMap<SaleUpdateDto, Sale>();

            // SaleDetail
            CreateMap<SaleDetail, SaleDetailDto>().ReverseMap();
            CreateMap<SaleDetailCreateDto, SaleDetail>();
            CreateMap<SaleDetailUpdateDto, SaleDetail>();

            // User
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
