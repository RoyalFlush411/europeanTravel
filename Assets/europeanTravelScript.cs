using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using europeanTravel;
using System.Text.RegularExpressions;

public class europeanTravelScript : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMSelectable bellButton;
    public KMSelectable singleReturnBut;
    public Renderer singleReturnButRend;
    public KMSelectable classBut;
    public Renderer classButRend;
    public KMSelectable fromRight;
    public KMSelectable destinationRight;
    public KMSelectable seatUp;
    public Renderer seatUpRend;
    public KMSelectable digit1Up;
    public Renderer digit1UpRend;
    public KMSelectable digit2Up;
    public Renderer digit2UpRend;
    public KMSelectable digit3Up;
    public Renderer digit3UpRend;
    public KMSelectable digit4Up;
    public Renderer digit4UpRend;
    public KMSelectable digit5Up;
    public Renderer digit5UpRend;
    public Renderer topBorder;
    public Renderer bottomBorder;

    public KMAudio Audio;

    //Set-up
    public Material ticketMat;
    public Renderer stamp;

    //Country
    private string country = "";

    //City Lists
    private List<string> fromCities = new List<string>();
    private List<string> destinationCities = new List<string>();
    public TextMesh fromCitiesText;
    public TextMesh destinationCitiesText;
    int fromIndex;
    int destinationIndex;

    //Top buttons
    public TextMesh singleReturnText;
    public TextMesh classText;

    //Serial Number
    string serial = "";
    public TextMesh serialText;

    //Seat
    private string[] seatAllocation = new string[] { "1A", "1B", "2A", "2B", "3A", "3B", "4A", "4B"};
    public TextMesh seatText;
    int seatIndex = 0;

    //Price
    string price = "000,00";
    public TextMesh priceText;

    //Answers
    string correctFrom;
    string correctDestination;
    string correctSingle;
    string correctClass;
    string correctSeat;
    string correctPrice;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    bool solved = false;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        singleReturnBut.OnInteract += delegate () { OnsingleReturnBut(); return false; };
        classBut.OnInteract += delegate () { OnclassBut(); return false; };
        fromRight.OnInteract += delegate () { OnfromRight(); return false; };
        destinationRight.OnInteract += delegate () { OndestinationRight(); return false; };
        seatUp.OnInteract += delegate () { OnseatUp(); return false; };
        digit1Up.OnInteract += delegate () { Ondigit1Up(); return false; };
        digit2Up.OnInteract += delegate () { Ondigit2Up(); return false; };
        digit3Up.OnInteract += delegate () { Ondigit3Up(); return false; };
        digit4Up.OnInteract += delegate () { Ondigit4Up(); return false; };
        digit5Up.OnInteract += delegate () { Ondigit5Up(); return false; };
        bellButton.OnInteract += delegate () { OnbellButton(); return false; };
        stamp.gameObject.SetActive(false);
    }

    void Start()
    {
        countryPicker();
        while (serial.Length < 6)
        {
            serialGenerator();
        }
        Debug.LogFormat("[European Travel #{0}] The serial number is {1}.", moduleId, serial);
        serialText.text = serial;
        randomStart();
        fromIndex = UnityEngine.Random.Range(0, fromCities.Count);
        fromCitiesText.text = fromCities[fromIndex];
        destinationIndex = UnityEngine.Random.Range(0, destinationCities.Count);
        destinationCitiesText.text = destinationCities[destinationIndex];
        answerPicker();
    }

    void answerPicker()
    {
        if (serial[0] == 'A' || serial[0] == 'B' || serial[0] == 'C')
        {
            correctFrom = fromCities[0];
        }
        else if (serial[0] == 'D' || serial[0] == 'E' || serial[0] == 'F')
        {
            correctFrom = fromCities[1];
        }
        else if (serial[0] == 'G' || serial[0] == 'H' || serial[0] == 'I')
        {
            correctFrom = fromCities[2];
        }
        else if (serial[0] == 'J' || serial[0] == 'K' || serial[0] == 'L')
        {
            correctFrom = fromCities[3];
        }
        else if (serial[0] == 'M' || serial[0] == 'N' || serial[0] == 'P')
        {
            correctFrom = fromCities[4];
        }
        else if (serial[0] == 'Q' || serial[0] == 'R' || serial[0] == 'S')
        {
            correctFrom = fromCities[5];
        }
        else if (serial[0] == 'T' || serial[0] == 'U' || serial[0] == 'V')
        {
            correctFrom = fromCities[6];
        }
        else if (serial[0] == 'W' || serial[0] == 'X' || serial[0] == 'Y')
        {
            correctFrom = fromCities[7];
        }
        else if (serial[0] == 'Z' || serial[0] == '0' || serial[0] == '1')
        {
            correctFrom = fromCities[8];
        }
        else if (serial[0] == '2' || serial[0] == '3' || serial[0] == '4')
        {
            correctFrom = fromCities[9];
        }
        else if (serial[0] == '5' || serial[0] == '6' || serial[0] == '7')
        {
            correctFrom = fromCities[10];
        }
        else if (serial[0] == '8' || serial[0] == '9')
        {
            correctFrom = fromCities[11];
        }
        Debug.LogFormat("[European Travel #{0}] The departure city is {1}.", moduleId, correctFrom);


        if (serial[1] == 'A' || serial[1] == 'B' || serial[1] == 'C')
        {
            correctDestination = destinationCities[0];
        }
        else if (serial[1] == 'D' || serial[1] == 'E' || serial[1] == 'F')
        {
            correctDestination = destinationCities[1];
        }
        else if (serial[1] == 'G' || serial[1] == 'H' || serial[1] == 'I')
        {
            correctDestination = destinationCities[2];
        }
        else if (serial[1] == 'J' || serial[1] == 'K' || serial[1] == 'L')
        {
            correctDestination = destinationCities[3];
        }
        else if (serial[1] == 'M' || serial[1] == 'N' || serial[1] == 'P')
        {
            correctDestination = destinationCities[4];
        }
        else if (serial[1] == 'Q' || serial[1] == 'R' || serial[1] == 'S')
        {
            correctDestination = destinationCities[5];
        }
        else if (serial[1] == 'T' || serial[1] == 'U' || serial[1] == 'V')
        {
            correctDestination = destinationCities[6];
        }
        else if (serial[1] == 'W' || serial[1] == 'X' || serial[1] == 'Y')
        {
            correctDestination = destinationCities[7];
        }
        else if (serial[1] == 'Z' || serial[1] == '0' || serial[1] == '1')
        {
            correctDestination = destinationCities[8];
        }
        else if (serial[1] == '2' || serial[1] == '3' || serial[1] == '4')
        {
            correctDestination = destinationCities[9];
        }
        else if (serial[1] == '5' || serial[1] == '6' || serial[1] == '7')
        {
            correctDestination = destinationCities[10];
        }
        else if (serial[1] == '8' || serial[1] == '9')
        {
            correctDestination = destinationCities[11];
        }
        Debug.LogFormat("[European Travel #{0}] The destination city is {1}.", moduleId, correctDestination);


        if (serial[2] == '0' || serial[2] == '1' || serial[2] == '2' || serial[2] == '3' || serial[2] == '4' || serial[2] == '5' || serial[2] == '6' || serial[2] == '7' || serial[2] == '8' || serial[2] == '9')
        {
            correctClass = "2nd class";
        }
        else
        {
            correctClass = "1st class";
        }
        if (serial[4] == '0' || serial[4] == '1' || serial[4] == '2' || serial[4] == '3' || serial[4] == '4' || serial[4] == '5' || serial[4] == '6' || serial[4] == '7' || serial[4] == '8' || serial[4] == '9')
        {
            correctSingle = "RTN";
        }
        else
        {
            correctSingle = "SGL";
        }
        Debug.LogFormat("[European Travel #{0}] The correct ticket type is {1} {2}.", moduleId, correctClass, correctSingle);

        if (correctClass == "2nd class")
        {
            if (serial[3] == 'A' || serial[3] == 'B' || serial[3] == 'C')
            {
                correctPrice = "023,99";
            }
            else if (serial[3] == 'D' || serial[3] == 'E' || serial[3] == 'F')
            {
                correctPrice = "095,54";
            }
            else if (serial[3] == 'G' || serial[3] == 'H' || serial[3] == 'I')
            {
                correctPrice = "053,11";
            }
            else if (serial[3] == 'J' || serial[3] == 'K' || serial[3] == 'L')
            {
                correctPrice = "010,83";
            }
            else if (serial[3] == 'M' || serial[3] == 'N' || serial[3] == 'P')
            {
                correctPrice = "005,12";
            }
            else if (serial[3] == 'Q' || serial[3] == 'R' || serial[3] == 'S')
            {
                correctPrice = "102,33";
            }
            else if (serial[3] == 'T' || serial[3] == 'U' || serial[3] == 'V')
            {
                correctPrice = "076,00";
            }
            else if (serial[3] == 'W' || serial[3] == 'X' || serial[3] == 'Y')
            {
                correctPrice = "014,22";
            }
            else if (serial[3] == 'Z' || serial[3] == '0' || serial[3] == '1')
            {
                correctPrice = "088,90";
            }
            else if (serial[3] == '2' || serial[3] == '3' || serial[3] == '4')
            {
                correctPrice = "121,44";
            }
            else if (serial[3] == '5' || serial[3] == '6' || serial[3] == '7')
            {
                correctPrice = "001,98";
            }
            else if (serial[3] == '8' || serial[3] == '9')
            {
                correctPrice = "033,08";
            }
        }
        else
        {
            if (serial[3] == 'A' || serial[3] == 'B' || serial[3] == 'C')
            {
                correctPrice = "047,98";
            }
            else if (serial[3] == 'D' || serial[3] == 'E' || serial[3] == 'F')
            {
                correctPrice = "191,08";
            }
            else if (serial[3] == 'G' || serial[3] == 'H' || serial[3] == 'I')
            {
                correctPrice = "106,22";
            }
            else if (serial[3] == 'J' || serial[3] == 'K' || serial[3] == 'L')
            {
                correctPrice = "021,66";
            }
            else if (serial[3] == 'M' || serial[3] == 'N' || serial[3] == 'P')
            {
                correctPrice = "010,24";
            }
            else if (serial[3] == 'Q' || serial[3] == 'R' || serial[3] == 'S')
            {
                correctPrice = "204,66";
            }
            else if (serial[3] == 'T' || serial[3] == 'U' || serial[3] == 'V')
            {
                correctPrice = "152,00";
            }
            else if (serial[3] == 'W' || serial[3] == 'X' || serial[3] == 'Y')
            {
                correctPrice = "028,44";
            }
            else if (serial[3] == 'Z' || serial[3] == '0' || serial[3] == '1')
            {
                correctPrice = "177,80";
            }
            else if (serial[3] == '2' || serial[3] == '3' || serial[3] == '4')
            {
                correctPrice = "242,88";
            }
            else if (serial[3] == '5' || serial[3] == '6' || serial[3] == '7')
            {
                correctPrice = "003,96";
            }
            else if (serial[3] == '8' || serial[3] == '9')
            {
                correctPrice = "066,16";
            }
        }
        Debug.LogFormat("[European Travel #{0}] The correct price is €{1}.", moduleId, correctPrice);


        if (serial[5] >='A' && serial[5] <='G')
        {
            correctSeat = "1A";
        }
        else if (serial[5] >='H' && serial[5] <='P')
        {
            correctSeat = "1B";
        }
        else if (serial[5] >='Q' && serial[5] <='T')
        {
            correctSeat = "2A";
        }
        else if (serial[5] >='U' && serial[5] <='Z')
        {
            correctSeat = "2B";
        }
        else if (serial[5] >='Q' && serial[5] <='T')
        {
            correctSeat = "2A";
        }
        else if (serial[5] >='0' && serial[5] <='2')
        {
            correctSeat = "3A";
        }
        else if (serial[5] >='3' && serial[5] <='5')
        {
            correctSeat = "3B";
        }
        else if (serial[5] >='6' && serial[5] <='8')
        {
            correctSeat = "4A";
        }
        else
        {
            correctSeat = "4B";
        }
        Debug.LogFormat("[European Travel #{0}] The correct seat number is {1}.", moduleId, correctSeat);
    }

    void countryPicker()
    {
        int countryIndex = UnityEngine.Random.Range(0, 6);
        switch (countryIndex)
        {
            case 0:
               country = "The Netherlands";
               Material netherlandsMat = new Material(ticketMat);
               netherlandsMat.color = new Color(1.0f, 0.449f, 0.0f); //Orange
               singleReturnButRend.material = netherlandsMat;
               classButRend.material = netherlandsMat;
               seatUpRend.material = netherlandsMat;
               topBorder.material = netherlandsMat;
               bottomBorder.material = netherlandsMat;
               fromCities.Add("Zwolle");
               fromCities.Add("Groningen");
               fromCities.Add("Amsterdam CS");
               fromCities.Add("Utrecht CS");
               fromCities.Add("Den Haag CS");
               fromCities.Add("Zutphen");
               fromCities.Add("Maastricht");
               fromCities.Add("Schiphol A'port");
               fromCities.Add("Delft");
               fromCities.Add("Alkmaar");
               fromCities.Add("Lelystad Zuid");
               fromCities.Add("Kampen");
               destinationCities.Add("Gouda");
               destinationCities.Add("Leiden CS");
               destinationCities.Add("Leeuwarden");
               destinationCities.Add("Middelburg");
               destinationCities.Add("Rotterdam CS");
               destinationCities.Add("Deurne");
               destinationCities.Add("Deventer");
               destinationCities.Add("Assen");
               destinationCities.Add("Eindhoven");
               destinationCities.Add("Nijmegen");
               destinationCities.Add("Zandvoort aan Zee");
               destinationCities.Add("Kerkrade Centrum");
               break;

            case 1:
                country = "United Kingdom";
                Material ukMaterial = new Material(ticketMat);
                ukMaterial.color = new Color(0.596f, 0.984f, 0.596f); //Pale green
                singleReturnButRend.material = ukMaterial;
                classButRend.material = ukMaterial;
                seatUpRend.material = ukMaterial;
                topBorder.material = ukMaterial;
                bottomBorder.material = ukMaterial;
                fromCities.Add("Swansea");
                fromCities.Add("Coventry");
                fromCities.Add("Peterborough");
                fromCities.Add("Cambridge");
                fromCities.Add("Stoke-on-Trent");
                fromCities.Add("Watford Junction");
                fromCities.Add("Exeter");
                fromCities.Add("Portsmouth H'bour");
                fromCities.Add("Heathrow A'port");
                fromCities.Add("Luton");
                fromCities.Add("Dover");
                fromCities.Add("Brighton");
                destinationCities.Add("Bristol Temple Meads");
                destinationCities.Add("Pembroke Dock");
                destinationCities.Add("London St. Pancras");
                destinationCities.Add("Aylesbury");
                destinationCities.Add("Chester");
                destinationCities.Add("Bangor");
                destinationCities.Add("Stourbridge Town");
                destinationCities.Add("Nottingham");
                destinationCities.Add("Manchester Victoria");
                destinationCities.Add("Sheffield");
                destinationCities.Add("Wolverhamption");
                destinationCities.Add("Hull");
                break;

            case 2:
                country = "Germany";
                Material germanyMat = new Material(ticketMat);
                germanyMat.color = new Color(0.9f, 0.0f, 0.0f); //Red
                singleReturnButRend.material = germanyMat;
                classButRend.material = germanyMat;
                seatUpRend.material = germanyMat;
                topBorder.material = germanyMat;
                bottomBorder.material = germanyMat;
                fromCities.Add("Ulm Hbf.");
                fromCities.Add("Emden Hbf.");
                fromCities.Add("Cottbus");
                fromCities.Add("Erfurt Hbf.");
                fromCities.Add("Kiel Hbf.");
                fromCities.Add("Potsdam Hbf.");
                fromCities.Add("Ingolstadt Hbf.");
                fromCities.Add("Berlin Ost.");
                fromCities.Add("Mainz Hbf.");
                fromCities.Add("Frankfurt F'hafen");
                fromCities.Add("Regensburg Hbf.");
                fromCities.Add("Oberstdorf");
                destinationCities.Add("Leipzig Hbf.");
                destinationCities.Add("Augsburg Hbf.");
                destinationCities.Add("Bonn Hbf.");
                destinationCities.Add("Leer (Ostfriesl)");
                destinationCities.Add("Bielefeld Hbf.");
                destinationCities.Add("Chemnitz Hbf.");
                destinationCities.Add("Karlsruhe Hbf.");
                destinationCities.Add("Freiburg Hbf.");
                destinationCities.Add("Lübeck Hbf.");
                destinationCities.Add("Wittenberge");
                destinationCities.Add("Dessau Hbf.");
                destinationCities.Add("Jena Paradies");
                break;

            case 3:
                country = "France";
                Material franceMat = new Material(ticketMat);
                franceMat.color = new Color(0.0f, 1.0f, 1.0f); //Aqua
                singleReturnButRend.material = franceMat;
                classButRend.material = franceMat;
                seatUpRend.material = franceMat;
                topBorder.material = franceMat;
                bottomBorder.material = franceMat;
                fromCities.Add("Clermont-Ferrand");
                fromCities.Add("Bordeaux St-Jean");
                fromCities.Add("Lille");
                fromCities.Add("Montargis");
                fromCities.Add("Grenoble");
                fromCities.Add("Cannes");
                fromCities.Add("Redon");
                fromCities.Add("Biarritz");
                fromCities.Add("Limoges");
                fromCities.Add("Rouen-Rive-Droite");
                fromCities.Add("Le Havre");
                fromCities.Add("Dijon-Ville");
                destinationCities.Add("C. De Gaulle A'port");
                destinationCities.Add("St-Dizier");
                destinationCities.Add("Boulogne-Ville");
                destinationCities.Add("Paris Gare du Nord");
                destinationCities.Add("Poitiers");
                destinationCities.Add("Angers-Saint-Laud");
                destinationCities.Add("Nancy-Ville");
                destinationCities.Add("Lisieux");
                destinationCities.Add("Marseille St-Charles");
                destinationCities.Add("Toul");
                destinationCities.Add("Perpignan");
                destinationCities.Add("Nîmes");
                break;

            case 4:
                country = "Spain";
                Material spainMat = new Material(ticketMat);
                spainMat.color = new Color(0.855f, 0.647f, 0.125f); //Goldenrod yellow
                singleReturnButRend.material = spainMat;
                classButRend.material = spainMat;
                seatUpRend.material = spainMat;
                topBorder.material = spainMat;
                bottomBorder.material = spainMat;
                fromCities.Add("Santander");
                fromCities.Add("Ferrol");
                fromCities.Add("Plasencia");
                fromCities.Add("Córdoba");
                fromCities.Add("Almería");
                fromCities.Add("Gandía");
                fromCities.Add("Albacete");
                fromCities.Add("Aranjuez");
                fromCities.Add("Cádiz");
                fromCities.Add("Jaca");
                fromCities.Add("Vitoria");
                fromCities.Add("Murcia del Carmen");
                destinationCities.Add("Girona");
                destinationCities.Add("Soria");
                destinationCities.Add("Ourense-Empalme");
                destinationCities.Add("Zafra");
                destinationCities.Add("Málaga");
                destinationCities.Add("San Sebastián");
                destinationCities.Add("Reus");
                destinationCities.Add("Barcelona Sants");
                destinationCities.Add("Tarragona");
                destinationCities.Add("Guadalajara");
                destinationCities.Add("Madrid Atocha");
                destinationCities.Add("Linares-Baeza");
                break;

            case 5:
                country = "Belgium";
                Material belgiumMat = new Material(ticketMat);
                belgiumMat.color = new Color(0.804f, 0.361f, 0.361f); //Indian red
                singleReturnButRend.material = belgiumMat;
                classButRend.material = belgiumMat;
                seatUpRend.material = belgiumMat;
                topBorder.material = belgiumMat;
                bottomBorder.material = belgiumMat;
                fromCities.Add("Antwerpen-Zuid");
                fromCities.Add("Lokeren");
                fromCities.Add("Tielen");
                fromCities.Add("Hasselt");
                fromCities.Add("Sint-Joris-Weert");
                fromCities.Add("Waregem");
                fromCities.Add("Oostende");
                fromCities.Add("Enghien");
                fromCities.Add("Lierde");
                fromCities.Add("Brussel-Zuid");
                fromCities.Add("Halle");
                fromCities.Add("Gent-Sint-Pieters");
                destinationCities.Add("Charleroi-Sud");
                destinationCities.Add("Aarschot");
                destinationCities.Add("Mechelen");
                destinationCities.Add("Leuven");
                destinationCities.Add("Spa");
                destinationCities.Add("Idegem");
                destinationCities.Add("Tongeren");
                destinationCities.Add("Villers-La-Ville");
                destinationCities.Add("De Panne");
                destinationCities.Add("Knokke");
                destinationCities.Add("Zeebrugge-Strand");
                destinationCities.Add("Kortrijk");
                break;
          }
          Debug.LogFormat("[European Travel #{0}] The chosen country is {1}.", moduleId, country);
    }

    void randomStart()
    {
        seatIndex = UnityEngine.Random.Range(0, 8);
        seatText.text = seatAllocation[seatIndex];
        int singRetRand = UnityEngine.Random.Range(0, 2);
        if (singRetRand == 0)
        {
            singleReturnText.text = "RTN";
        }
        else
        {
            singleReturnText.text = "SGL";
        }

        int classRand = UnityEngine.Random.Range(0, 2);
        if (classRand == 0)
        {
            classText.text = "2nd class";
        }
        else
        {
            classText.text = "1st class";
        }
    }

    void serialGenerator()
    {
        int serialIndex = UnityEngine.Random.Range(0, 35);

        switch (serialIndex)
        {
            case 0:
                serial += "A";
                break;
            case 1:
                serial += "C";
                break;
            case 2:
                serial += "E";
                break;
            case 3:
                serial += "G";
                break;
            case 4:
                serial += "I";
                break;
            case 5:
                serial += "K";
                break;
            case 6:
                serial += "M";
                break;
            case 7:
                serial += "N";
                break;
            case 8:
                serial += "Q";
                break;
            case 9:
                serial += "S";
                break;
            case 10:
                serial += "U";
                break;
            case 11:
                serial += "W";
                break;
            case 12:
                serial += "Y";
                break;
            case 13:
                serial += "0";
                break;
            case 14:
                serial += "1";
                break;
            case 15:
                serial += "2";
                break;
            case 16:
                serial += "3";
                break;
            case 17:
                serial += "4";
                break;
            case 18:
                serial += "5";
                break;
            case 19:
                serial += "6";
                break;
            case 20:
                serial += "7";
                break;
            case 21:
                serial += "8";
                break;
            case 22:
                serial += "9";
                break;
            case 23:
                serial += "B";
                break;
            case 24:
                serial += "D";
                break;
            case 25:
                serial += "F";
                break;
            case 26:
                serial += "H";
                break;
            case 27:
                serial += "J";
                break;
            case 28:
                serial += "L";
                break;
            case 29:
                serial += "P";
                break;
            case 30:
                serial += "R";
                break;
            case 31:
                serial += "T";
                break;
            case 32:
                serial += "V";
                break;
            case 33:
                serial += "X";
                break;
            case 34:
                serial += "Z";
                break;
        }
    }

    //Buttons
    public void OnsingleReturnBut()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        singleReturnBut.AddInteractionPunch(.5f);
        if (solved == false)
        {
            if (singleReturnText.text == "RTN")
            {
                singleReturnText.text = "SGL";
            }
            else
            {
                singleReturnText.text = "RTN";
            }
        }
    }

    public void OnclassBut()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        classBut.AddInteractionPunch(.5f);
        if (solved == false)
        {
            if (classText.text == "1st class")
            {
                classText.text = "2nd class";
            }
            else
            {
                classText.text = "1st class";
            }
        }
    }

    public void OnfromRight()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, fromRight.transform);
        fromRight.AddInteractionPunch(.5f);
        if (solved == false)
        {
            fromIndex = (fromIndex + 1) % fromCities.Count;
            fromCitiesText.text = fromCities[fromIndex];
        }
    }

    public void OndestinationRight()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, destinationRight.transform);
        destinationRight.AddInteractionPunch(.5f);
        if (solved == false)
        {
            destinationIndex = (destinationIndex + 1) % destinationCities.Count;
            destinationCitiesText.text = destinationCities[destinationIndex];
        }
    }

    public void OnseatUp()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        seatUp.AddInteractionPunch(.5f);
        if (solved == false)
        {
            seatIndex = (seatIndex + 1) % seatAllocation.Length;
            seatText.text = seatAllocation[seatIndex];
        }
    }

    public void Ondigit1Up()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        digit1Up.AddInteractionPunch(.5f);
        if (solved == false)
        {
            char[] priceChars = price.ToCharArray();
            int number = (int)(priceChars[0] - '0');
            number = (number + 1) % 10;
            priceChars[0] = (char)('0' + number);

            price = new string(priceChars);
            priceText.text = price;
        }
    }

    public void Ondigit2Up()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        digit2Up.AddInteractionPunch(.5f);
        if (solved == false)
        {
            char[] priceChars = price.ToCharArray();
            int number = (int)(priceChars[1] - '0');
            number = (number + 1) % 10;
            priceChars[1] = (char)('0' + number);

            price = new string(priceChars);
            priceText.text = price;
        }
    }

    public void Ondigit3Up()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        digit2Up.AddInteractionPunch(.5f);
        if (solved == false)
        {
            char[] priceChars = price.ToCharArray();
            int number = (int)(priceChars[2] - '0');
            number = (number + 1) % 10;
            priceChars[2] = (char)('0' + number);

            price = new string(priceChars);
            priceText.text = price;
        }
    }

    public void Ondigit4Up()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        digit4Up.AddInteractionPunch(.5f);
        if (solved == false)
        {
            char[] priceChars = price.ToCharArray();
            int number = (int)(priceChars[4] - '0');
            number = (number + 1) % 10;
            priceChars[4] = (char)('0' + number);

            price = new string(priceChars);
            priceText.text = price;
        }
    }

    public void Ondigit5Up()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        digit5Up.AddInteractionPunch(.5f);
        if (solved == false)
        {
            char[] priceChars = price.ToCharArray();
            int number = (int)(priceChars[5] - '0');
            number = (number + 1) % 10;
            priceChars[5] = (char)('0' + number);

            price = new string(priceChars);
            priceText.text = price;
        }
    }

    public void OnbellButton()
    {
        GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
        bellButton.AddInteractionPunch();
        if (solved == false)
        {
            if (singleReturnText.text == correctSingle && classText.text == correctClass && fromCitiesText.text == correctFrom && destinationCitiesText.text == correctDestination && seatText.text == correctSeat && priceText.text == correctPrice)
            {
                Audio.PlaySoundAtTransform("bell", transform);
                stamp.gameObject.SetActive(true);
                GetComponent<KMBombModule>().HandlePass();
                Debug.LogFormat("[European Travel #{0}] Module disarmed. All aboard!", moduleId);
                solved = true;
            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                Debug.LogFormat("[European Travel #{0}] Strike! Your ticket is a {1} {2} from {3} to {4} on seat {5} costing €{6}. Unfortunately, that's not right!", moduleId, classText.text, singleReturnText.text, fromCitiesText.text, destinationCitiesText.text, seatText.text, priceText.text);
            }
        }
        else
        {
            GetComponent<KMBombModule>().HandleStrike();
            Debug.LogFormat("[European Travel #{0}] Strike! Your train has departed!", moduleId);
        }
    }

	bool AtLeast(string input, string target, int min = 1)
	{
		return input.Length >= min && input.Length <= target.Length && target.StartsWith(input);
	}

	string NormalizeString(string input)
	{
		return input.Replace('ü', 'u').Replace('ó', 'o').Replace('á', 'a').Replace('í', 'i').Replace('î', 'i');
	}

	int ModDistance(int a, int b, int c)
	{
		return (b - a + c) % c;
	}

    #pragma warning disable 414
	private string TwitchHelpMessage = "Submit your answer using !{0} submit <type>;<class>;<departure>;<destination>;<seat>;<price>. City names are matched from the beginning and must be unique.";
    #pragma warning restore 414

	private IEnumerator ProcessTwitchCommand(string command)
	{
		List<KMSelectable> selectables = new List<KMSelectable>();

		if (command.Substring(0, 7) == "submit ") command = command.Remove(0, 7);

		command = command.ToLowerInvariant();
		string[] options = command.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

		bool rtn = AtLeast(options[0], "rtn");
		if (rtn != (singleReturnText.text == "RTN")) selectables.Add(singleReturnBut);

		bool firstclass = AtLeast(options[1], "1st");
		if (firstclass != (classText.text == "1st class")) selectables.Add(classBut);

		var cities = fromCities.Where(x => NormalizeString(x.ToLowerInvariant()).StartsWith(options[2]));
		int count = cities.Count();
		if (count != 1)
		{
			if (count == 0) yield return "sendtochaterror That doesn't match any departure cities.";
			else if (count > 1) yield return "sendtochaterror That matches more than one departure city.";
			yield break;
		}

		for (int i = 0; i < ModDistance(fromIndex, fromCities.IndexOf(cities.First()), fromCities.Count); i++) selectables.Add(fromRight);

		cities = destinationCities.Where(x => NormalizeString(x.ToLowerInvariant()).StartsWith(options[3]));
		count = cities.Count();
		if (count != 1)
		{
			if (count == 0) yield return "sendtochaterror That doesn't match any destination cities.";
			else if (count > 1) yield return "sendtochaterror That matches more than one destination city.";
			yield break;
		}

		for (int i = 0; i < ModDistance(destinationIndex, destinationCities.IndexOf(cities.First()), destinationCities.Count); i++) selectables.Add(destinationRight);

		int presses = ModDistance(seatIndex, seatAllocation.ToList().IndexOf(options[4].ToUpperInvariant()), seatAllocation.Length);
		for (int i = 0; i < presses; i++) selectables.Add(seatUp);

		Match targetPrice = Regex.Match(options[5], @"^€?(\d{1,3})(?:[.,](\d{1,2}))?$");
		if (targetPrice.Success)
		{
			var priceInts = price.Remove(3, 1).ToCharArray().Select(x => x - '0').ToArray();
			var digitButtons = new[] { digit1Up, digit2Up, digit3Up, digit4Up, digit5Up };

			string number = targetPrice.Groups[1].Value.PadLeft(3, '0') + (targetPrice.Groups.Count == 2 ? targetPrice.Groups[2].Value.PadRight(2, '0') : "");
			for (int i = 0; i < 5; i++)
			{
				presses = ModDistance(priceInts[i], number[i] - '0', 10);
				for (int a = 0; a < presses; a++) selectables.Add(digitButtons[i]);
			}
		}
		else
		{
			yield return "sendtochaterror That's a invalid price.";
			yield break;
		}

		selectables.Add(bellButton);

		foreach (KMSelectable selectable in selectables)
		{
			selectable.OnInteract();
			yield return new WaitForSeconds(0.025f);
		}
	}
}
