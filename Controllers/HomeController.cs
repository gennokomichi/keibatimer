using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using keiba_Timer.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;

namespace keiba_Timer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // スクレイピング処理を行うアクション
      
// ...existing code...
public async Task<IActionResult> KeibaTimer()
{
    var scrapedData = await GetScrapedData();
    return View("KeibaTimer", scrapedData);  // ビュー名を指定し、スクレイピング結果をモデルとして渡す
}
// ...existing code...

private async Task<List<string>> GetScrapedData()
{
    var options = new ChromeOptions();
    options.AddArgument("--headless");  // ヘッドレスモード
    options.AddArgument("--no-sandbox");
    options.AddArgument("--disable-dev-shm-usage");

    using (var driver = new ChromeDriver(options))
    {
        driver.Navigate().GoToUrl("https://race.netkeiba.com/top/");

        // ページが完全に読み込まれるのを待つ
        await Task.Delay(5000);  // 必要に応じて待機時間を調整

        // スクレイピングする要素を取得
        var elements = driver.FindElements(By.CssSelector("span.RaceList_Itemtime"));
        var scrapedTexts = elements.Select(element => element.Text).ToList();

        return scrapedTexts;
    }
}
// ...existing code...

        // Indexアクション
        public IActionResult Index()
        {
            return View();
        }

        // Privacyアクション
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
