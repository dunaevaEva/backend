
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
var messages = new List<Message>();

app.MapGet("/forum", () => messages);

app.MapPost("/forum", (Message message) =>
{
    message.Id = messages.Count == 0 ? 1 : messages.Max(m => m.Id) + 1;
    messages.Add(message);
    return Results.Created($"/forum/{message.Id}", message);
});

app.MapPut("/forum", (Message message) =>
{
    var replace = messages.FirstOrDefault(m => m.Id == message.Id);
    if (replace != null)
        return Results.NotFound();
    if (message.Author != null)
    replace.Author = message.Author;
    if (message.Text != null)
    replace.Text = message.Text;
    
    return Results.NoContent();
});

app.MapPatch("/forum", (Message message) =>
{
    var replace = messages.FirstOrDefault(m => m.Id == message.Id);
    if (replace != null)
        return Results.NotFound();
    if (message.Author != null)
    replace.Author = message.Author;
    if (message.Text == null)
    replace.Text = message.Text;
    
    return Results.NoContent();
    
});
app.MapDelete("/forum", () =>
{
    messages.Clear();
    return Results.Ok("Все сообщения удалены");
});

app.Run();

public class Message
{
    public int Id { get; set; }
    public string Author { get; set; }
    public string Text { get; set; }
    
}
