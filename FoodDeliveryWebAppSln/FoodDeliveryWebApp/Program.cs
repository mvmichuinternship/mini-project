using FoodDeliveryWebApp.context;
using FoodDeliveryWebApp.interfaces;
using FoodDeliveryWebApp.models;
using FoodDeliveryWebApp.repositories;
using FoodDeliveryWebApp.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace FoodDeliveryWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
            });


            //builder.Services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>();


            //Debug.WriteLine(builder.Configuration["TokenKey:JWT"]);
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey:JWT"]))
                    };

                });


            builder.Services.AddDbContext<FoodAppContext>(
               options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"))
               );

            builder.Services.AddScoped<IRepository<int,User>,UserRepository>();
            builder.Services.AddScoped<IRepository<int,Admin>,AdminRepository>();
            builder.Services.AddScoped<IRepository<int,Customer>,CustomerRepository>();
            builder.Services.AddScoped<IRepository<int,Menu>,MenuRepository>();
            builder.Services.AddScoped<IRepository<int,Cart>,CartRepository>();
            builder.Services.AddScoped<IRepository<int,Order>, OrderRepository>();
            builder.Services.AddScoped<IRepository<int,CartDetails>,CartDetailsRepository>();
            builder.Services.AddScoped<IRepository<int, OrderDetails>, OrderDetailsRepository>();
            builder.Services.AddScoped<IRepository<int, Payment>, PaymentRepository>();
            builder.Services.AddScoped<IRepository<int, Feedback>, FeedbackRepository>();
            builder.Services.AddScoped<IRepository<int, FbComment>, FeedbackCommentRepository>();
            builder.Services.AddScoped<CartDetailsRepository>();
            builder.Services.AddScoped<OrderDetailsRepository>();
            builder.Services.AddScoped<FeedbackCommentRepository>();


            builder.Services.AddScoped<IRegisterLoginService,RegisterLoginService>();
            builder.Services.AddScoped<ITokenService,TokenService>();
            builder.Services.AddScoped<IAdminServices,AdminServices>();
            builder.Services.AddScoped<ICartService,CartService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IFeedbackService, FeedbackService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
