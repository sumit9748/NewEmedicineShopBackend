using Emedicine.BAL.CartBased;
using Emedicine.BAL.MedcineBased;
using Emedicine.BAL.OrderBased;
using Emedicine.BAL.Services;
using Emedicine.BAL.UserBased;
using Emedicine.DAL.Data;
using Emedicine.DAL.DataAccess;
using Emedicine.DAL.DataAccess.Interface;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        //adding db context
        builder.Services.AddDbContext<MedicineDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("EMedcs"), b => b.MigrationsAssembly("Emedicine"));
        });

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", b =>
            {
                b.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });



        builder.Services.AddHttpClient();
        //add injection from dal to program
        builder.Services.AddScoped<IDataAccess, DataAccess>();

        //add injection from business layer to dal
        builder.Services.AddScoped<IUserManager, UserManager>();

        //medicine scope
        builder.Services.AddScoped<IMedicineMain, MedicineMain>();
        //cart scope
        builder.Services.AddScoped<ICartMain, CartMain>();
        //order scope
        builder.Services.AddScoped<IOrderMain, OrderMain>();
        //add jwt authetication service 
        
        builder.Services.AddScoped<IService, Service>();

       

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidAudience = builder.Configuration["AppSettings:Audience"],
                   ValidIssuer = builder.Configuration["AppSettings:Issuer"],
                   
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("gjgejgjge7836673-6e7hfghdjjdndeghej7494bdb@"))
               };
           });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors();

        app.UseHttpsRedirection();
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

}