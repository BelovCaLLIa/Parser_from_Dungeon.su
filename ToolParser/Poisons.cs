using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Parser
{
	//таблица яды
	class Poisons
	{
		private IWebDriver browser;
		//Запуск браузера
		public void startBrowser()
		{
			browser = new OpenQA.Selenium.Chrome.ChromeDriver();
			browser.Manage().Window.Maximize();
			browser.Navigate().GoToUrl("https://dungeon.su/articles/mechanics/109-gerbalizm_i_alhimiya/");
		}

		//поиск элемента
		public List<string> findAnItem()
		{
			//IWebElement id = browser.FindElement(By.Id("ingredienty_yadov"));

			//Создание бд
			//DataSet ingredienty_yadov = new DataSet(id.Text);
			//Console.WriteLine(ingredienty_yadov.DataSetName);

			IReadOnlyList<IWebElement> td = browser.FindElements(By.TagName("td"));

			List<string> str = new List<string>();
			for (int i = 0; i < td.Count; i++)
			{
				if (127 < i && i < 199)
				{
					str.Add(Convert.ToString(td[i].Text));
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
			using (FileStream fs = new FileStream("Poisons.json", FileMode.OpenOrCreate))
			{
				//записывает как UTF-8, но отображаются как ASCII символы
				await JsonSerializer.SerializeAsync<List<string>>(fs, str, options);
				Console.WriteLine("Data has been saved to file Poisons.json");
			}
		}
	}
}




/*
			//список из элементов с тегом 
			//IReadOnlyList<IWebElement> td = browser.FindElements(By.TagName("span"));
			//пауза
			browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
			IWebElement book_id = browser.FindElement(By.Id("eOQ1TRQA"));
			//Двойной клик
			Actions actions = new Actions(browser);
			actions.DoubleClick(book_id).Perform();
			//ждём открытия книжки
			browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

			IWebElement table = browser.FindElement(By.CssSelector("div[class='textLayer'] [style='width: 815px; height: 1154px;']"));

 */
