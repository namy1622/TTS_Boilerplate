using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTS_boilerplate.Core;
using TTS_boilerplate.Models;
using TTS_boilerplate.Statistics.Dto;

namespace TTS_boilerplate.Statistics
{
    public class StatisticsAppService : IStatisticsAppService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<TTS_boilerplate.Models.Category> _categoryRepository;
        private readonly IRepository<CartItem> _cartItemRepository;
        private readonly IRepository<Cart> _cartRepository;

        public StatisticsAppService(
            IRepository<Product> productRepository,
            IRepository<TTS_boilerplate.Models.Category> categoryRepository,
            IRepository<Cart> cartRepository,
            IRepository<CartItem> cartItemRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<ListResultDto<chartCategoryDto>> GetDataForChartCategory()
        {
            var chartCategory = await _productRepository.GetAll().AsNoTracking()
                            .Include(p => p.BelongToCategory)
                            .GroupBy(p => p.BelongToCategory.NameCategory)
                            .Select(g => new chartCategoryDto
                            {
                                NameCategory = g.Key,
                                QuantityProduct = g.Count()
                            }).ToListAsync();
            return new ListResultDto<chartCategoryDto>
            {
                Items = chartCategory
            };
        }

        [HttpGet]
        public async Task<List<chartCategoryDto>> TotalAmountOfCategory()
        {          
            var allProductWithCategory = await _cartItemRepository.GetAll().AsNoTracking()
                .Where(ci => ci.Status == CartItem.OrderStatus.Confirmed)
                .Include(ci => ci.Product)
                    .ThenInclude(p => p.BelongToCategory)
                    .GroupBy(a => a.Product.BelongToCategory.NameCategory)
                    
                    .Select(g => new chartCategoryDto
                    {
                        NameCategory = g.Key,
                        QuantityProduct = g.Sum(ci => ci.Quantity),
                        TotalAmount = g.Sum(ci => (int)(ci.Product.Price ?? 0) * ci.Quantity)
                    })
                    .ToListAsync();

            return allProductWithCategory;
        }

        [HttpGet]
        public async Task<List<RevenueProduct>> RevenueByProduct()
        {
            var revenueProduct = await _cartItemRepository.GetAll().AsNoTracking()
                .Where(ci => ci.Status == CartItem.OrderStatus.Confirmed)
                .Include(ci => ci.Product)
                .OrderByDescending(ci => ci.Quantity) 
                .Select(ci => new RevenueProduct
                {
                    NameProduct = ci.Product.NameProduct,
                    QuantityProduct = ci.Quantity,
                    TotalAmount = (int)(ci.Product.Price ?? 0) * ci.Quantity
                }).ToListAsync();

            return revenueProduct;
        }
        [HttpGet]
        public async Task<RevenueProduct> TotalRevenueProduct()
        {
            var revenueList = await RevenueByProduct();

            return new RevenueProduct
            {
                TotalAmount = revenueList.Sum(x => x.TotalAmount),
                QuantityProduct = revenueList.Count()
            };
        }

        [HttpGet]
        public async Task<List<RevenueProduct>> TopRevenueProduct()
        {
            var revenueList = await RevenueByProduct();

            var topRevenue = revenueList.Take(5).ToList();

            return topRevenue;
        }
    }
}
