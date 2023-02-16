using Microsoft.EntityFrameworkCore;
using Vms;
using Vms.JsonModel;
using System.Xml.Serialization;
using System;
using System.IO;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<M3sDb>(opt => opt.UseInMemoryDatabase("UserList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");


app.MapGet("/",()=> "Root url");

app.MapGet("/connect",async (DataContext context)=>{
    Console.WriteLine(context.Database.GetConnectionString());
    return context.Users.Count();
    //return context.Database.GetDbConnection().State;
});

app.MapGet("/devices",async (DataContext db) => await db.Devices.ToListAsync());

app.MapGet("/devcfg",async(DataContext db) => await db.Devcfgs.ToListAsync());

app.MapGet("/map", async(DataContext db) => await db.Maps.ToListAsync());

app.MapGet("/nvr", async(DataContext db)=> await db.Nvrs.ToListAsync());

app.MapGet("/evtd",async(DataContext db)=> await db.Evtds.Take(10).ToListAsync());

app.MapGet("/evtlog", async(DataContext db) => await db.EvtLogs.Take(10).ToListAsync());

app.MapGet("/users", async(DataContext db) => await db.Users.ToListAsync());

app.MapGet("/devicesMap/{mapid}",async(DataContext db, int mapid)=>{
    var devcfg = await db.Devcfgs.Where(x=> x.DEVTYP == 3 && x.DEVID == mapid).FirstAsync();
    XmlSerializer serializer = new XmlSerializer(typeof(JDeviceMap));
    string strCam = "";
    using(TextReader reader = new System.IO.StringReader(devcfg.DEVCFG))
    {
        Console.WriteLine(reader);
        var result = (JDeviceMap)serializer.Deserialize(reader);
        foreach(var item in result.devices)
        {
            //Console.WriteLine(item.Trim());
            strCam += item.Trim() + ",";
        }

    }
    if(strCam.Length < 1)
    {
        return Enumerable.Empty<object>();
    }
    var camInMap = strCam.Substring(0, strCam.Length - 1).Split(",");
    var devLst = await db.Devices.Where(x=> camInMap.Contains(x.DEVSYM)).ToListAsync();
    return devLst.Select( x=>
        new{
            id = x.ID,
            devsym = x.DEVSYM?.Trim(),
            devnm = x.DEVNM?.Trim()
        }
    ).ToList();
    //return devLst;
});


app.MapGet("/GetCamRecordings/{cam}/{recdate}", async (DataContext db, string cam, DateTime recdate) =>{
    Console.WriteLine(recdate);
    //var timeArr = new{}
    var recordingData = new {
        camsym = cam,
        recdate = recdate,
        timings = new[]{
            new {tmb = "00:10:50", tme = "01:50:00"},
            new {tmb = "03:00:50", tme = "03:30:00"},
            new {tmb = "10:00:50", tme = "10:50:00"},
            new {tmb = "10:50:00", tme = "11:10:00"},
            new {tmb = "11:25:00", tme = "12:30:00"},
            new {tmb = "17:10:00", tme = "19:30:00"},
        }
    };

    var findCamInSrvr = await db.Devcfgs.FirstOrDefaultAsync(x=> x.DEVTYP==3 && x.DEVCFG!=null && x.DEVCFG.Contains(cam));
    return recordingData;
});

app.Run();

record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}