
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using quiz.Data;
using quiz.Handler;

namespace quiz;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services
            .AddAuthentication()
            .AddScheme<AuthenticationSchemeOptions, QuizAuthHandler>("Authentication", null);

        builder.Services.AddDbContext<QuizDBContext>(options => options.UseSqlite(builder.Configuration["quizDBConnection"]));
        builder.Services.AddScoped<IQuizRepo, QuizRepo>();

        builder.Services.AddControllers();
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy => policy.RequireClaim("admin"));
            options.AddPolicy("AuthOnly", policy => {
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c =>
                    (c.Type == "normalUser")));
            });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
