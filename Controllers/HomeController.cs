using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WEBSAIGONGLISTEN.Models;

namespace WEBSAIGONGLISTEN.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;// day la hung
        public HomeController(IProductRepository productRepository, ApplicationDbContext context) // cho nay la hung
        {
            _context = context; // cho nay la hung
            _productRepository = productRepository;
        }

        // Hien thi danh sach san pham
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Tour()
        {
            var tours = _context.Products.ToList(); // Lấy danh sách tour từ cơ sở dữ liệu
            return View(tours);
        }

        /*public async Task<IActionResult> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                // Trả về một View trống hoặc thông báo không tìm thấy.
                return View("Search", new List<Product>());
            }

            var result = await _context.Products
                .Where(p => p.Name.Contains(query) || (p.Description != null && p.Description.Contains(query)))
                .ToListAsync();

            // Trả về kết quả tìm kiếm qua View "Tour" hoặc một View khác mà bạn muốn hiển thị kết quả
            return View("Search", result);
        }*/

        public async Task<IActionResult> Search(string term) // Sử dụng tham số "term" thay vì "query"
    {
        if (string.IsNullOrWhiteSpace(term))
        {
            return Json(new List<string>()); // Trả về một danh sách rỗng nếu truy vấn trống
        }

        var result = await _context.Products
            .Where(p => p.Name.Contains(term) || (p.Description != null && p.Description.Contains(term)))
            .Select(p => p.Name) // Chọn chỉ các tên tour để trả về danh sách gợi ý
            .Distinct() // Loại bỏ các giá trị trùng lặp
            .ToListAsync();

        return Json(result); // Trả về danh sách gợi ý dưới dạng JSON
    }
    }
}
