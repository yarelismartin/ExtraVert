// See https://aka.ms/new-console-template for more information
using ExtraVert;
using System.ComponentModel.DataAnnotations;

string greeting = "Welcome to ExtraVert!";

Console.WriteLine(@greeting);

List<Plant> plants = new List<Plant>()
{
    new Plant("Tulip", 2, 15.50M, "Los Angeles", 90001, true),
    new Plant("Rose", 3, 25.99m, "New York", 10001, false),
    new Plant("Orchid", 4, 35.75m, "Miami", 33101, false),
    new Plant("Fern", 2, 10.25m, "Seattle", 98101, true),
    new Plant("Sunflower", 5, 19.99m, "Chicago", 60601, false),
};

string choice = null;
while (choice != "0")
{
    Console.WriteLine(@" Chose an Option:
      0. Exit
      1. Display all plants
      2. Post a plant to be adopted
      3. Adopt a plant
      4. Delist a plant");
    choice = Console.ReadLine();
    if (choice == "0")
    {
        Console.WriteLine("Goodbye!");
        Console.Clear();
    }
    else if (choice == "1")
    {
        ListPlants();
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
    else
    {
        Console.WriteLine("Invalid option. Please choose a valid option from the list.");
        Console.Clear();
    }
}




    void ListPlants()
    {
        for (int i = 0; i < plants.Count; i++)
        {
        Plant plant = plants[i];
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

    bool plantSold = false;

    Plant newPlant = new Plant(plantSpecies, plantLightNeeds, plantPrice, plantCity, plantZIP, plantSold);

    plants.Add(newPlant);

    Console.WriteLine("Plant has been posted successfully!");

}


void AdobtAPlant()

    {
    Console.WriteLine("Your adobtion journey starts here! Choose one of the following plants to adopt: ");
    List<Plant> avilablePlants = plants.Where(p => !p.Sold).ToList();


    //if (availablePlants.Count == 0)
    if (!avilablePlants.Any()) 
    {
        Console.WriteLine("No plants are avilable for adoption :( ");
        return;

    }

    for (int i = 0; i < avilablePlants.Count; i++)
    {
        Plant plant = avilablePlants[i];
        Console.WriteLine($"{i + 1}. A {plant.Species} in {plant.City} is available for {plant.AskingPrice} dollars.");
    }
   
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
    ListPlants();


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