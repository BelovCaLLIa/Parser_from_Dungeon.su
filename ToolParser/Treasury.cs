using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using System.IO;
using System.Text.Encodings.Web;

namespace Parser
{
    //таблица Сокровищница
    class Treasury
    {
		private IWebDriver browser;
		//Запуск браузера
		public void startBrowser()
		{
			browser = new OpenQA.Selenium.Chrome.ChromeDriver();
			browser.Manage().Window.Maximize();
			browser.Navigate().GoToUrl("https://dungeon.su/articles/inventory/74-sokrovischnitsa/");
		}

		//поиск элемента
		public List<string> findAnItem()
		{
			IReadOnlyList<IWebElement> strong = browser.FindElements(By.TagName("strong"));

			IReadOnlyList<IWebElement> td = browser.FindElements(By.TagName("td"));

			List<string> str = new List<string>();
			for (int i = 0; i < td.Count; i++)
			{
				if (4 < i)
				{
					str.Add(Convert.ToString(td[i].Text));
				}
				if (i == 0)
				{
					str.Add(Convert.ToString(strong[3].Text));
				}
				if (i == 36)
				{
					str.Add(Convert.ToString(strong[4].Text));
				}
				if (i == 90)
				{
					str.Add(Convert.ToString(strong[5].Text));
				}
				if (i == 180)
				{
					str.Add(Convert.ToString(strong[6].Text));
				}
				if (i == 282)
				{
					str.Add(Convert.ToString(strong[7].Text));
				}
				if (i == 360)
				{
					str.Add(Convert.ToString(strong[8].Text));
				}
				if (i == 378)
				{
					str.Add(Convert.ToString(strong[9].Text));
				}
				if (i == 450)
				{
					str.Add(Convert.ToString(strong[10].Text));
				}
				if (i == 508)
				{
					str.Add(Convert.ToString(strong[11].Text));
				}
				if (i == 542)
				{
					str.Add(Convert.ToString(strong[12].Text));
				}
				if (i == 558)
				{
					str.Add(Convert.ToString(strong[14].Text));
				}
				if (i == 680)
				{
					str.Add(Convert.ToString(strong[15].Text));
				}
				if (i == 858)
				{
					str.Add(Convert.ToString(strong[16].Text));
				}
				if (i == 998)
				{
					str.Add(Convert.ToString(strong[17].Text));
				}
			}

			browser.Close();

			return str;
		}

		public async Task writingToFileAsync(List<string> str)
		{

			JsonSerializerOptions options = new JsonSerializerOptions
			{
				Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
				WriteIndented = true
			};


			// сохранение данных
			using (FileStream fs = new FileStream("Treasury.json", FileMode.OpenOrCreate))
			{
				//записывает как UTF-8, но отображаются как ASCII символы
				await JsonSerializer.SerializeAsync<List<string>>(fs, str, options);
				Console.WriteLine("Data has been saved to file Treasury.json");
			}
		}
	}
}
