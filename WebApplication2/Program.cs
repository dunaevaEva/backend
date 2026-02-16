
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseStaticFiles();

app.MapGet("/", () =>
    {
        return Results.File("static.html", "text/html");
    }
    );
    
app.MapGet("/forum", () =>
    {
        return Results.File("static.html", "text/html");
    }
);

app.MapGet("/doxx", (HttpContext context) =>
    {
        var userIp = context.Connection.RemoteIpAddress?.ToString();
        return Results.Content($"Ваш IP: {userIp}", "text/html; charset=utf-8");
    }
);
app.Run();