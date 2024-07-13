// See https://aka.ms/new-console-template for more information
using ExtraVert;
using System.ComponentModel.DataAnnotations;
using System.Linq;

string greeting = "Welcome to ExtraVert!";

Console.WriteLine(@greeting);

List<Plant> plants = new List<Plant>()
{
    new Plant("Tulip", 2, 15.50M, "Los Angeles", 90001, true, new DateTime(2024,7,7)),
    new Plant("Rose", 3, 25.99m, "New York", 10001, false, new DateTime(2024,6,7)),
    new Plant("Orchid", 4, 35.75m, "Miami", 33101, false, new DateTime(2024,10,7)),
    new Plant("Fern", 2, 10.25m, "Seattle", 98101, true, new DateTime(2024,1,7)),
    new Plant("Sunflower", 5, 19.99m, "Chicago", 60601, false, new DateTime(2024,3,7)),
    new Plant("Lavender", 3, 18.50m, "Denver", 80201, false, new DateTime(2024,11,7)),
    new Plant("Daisy", 2, 12.75m, "San Francisco", 94101, false, new DateTime(2024,9,7)),
    new Plant("Peony", 4, 29.99m, "Boston", 02101, true, new DateTime(2024,4,7)),
    new Plant("Cactus", 1, 8.99m, "Phoenix", 85001, false, new DateTime(2024,7,7)),
    new Plant("Hydrangea", 3, 22.50m, "Portland", 97201, true, new DateTime(2024,7,7))
};


Random random = new Random();
Plant plantOfTheDay = null;

string choice = null;
while (choice != "0")
{
    PlantOfTheDayIndex();
    Console.WriteLine($"The plant of the day is a {plantOfTheDay.Species} in {plantOfTheDay.City} for {plantOfTheDay.AskingPrice} dollars.");


    Console.WriteLine(@" Chose an Option:
      0. Exit
      1. Display all plants
      2. Post a plant to be adopted
      3. Adopt a plant
      4. Delist a plant
      5. Search for plants by light needs
      6. Stats for ExtraVert");


    choice = Console.ReadLine();

    if (choice == "0")
    {
        Console.WriteLine("Goodbye!");
        Console.Clear();
    }
    else if (choice == "1")
    {
        ListPlants(plants);
    }
    else if (choice == "2")
    {
        PostAPlant();
    }
    else if (choice == "3")
    {
        AdobtAPlant();
    }
    else if (choice == "4")
    {
        DelistAPlant();
    }
    else if ( choice == "5")
    {
        SearchByLightNeeds();
    }
    else if (choice == "6")
    {
        StatsForExtraVert();
    }
    else
    {
        Console.WriteLine("Invalid option. Please choose a valid option from the list.");
        Console.Clear();
    }
}


void PlantOfTheDayIndex()
{
    // using a while loop so that the random fetch stops when we found a random number that suits the condition
    while (true)
    {
        // Creating a list of plants using the plants varible and only pulling the plants where it is not sold
        List<Plant> avilablePlants = plants.Where(p => !p.Sold && p.AvilableUntil >= DateTime.Now).ToList();

        // is avilable plants come back as true
        if (avilablePlants.Any())
        {
            // call our random varible and give it the range from 0 to the length  -1 to consider the 0 index
            int randomIndex = random.Next(0,avilablePlants.Count -1);
            // we reassign the plantOfTheDay a new value so it is no longer null
            // taking the avilable plants list and passing it the random number that will be used as an index
            plantOfTheDay = avilablePlants[randomIndex];
            // break the while loop
            //program continues executing the next line of code after the loop.
            break;
        }
        else
        {
            // this runs when any is false because there are no plants to pick from
            Console.WriteLine("No plants are aviliable for adoption ");
            // this is returned because we want to exit the method immediately
            // terminates further execution of the method, returning control to where the method was called from.
            return;
        }
    }

}

void ListPlants(List<Plant> plantToDisplay)
    {
        for (int i = 0; i < plantToDisplay.Count; i++)
        {
        Plant plant = plantToDisplay[i];
        Console.WriteLine($"{i + 1}. A {plant.Species} in {plant.City} {(plant.Sold ? "was sold" : "is available")} for {plant.AskingPrice} dollars.");
        }
    }

void PostAPlant()
{
    Console.WriteLine("What species is this plant?");
    string plantSpecies = Console.ReadLine();

    int plantLightNeeds;
    while( true )
    {
         Console.WriteLine("On a scale of 1-5, 1 being shade, 5 representing full sun. What is your plant's light needs?");

        if (int.TryParse(Console.ReadLine(), out plantLightNeeds) && plantLightNeeds >= 1 && plantLightNeeds <=5) 
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
        }
    }

    decimal plantPrice;
    while (true)
    {
        Console.WriteLine("What is your asking price for your plant");

        if (decimal.TryParse(Console.ReadLine(), out plantPrice))
        {
            break;
        }    
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid price.");
        }
    } 

    string plantCity;

    //This initiates an indefinite loop that will continue to execute until explicitly stopped by a break statement.
    while (true)
    {
        Console.WriteLine("What city is this plant located in?");
        plantCity = Console.ReadLine();

        //checks if the input is not null, empty, or whitespace and does not have a digit ccharacter
        if (!string.IsNullOrWhiteSpace(plantCity) && !plantCity.Any(char.IsDigit)) 
        {
            //When the condition inside the if statement evaluates to true, you use break to exit the loop.
            break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid price.");
        }
    }


    int plantZIP;
    while (true)
    {
        Console.WriteLine("What is the ZIP code?");

        if (int.TryParse(Console.ReadLine(), out plantZIP))
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid ZIP code?.");
        }
    }

    DateTime availableUntil;
    while (true)
    {
        Console.WriteLine("How long will this plant be avilable for? Enter answer in the following format:  yyyy/mm/dd ");
        
        if (DateTime.TryParse(Console.ReadLine(), out availableUntil))
        {
            break;
        }
        else
        {
            Console.WriteLine("Invalid input, please enter in the following format YYYY/MM/DD ");
        }
    }

    bool plantSold = false;

    Plant newPlant = new Plant(plantSpecies, plantLightNeeds, plantPrice, plantCity, plantZIP, plantSold, availableUntil);

    plants.Add(newPlant);

    Console.WriteLine("Plant has been posted successfully!");

}

void AdobtAPlant()

    {
    Console.WriteLine("Your adobtion journey starts here! Choose one of the following plants to adopt: ");
    List<Plant> avilablePlants = plants.Where(p => !p.Sold && p.AvilableUntil >= DateTime.Now).ToList();


    //if (availablePlants.Count == 0)
    if (!avilablePlants.Any()) 
    {
        Console.WriteLine("No plants are avilable for adoption :( ");
        return;

    }

    ListPlants(avilablePlants);
    //for (int i = 0; i < avilablePlants.Count; i++)
    //{
    //    Plant plant = avilablePlants[i];
    //    Console.WriteLine($"{i + 1}. A {plant.Species} in {plant.City} is available for {plant.AskingPrice} dollars.");
   // }
   
    //ensures the input is at least 1 and does not exceed the number of available plants.

    while (true)
    {
        Console.WriteLine("Enter the number of the plant you want to adopt. ");
        if (int.TryParse(Console.ReadLine(), out int plantIndex) && plantIndex >= 1 && plantIndex <= avilablePlants.Count)
        {
            Plant selectedPlant = avilablePlants[plantIndex - 1];
            selectedPlant.Sold = true;
            Console.WriteLine($"You have just adopted {selectedPlant.Species}");
            break;
        }
        else
        {
            Console.WriteLine("Invalid input please enter a NUMBER within the range.");
        }
    }
   
}

void DelistAPlant()
{
    Console.WriteLine("Enter the number of the plant you want to delist.");
    ListPlants(plants);


    while (true)
    {
        if (int.TryParse(Console.ReadLine(), out int plantIndex) && plantIndex >= 1 && plantIndex <= plants.Count)
        {
            plants.RemoveAt(plantIndex - 1);
            Console.WriteLine("Plant has been delisted.");
            break;

        }
        else
        {
            Console.WriteLine("Invalid input please enter a valid number.");
        }
    }
}

void SearchByLightNeeds()
{
    int lightNeedValue;
    while (true) 
    {
        Console.WriteLine("Enter a maxiumun light needs number between 1-5");
        if(int.TryParse(Console.ReadLine(), out lightNeedValue) && lightNeedValue >= 1 && lightNeedValue <=5)
        {
            List<Plant> plantsInSearchRange = plants.FindAll(p => p.LightNeeds == lightNeedValue);
            ListPlants(plantsInSearchRange);
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a number between 1 and 5.");
        }
    }
}

void StatsForExtraVert()
{
    Console.WriteLine($"Stats\r\n");

    //List<Plant> lowestPricedPlant = plants.OrderBy(p => p.AskingPrice).ToList();
    //Console.WriteLine($"Lowest price plant name: {lowestPricedPlant[0].Species}\r\n");
    Plant lowestPricedPlanttwo = plants.OrderBy(p => p.AskingPrice).FirstOrDefault();
    Console.WriteLine($"Lowest price plant name: {lowestPricedPlanttwo.Species}\r\n");

    Console.WriteLine(@$"Number of Plants Available:");
    List <Plant> avilablePlants = plants.Where(p => !p.Sold && p.AvilableUntil >= DateTime.Now).ToList();
    ListPlants(avilablePlants);
    Console.WriteLine("\n");

    Console.WriteLine($"Name of plant with highest light needs\r\n");
    Plant highestLightNeed = plants.OrderByDescending(p => p.LightNeeds).FirstOrDefault();
    Console.WriteLine($"The {highestLightNeed.Species} needs {highestLightNeed.LightNeeds} out of 5.");


    double averageLightNeed = plants.Average(p => p.LightNeeds);
    Console.WriteLine($@"Average light needs. " +
        $"The average light need is {averageLightNeed}\r\n");


    int totalPlants = plants.Count;
    int totalSoldPlants = plants.Count(p => p.Sold);
    double percentageAdopted = (double)totalSoldPlants / totalPlants * 100;
    Console.WriteLine($"Percentage of plants adopted: {percentageAdopted}%\n");
}

 //string PlantDetails(Plant plant)
//{
//    string plantString = $"A {plant.Species} in {plant.City} {(plant.Sold ? "was sold" : "is available")} for {plant.AskingPrice} dollars.";

//    return plantString;

//}