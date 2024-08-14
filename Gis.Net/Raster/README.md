# Salvare un immagine raster nel database

## ST_FromGDALRaster

La funzione ST_FromGDALRaster è una funzione di PostGIS che consente di creare un raster da un file raster supportato da GDAL. GDAL è una libreria open source per l'elaborazione di dati geospaziali. 
Un file raster è un file che contiene una matrice di pixel che rappresentano una regione geografica. Un esempio di file raster è un'immagine TIFF.
Per usare la funzione ST_FromGDALRaster, devi prima caricare il file raster in un tipo di dato bytea, che è un tipo di dato binario di PostgreSQL. 
Puoi usare la funzione pg_read_binary_file per leggere il file raster dal disco e convertirlo in bytea. Poi, puoi passare il bytea alla funzione ST_FromGDALRaster, specificando opzionalmente il SRID (Spatial Reference System Identifier) del raster. 
Il SRID è un codice numerico che identifica il sistema di coordinate usato dal raster. Se non specifichi il SRID, la funzione cercherà di assegnarlo automaticamente dal file raster. 
Se specifichi il SRID, il valore fornito sovrascriverà quello automatico.
Ecco alcuni esempi di come usare la funzione ST_FromGDALRaster con un file raster TIFF:

```sql
-- Carica il file raster TIFF in un bytea
SELECT pg_read_binary_file('raster.tiff') AS raster_data;

-- Crea un raster da un bytea senza specificare il SRID
SELECT ST_FromGDALRaster(raster_data) AS raster;

-- Crea un raster da un bytea specificando il SRID
SELECT ST_FromGDALRaster(raster_data, 32632) AS raster;
```
### Links

- [ST_FromGDALRaster](https://postgis.net/docs/manual-3.5/it/RT_ST_FromGDALRaster.html)
- [Elaborazioni di Immagini con la Libreria OpenCV](https://amslaurea.unibo.it/810/1/varano_pietro_tesi.pdf)

## pg_read_binary_file

La funzione pg_read_binary_file di PostGIS permette di leggere un file binario dal disco e convertirlo in un tipo di dato bytea, che è un tipo di dato binario di PostgreSQL. 
Questa funzione è utile per caricare file raster, come le immagini TIFF, nel database e usarli con altre funzioni di PostGIS, come ST_FromGDALRaster.
Ecco una lista di esempi con pg_read_binary_file di PostGIS per leggere immagine TIFF:

- Per leggere un file TIFF dal percorso relativo alla directory del cluster del database, puoi usare il seguente comando:

```sql
SELECT pg_read_binary_file('raster.tiff') AS raster_data;
```
- Per leggere un file TIFF dal percorso assoluto, devi prima creare un collegamento simbolico (symlink) dalla directory del cluster del database alla directory dove si trova il file TIFF. 
- Ad esempio, se il file TIFF si trova in `/home/user/raster.tiff`, puoi creare un symlink con il comando:
```bash
ln -s /home/user/raster.tiff /var/lib/postgresql/13/main/raster.tiff
```
Poi, puoi usare il comando:
```sql
SELECT pg_read_binary_file('raster.tiff') AS raster_data;
```
- Per leggere un file TIFF dal percorso relativo alla directory dei log del database, devi prima impostare il parametro `log_directory` nel file `postgresql.conf`. 
- Ad esempio, se imposti `log_directory = '/var/log/postgresql'`, puoi usare il comando:
```sql
SELECT pg_read_binary_file('/var/log/postgresql/raster.tiff') AS raster_data;
```

### Links
- [Permission Denied in PostgreSQL when using pg_read_binary_file](https://stackoverflow.com/questions/77341827/permission-denied-in-postgresql-when-using-pg-read-binary-file)
- [PostgreSQL 9.x - pg_read_binary_file & inserting files into bytea](https://stackoverflow.com/questions/16048649/postgresql-9-x-pg-read-binary-file-inserting-files-into-bytea)
- [Guida rapida PostGIS — OSGeoLive 15.0 Documentation](http://live.osgeo.org/it/quickstart/postgis_quickstart.html)
- [Manuale di PostGIS 3.5.0dev](https://www.postgis.net/docs/manual-dev/postgis-it.html)
- [geopandas.read_postgis](https://geopandas.org/en/stable/docs/reference/api/geopandas.read_postgis.html)
