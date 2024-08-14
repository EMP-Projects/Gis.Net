# Raster Data Model
[PostGIS supporta un altro tipo di tipo di dati spaziali chiamato raster]((https://postgis.net/workshops/de/postgis-intro/rasters.html)).
I dati raster, proprio come i dati geometrici, utilizzano coordinate cartesiane e un sistema di riferimento spaziale.
Tuttavia, invece dei dati vettoriali, i dati raster sono rappresentati come una matrice n-dimensionale composta da pixel e bande.
Le bande definiscono il numero di matrici e ogni pixel memorizza un valore corrispondente a ciascuna banda.
Quindi un raster a 3 bande come un'immagine RGB, avrebbe 3 valori per ciascun pixel corrispondente
alle fasce Rosso-Verde-Blu.
In poche parole, un raster è una matrice, fissata su un sistema di coordinate, e possono interagire con le geometrie.
Il formato raster viene comunemente utilizzato per archiviare dati di elevazione, dati di temperatura, dati satellitari e temi
dati che rappresentano cose come la contaminazione ambientale, la densità di popolazione e il rischio ambientale.
È possibile utilizzare i raster per memorizzare qualsiasi dato numerico che abbia una posizione di coordinate significativa.
L'unica limitazione è che per tutti i dati in una banda specifica i tipi di dati numerici devono essere gli stessi.

## GisRasterModel
GisRasterModel è il modello per rappresentare i dati raster con PostGis utilizzando Ef Core, per poter [indicizzare](https://postgis.net/docs/manual-3.5/it/using_raster_dataman.html) i dati raster
Il provider **Npgsql EF Core** ti consente di specificare il metodo dell'indice da utilizzare chiamando HasMethod() 
sull'indice nel metodo OnModelCreating del tuo contesto. 
Il tipo di indice da usare è [*gist*](https://postgis.net/docs/manual-3.5/it/using_raster_dataman.html):

```C#

[Table("gis_raster", Schema = "gis")]
public class MyRasterModel : GisRasterModel 
{
    /// add your properties
}

protected override void OnModelCreating(ModelBuilder modelBuilder)
    => modelBuilder.Entity<MyRasterModel>()
    .HasIndex(b => b.Raster)
    .HasMethod("gist");
```