# ServiceDesk

Eine ASP.NET-Core-MVC-Anwendung zur Verwaltung von IT-Tickets, Mitarbeitern und Kategorien.

## Voraussetzungen

- Windows mit SQL Server Express LocalDB (Instanz `MSSQLLocalDB`)
- .NET 10 SDK
- EF-Core-CLI in Version 10 (siehe unten)

## Lokal starten

1. Das Repository klonen oder als ZIP herunterladen und entpacken. Im Hauptordner des Projekts PowerShell öffnen.
2. Die Datenbankverbindung prüfen. `ServiceDesk/appsettings.json` enthält unter `ConnectionStrings:DefaultConnection` die Verbindung zur lokalen Datenbank `ServiceDeskDb`. Bei einer anderen SQL-Server-Instanz den Verbindungsstring lokal anpassen.
3. EF-Core-CLI einmalig installieren. Falls bereits eine Version installiert ist, bei Bedarf mit `dotnet tool update --global dotnet-ef --version "10.*"` aktualisieren.

   ```powershell
   dotnet tool install --global dotnet-ef --version "10.*"
   ```

4. Die Datenbank mit den vorhandenen Migrationen erstellen und die Anwendung starten.

   ```powershell
   dotnet ef database update 20261112122818_ChangedIDname --project ServiceDesk/ServiceDesk.csproj
   dotnet run --project ServiceDesk/ServiceDesk.csproj --launch-profile http
   ```

5. [http://localhost:5174](http://localhost:5174) im Browser öffnen. Zum Beenden im Terminal `Strg+C` drücken.

Die Datenbankmigration verwendet den im Repository vorhandenen Stand. Die Anwendung startet auf dem Dashboard. Bei einer frischen Datenbank können Kategorien, Mitarbeiter und Tickets über die Oberfläche angelegt werden.

## Testdaten einfügen

Zum Testen und Vorführen der Anwendung steht das SQL-Skript [SQLTestDatenDB.sql](SQLTestDatenDB.sql) im Hauptverzeichnis des Repositories bereit. Es fügt **6 Beispielkategorien, 8 Testmitarbeiter und 25 Testtickets** mit unterschiedlichen Status- und Prioritätswerten in die Datenbank `ServiceDeskDb` ein. Die Testtickets sind am Präfix `[TEST]` erkennbar.

1. Zuerst die Datenbank mit den EF-Core-Migrationen erstellen (siehe **Lokal starten**, Schritt 4).
2. In SQL Server Management Studio (SSMS) eine Verbindung zur lokalen SQL-Server-Instanz `(localdb)\\MSSQLLocalDB` herstellen.
3. Die Datenbank **`ServiceDeskDb`** auswählen und die Datei [SQLTestDatenDB.sql](SQLTestDatenDB.sql) öffnen.
4. Das Skript mit **Ausführen (Execute)** starten und anschließend die Anwendung bzw. die Ticketübersicht aktualisieren.

Das Skript löscht oder ändert keine vorhandenen Datensätze. Bereits vorhandene Beispieldaten werden bei erneutem Ausführen übersprungen. Es ist ausschließlich für lokale Test- und Demonstrationsdaten vorgesehen.
