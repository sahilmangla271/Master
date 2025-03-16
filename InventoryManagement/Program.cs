using Inventory.Management.Infrastructure.Data;
using Inventory.Management.Infrastructure.Data.EF;
using Inventory.Management.Infrastructure.Implementation;
using Inventory.Management.Infrastructure.Interface;
using Inventory.Management.Infrastructure.Services.Booking;
using Inventory.Management.Infrastructure.Services.Member;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<InventoryDBContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IUploadService, UploadService>();



var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

app.Run();
