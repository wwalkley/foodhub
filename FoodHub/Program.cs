var builder = WebApplication.CreateBuilder( args );

builder.Services.AddControllers( );
builder.Services.AddEndpointsApiExplorer( );
builder.Services.AddSwaggerGen( );

var app = builder.Build( );

app.UseSwagger( );
app.UseSwaggerUI( );
app.UseHttpsRedirection( );
app.UseRouting( );
app.UseAuthorization( );
app.UseEndpoints( endpoints => { endpoints.MapControllers( ); } );
app.MapControllers( );
app.Run( );