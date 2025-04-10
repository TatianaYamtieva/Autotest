using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace practice_for_selenium;

public class Tests
{
    public IWebDriver driver;
    public WebDriverWait wait; 

    [SetUp] //метод подготовки к тесту
    public void Setup()
    {
        driver = new ChromeDriver(); //запуск браузера Chrome
        driver.Manage().Window.Maximize(); 
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); //установка неявного ожидания
        wait = new WebDriverWait (driver, TimeSpan.FromSeconds(5)); //явное ожидание

    }


     [TearDown] //что происходит после завершения каждого теста
    public void TearDown()
    {
        driver.Quit(); //закрытие браузера
        driver.Dispose();

    }

    private void Authorize()
    {
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/"); //переход на страницу сайта

        var login = driver.FindElement(By.Id("Username")); //поиск поля логина
        login.SendKeys("yamtieva99@mail.ru"); //ввод логина

        var password = driver.FindElement (By.Id("Password")); //поиск поля пароля
        password.SendKeys("Tatiana1999!"); //ввод пароля

        var enter = driver.FindElement(By.Name("button")); //поиск кнопки Войти
        enter.Click(); //нажатие кнопки Войти

        wait.Until(ExpectedConditions.UrlToBe ("https://staff-testing.testkontur.ru/news")); //явное ожидание что страница новостей прогрузилась
    }


    [Test]
    public void Authorization() //авторизация на стаффе 
    {
        Authorize (); //вызов метода авторизации
        Assert.That(driver.Title, Does.Contain("Новости"), "После авторизации при переходе на страницу Новостей мы не смогли найти заголовок Новости"); //проверка, что открыта нужная страница
    }


  [Test]
    public void Сommunity () // переход в "сообщества" 
    { 
        Authorize ();

        var community_button = driver.FindElement(By.CssSelector("[data-tid='Community']")); //ищем "сообщества"
        community_button.Click(); // кликаем на Сообщества

        wait.Until(ExpectedConditions.UrlToBe ("https://staff-testing.testkontur.ru/communities")); //ожидание открытия страницы Сообщества

        var title_page_element = driver.FindElement(By.CssSelector("[data-tid='Title']")); //поиск заголовка
        Assert.That(title_page_element.Text, Does.Contain("Сообщества"), "при переходе на вкладку Сообщества мы не смогли найти заголовок Сообщества");//проверка наличия заголовка и текст сообщения об ошибке
        
    }


    [Test]
    public void Сommunity_create() //создание сообщества без краткого описания 
    {
        Authorize ();

        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities");   //переход на страницу "сообщества"
        wait.Until(ExpectedConditions.UrlToBe ("https://staff-testing.testkontur.ru/communities")); // ожидаем прогрузки страницы

        var create =  driver.FindElement(By.ClassName("sc-juXuNZ")); // ищем "+создать"
        create.Click();//нажимаем на кнопку "+Создать"

        var name_community = driver.FindElement(By.CssSelector("[data-tid='Name']")); //ищем поле ввода названия сообщества
        name_community.SendKeys("Заполняю название сообщества");//заполняем название сообщества

        var create_community = driver.FindElement(By.CssSelector("[data-tid='CreateButton']")); //ищем кнопку Создать
        create_community.Click(); //нажимаем кнопку Создать

        wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("span[data-tid='Title']")));//ожидание, что заголовок станет кликабельным

        var title_page_element = driver.FindElement(By.CssSelector("span[data-tid='Title']"));//поиск заголовка Управление сообществом
        Assert.That(title_page_element.Text, Does.Contain("Управление сообществом «"), "После создания сообщества мы не смогли найти заголовок станицы Управление сообществом");//проверка наличия заголовка и текст сообщения об ошибке
    }


    [Test]
    public void settings_profile() //переход в настройки 
    {
        Authorize ();
        var menu=driver.FindElement(By.CssSelector("[data-tid='DropdownButton']")); //поиск кнопки для раскрытия списка
        menu.Click();//раскрыть список

        var settings=driver.FindElement(By.CssSelector("[data-tid='Settings']")); //поиск "настройки"
        settings.Click();//нажать на "настройки"

        wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("sc-bdnxRM"))); //ожидание, что заголовок модального окна станет кликабельным

        var title_page_element = driver.FindElement(By.ClassName("sc-bdnxRM")); //поиск заголовка модального окна "Настройки"
        Assert.That(title_page_element.Text, Does.Contain("Настройки"), "при переходе в модальное окно Настроек мы не смогли найти заголовок Настройки");//проверка наличия заголовка и текст сообщения об ошибке
    }


     [Test]
    public void edit_profile() //переход в редактирование профиля 
    {
        Authorize ();
        var menu=driver.FindElement(By.CssSelector("[data-tid='DropdownButton']")); //поиск кнопки для раскрытия списка
        menu.Click(); //раскрыть список

        var edit=driver.FindElement(By.CssSelector("[data-tid='ProfileEdit']")); // поиск кнопки "редактировать"
        edit.Click(); //нажать на кнопку "редактировать"

        wait.Until(ExpectedConditions.UrlToBe ("https://staff-testing.testkontur.ru/profile/settings/edit")); // ожидание что страница редактировния профиля загрузилась
       
        var title_page_element = driver.FindElement(By.CssSelector("[data-tid='Title']")); //поиск заголовка страницы "Редактирование профиля"
        Assert.That(title_page_element.Text, Does.Contain("Редактирование профиля"), "при переходе на вкладку редактирования профиля мы не смогли найти заголовок Редактирование профиля");//проверка наличия заголовка и текст сообщения об ошибке
    }
}


