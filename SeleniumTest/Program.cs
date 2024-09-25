

using OpenQA.Selenium;
//不同瀏覽器有不同的 nuget library 與 namespace
using OpenQA.Selenium.Chrome;

string fullUrl = "https://tw.stock.yahoo.com/class-quote?sectorId=26&exchange=TAI"; 
 
var options = new ChromeOptions(); 
//使用 headless 模式
options.AddArguments(new List<string>() { 
    "headless", 
    "disable-gpu" 
}); 
 
//使用 ChromeDriver
var browser = new ChromeDriver(options); 
browser.Navigate().GoToUrl(fullUrl);
//使用 css selector 找到所有股票資訊
var nodes =//browser.FindElements(By.CssSelector("li.List(n)")); 
    browser.FindElements(By.CssSelector("li[class='List(n)']"));

var stocks = nodes.Select(a => new Stock
{
    //使用 XPath 找到股票名稱、代號、價格、漲跌、漲跌幅、開盤、昨收、最高、最低、成交量
    Name = a.FindElement(By.XPath("./div/div[1]/div[2]/div/div[1]")).Text.Trim(),
    Symbol =  a.FindElement(By.XPath("./div/div[1]/div[2]/div/div[2]")).Text.Trim(),
    Price =  a.FindElement(By.XPath("./div/div[2]")).Text.Trim(),
    Change = a.FindElement(By.XPath("./div/div[3]")).Text.Trim(),
    PriceChange = a.FindElement(By.XPath("./div/div[4]")).Text.Trim(),
    Open = a.FindElement(By.XPath("./div/div[5]")).Text.Trim(),
    LastClose = a.FindElement(By.XPath("./div/div[6]")).Text.Trim(),
    High = a.FindElement(By.XPath("./div/div[7]")).Text.Trim(),
    Low = a.FindElement(By.XPath("./div/div[8]")).Text.Trim(),
    Turnover = a.FindElement(By.XPath("./div/div[9]")).Text.Trim(),
    UpDown = UpDownCheck(a.FindElement(By.XPath("./div/div[3]/span")).GetAttribute("class"))
});

foreach(var stock in stocks) 
{
    Console.WriteLine($"股票名稱: {stock.Name.PadRight(12)}\t 股票代號: {stock.Symbol}\t 股價: {stock.Price.PadRight(5)}\t 漲跌: {stock.UpDown} {stock.PriceChange.PadRight(8)}\t 漲跌幅: {stock.UpDown} {stock.Change.PadRight(8)}\t 開盤: {stock.Open}\t 昨收: {stock.LastClose}\t 最高: {stock.High}\t 最低: {stock.Low}\t 成交量(張): {stock.Turnover}");
} 

string UpDownCheck(string value)
{
    if (value.Contains("up"))
    {
        return "上漲";
    }
    if (value.Contains("down"))
    {
        return "下跌";
    }
    return string.Empty;
}

class Stock
{
    public string Name { get; set; }
    public string Symbol { get; set; }
    public string Price { get; set; }
    public string Change { get; set; }
    public string PriceChange { get; set; }
    public string Open { get; set; }
    public string LastClose { get; set; }
    public string High { get; set; }
    public string Low { get; set; }
    public string Turnover { get; set; }
    public string UpDown { get; set; }
} 