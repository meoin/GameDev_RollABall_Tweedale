using UnityEngine;

public class PetRegistry : MonoBehaviour
{
    public PetDetails[,] allPets = {
        {
            new PetDetails("Gary", 20, 40, "Grey"),
            new PetDetails("Mary", 30, 25, "Tan"),
            new PetDetails("Barry", 35, 20, "Blue"),
            new PetDetails("Jerry", 45, 10, "Green"),
            new PetDetails("Larry", 60, 5, "Yellow")
        },
        {
            new PetDetails("Fanta", 170, 45, "Orange"),
            new PetDetails("Sprite", 250, 25, "Green"),
            new PetDetails("Pepsi", 300, 18, "Cyan"),
            new PetDetails("Grape", 400, 9, "Purple"),
            new PetDetails("Creamsoda", 500, 3, "Pink")
        },
        {
            new PetDetails("Moon", 1000, 40, "Grey"),
            new PetDetails("Mars", 1750, 25, "Red"),
            new PetDetails("Saturn", 2500, 20, "Yellow"),
            new PetDetails("Neptune", 3800, 10, "Blue"),
            new PetDetails("Venus", 5000, 5, "Tan")
        },
        {
            new PetDetails("Sasquatch", 7500, 40, "Orange"),
            new PetDetails("Unicorn", 10000, 25, "Pink"),
            new PetDetails("Zilla", 12500, 20, "Green"),
            new PetDetails("Yeti", 15000, 10, "White"),
            new PetDetails("Mothman", 22500, 5, "Black")
        },
        {
            new PetDetails("Buddy", 20, 40, "Grey"),
            new PetDetails("Jimmy", 30, 25, "Tan"),
            new PetDetails("Bobby", 35, 20, "Blue"),
            new PetDetails("Freddy", 45, 10, "Green"),
            new PetDetails("Larry", 60, 5, "Yellow")
        }
    };
}
