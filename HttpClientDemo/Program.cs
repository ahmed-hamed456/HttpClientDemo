
using HttpClientDemo.Handlers;
using HttpClientDemo.HttpClientServices;
using Microsoft.Net.Http.Headers;

namespace HttpClientDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            //For definetion more client
            builder.Services.AddHttpClient("Posts", x =>
            {
                x.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts");
                x.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<ValidateHeaderHandler>();
            

            //builder.Services.AddHttpClient("Users", x =>
            //{
            //    x.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/users");
            //    //x.DefaultRequestHeaders.Add(HeaderNames.Authorization, "application/json");
            //});



            builder.Services.AddScoped(typeof(CRUDHttpService));
            builder.Services.AddTransient(typeof(ValidateHeaderHandler));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
}
