using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

// Создаём строителя веб-приложения с настройками по-умолчанию
var builder = WebApplication.CreateBuilder(args);
// Добавляем поддержку MVC (model-view-controller)
builder.Services.AddMvc(option => option.EnableEndpointRouting = true);
// Настраиваем Swagger для генерации документации API
builder.Services.AddSwaggerGen(option => {
    // Создаём новую версию документации Swagger
    option.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Инструментарий"
    });
    // Получаем путь к XML файлу с комментариями из кода
    // AppContext.BaseDirectory - директория, где запущено приложение
    string PathFile = Path.Combine(AppContext.BaseDirectory, "praktika33.xml");
    // Добавление xml-комментарии в swagger для лучшей документации
    option.IncludeXmlComments(PathFile);
});
// Строим приложение после настройки всех сервисов
var app = builder.Build();
// Вклюаем middleware для генерации Swagger json
app.UseSwagger();
// Включаем маршрутизациб запросов
app.UseRouting();
// Настраиваем конечные точки (endpoint) приложения
app.UseEndpoints(endpoints => {
    // Подключаем все контроллеры как конечные точки
    endpoints.MapControllers();
});
// Настраиваем пользовательский интерфейс Swagger UI
app.UseSwaggerUI(c => {
    // Указываем путь к json спецификации Swagger
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Инсрументарий");
});
// Запускаем приложение и начинаем обрабатывать запросы
app.Run();