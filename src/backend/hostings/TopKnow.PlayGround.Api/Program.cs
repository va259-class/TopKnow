namespace TopKnow.PlayGround.Api;

using TopKnow.Data.Extensions;
using TopKnow.Modules.PlayGround.Extensions;
using TopKnow.PlayGround.Api.Extensions;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddJwt(builder.Configuration);
        builder.Services.AddData(builder.Configuration);
        builder.Services.AddPlayGround();
        builder.Services.AddSignalR();

        builder.Services.AddCors(options =>
        {
            if (builder.Environment.IsDevelopment())
            {
                options.AddPolicy(builder.Environment.EnvironmentName,
                policy =>
                {
                    policy.WithOrigins("http://localhost:5173", "http://192.168.254.24:5173")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            }
        });

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors(builder.Environment.EnvironmentName);
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapHub<GameHub>("/gh").RequireAuthorization();
        app.Run();
    }
}
