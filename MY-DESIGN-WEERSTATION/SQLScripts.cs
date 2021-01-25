using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace MY_DESIGN_WEERSTATION
{
    class SQLScripts
    {
        //select instructie om Controle van inlog gegevens te doen 
        public static readonly string Login = "SELECT GebruikerInlognaam, GebruikerWachtwoord FROM tblGebruikers WHERE GebruikerInlognaam=@inlognaam and Gebruikerwachtwoord=@Password;";
       
        // INSERT instructie om nieuwe gebruiker toe te voegen
        public static readonly string Registreren = "INSERT INTO tblGebruikers (GebruikerNaam, GebruikerVoornaam, GebruikerStraat,GebruikerGemeente,  GebruikerHuisNR, GebruikerPostcode,GebruikerNummer,GebruikerEmail,GebruikerWachtwoord,GebruikerInlognaam,GebruikerType) VALUES (@GebruikersNaam,@GebruikersVoornaam, @Straat,@Gemeente,@HuisNR,  @Postcode, @Telefoonnummer,@Email,@Wachtwoord,@LoginNaam,@GebruikerType)";
       
        //SELECT instructie om alle gebruikers weer te geven 
        public static readonly string AlleGebruikers = "SELECT * FROM tblGebruikers";

    
        //SELECT isntructie die gefilterde gebruikers weer geeft voor gebruikers
        public static readonly string FilterVoornaam = "SELECT * FROM tblGebruikers WHERE GebruikerVoornaam =@Voornaam ;";
        public static readonly string FilterNaam = "SELECT * FROM tblGebruikers WHERE GebruikerNaam =@Naam ;";
        public static readonly string FilterNaamEnVoornaam = "SELECT * FROM  tblGebruikers WHERE GebruikerVoornaam =@Voornaam AND GebruikerNaam=@Naam;";
        public static readonly string FilterGemeentes = "SELECT * FROM tblGebruikers WHERE GebruikerGemeente = @Gemeente;";
        public static readonly string FilterStaart = "SELECT * FROM tblGebruikers WHERE GebruikerStraat =@Straat;";
        public static readonly string FilterPostcode = "SELECT * FROM tblGebruikers WHERE GebruikerPostcode =@Postcode;";
        public static readonly string FilterHuisnummer = "SELECT * FROM tblGebruikers WHERE GebruikerHuisNR =@Huisnummer;";
        public static readonly string FilterEmail = "SELECT * FROM tblGebruikers WHERE GebruikerEmail =@Email";
        public static readonly string FilterGebruikerType = "SELECT * FROM tblGebruikers WHERE  GebruikerType =@GebruikerType;";
        public static readonly string FilterAlles = "SELECT * FROM tblGberuikers WHERE GebruiksVoornaam=@Voornaam AND GebruikersNaam=@Naam AND GerbuikerGemeente=@Gemeente AND GebruikerStraat=@Straat AND GebruikerPostcode=@Postcode AND GebruikerHuisNR=@Huisnummer AND GebruikerEmail=@Email AND GebruuikerType=@HebruikerType;";

        //DELETE instructie om gebruiker te verwijderen 
        public static readonly string DeleteGebruiker = "DELETE FROM tblGebruikers WHERE GebruikerID =@ID;";

        //update een gebruiker
        public static readonly string UpdateGebruiker = "UPDATE tblGebruikers SET GebruikerNaam=@Naam, GebruikerVoornaam=@Voornaam, GebruikerStraat=@Straat, GebruikerGemeente=@Gemeente, GebruikerHuisNR=@HuisNR, GebruikerPostcode=@Postcode, GebruikerNummer=@TelefoonNummer, GebruikerEmail=@Email, GebruikerWachtwoord=@Wachtwoord, GebruikerInlognaam=@GebruikersNaam, GebruikerType=@GebruikerType WHERE GebruikerID=@ID;";

        //select insturctie om te checken of inlognaam al bestaat
        public static readonly string ControleInlognaam = "SELECT GebruikerInlognaam FROM tblGebruikers WHERE GebruikerInlognaam=@inlognaam;";

        //Selelct insturctie om te checken welke type
        public static readonly string CheckType = "SELECT GebruikerType FROM tblGebruikers WHERE GebruikerInlognaam=@inlognaam;";

        //Select instructie om ID te halen 
        public static readonly string GetID = "SELECT GebruikerID FROM tblGebruikers WHERE GebruikerInlognaam=@inlognaam;";

        //Insert insturctie voor nieuw weerstation 
        public static readonly string AddWeerstation = "INSERT INTO tblWeerstations (GebruikerID,WeerstationNaam,WeerstationLand,WeerstationGemeente,WeerstationLongitude,WeerstationLatitude,WeerstationRating,WeerstationTypeNetwerk,WeerstationWebsite) VALUES (@GebruikerID, @Naam, @Land, @Gemeente,@Lon,@lat,@Rating, @Type, @Site);";

        //Select instructie om alle weerstations te tonen 
        public static readonly string AlleWeerstations = "SELECT * FROM tblWeerstations";

        //Insert instructie om weergegevens van een station 
        public static readonly string InsertWeerGegevens = "INSERT INTO tblGegevens (GegDatum,GegTemperatuur,GegWind,GegNeerslag,GegLuchtdruk,GegRelativeLuchtvochtigheid,WeerstationID) VALUES(@Datum, @Temperatuur, @Wind, @Neerslag, @Luchtdruk, @RelLuchtvochtigheid, @WeerstationID);";

        //Select instructie om alle gegevens op te vragen van het geslecteerde form
        public static readonly string Gegevens = "SELECT * FROM tblGegevens WHERE WeerstationID=@ID;";

        //Select instructie filter voor Weerstations
        public static readonly string FilterLand = "SELECT * FROM tblWeerstations WHERE WeerstationLand=@Land;";
        public static readonly string FilterGemeente = "SELECT * FROM tblWeerstations WHERE WeerstationGemeente=@Gemeente;";
        public static readonly string FilterType = "SELECT * FROM tblWeerstations WHERE WeerstationTypeNetwerk=@Type;";
        public static readonly string FilterSite = "SELECT *FROM tblWeerstations WHERE WeerstationWebsite=@Site;";
        public static readonly string FilterAllesWeer = "SELECT *FROM tblWeerstations WHERE WeerstationLand=@Land AND WeerstationGemeente=@Gemeente AND WeerstationTypeNetwerk=@Type AND WeerstationWebsite=@Site;";

        //Select instructie filter voor Gegevens
        public static readonly string FilterDatum = "SELECT * FROM tblGegevens WHERE GegDatum =@Datum;";
        public static readonly string FilterTemp = "SELECT *FROM tblGegevens WHERE GegTemperatuur =@Temp;";
        public static readonly string FilterWind = "SELECT *FROM tblGegevens WHERE GegWind =@Wind;";
        public static readonly string FilterNeerslag = "SELECT * FROM tblGegevens WHERE GegNeerslag =@Neerslag";
        public static readonly string FilterLuchtdruk = "SELECT *FROM tblGegevens WHERE GegLuchtdruk =@Luchtdruk;";
        public static readonly string FilterLuchtvochtigheid = "SELECT *FROM tblGegevens WHERE GegRelativeLuchtvochtigheid=@Luchtvochtigheid;";

        //select instructie controle van weerstation naam al bestaat
        public static readonly string ControleStationNaam = "SELECT*FROM tblWeerstations WHERE WeerstationNaam =@Naam;";

        //select instrurtie voor lat te halen van weerstation
        public static readonly string WeerLat = "SELECT WeerstationLatitude FROM tblWeerstations WHERE WeerstationNaam=@Naam;";

        //select instructie voor lon te halen van weerstation
        public static readonly string Weerlon = "SELECT WeerstationLongitude FROM tblWeerstations WHERE WeerstationNaam=@Naam;";

        //delete instructie weerstation
        public static readonly string DeleteWeerstation = "DELETE FROM tblWeerstations WHERE WeerstationID=@ID;";

        //delete instructie van gegevens die bij het verwijderde weerstation horen
        public static readonly string DeleteGegevens = "DELETe FROM tblGegevens WHERE WeerstationID=@ID;";

        //delete instructie om enkel gegevens van een bepaaklde weerstation te verwijderen 
        public static readonly string DeleteEnkelGegevens = "DELETE FROM tblGegevens WHERE GegID=@ID;";
    }
}
