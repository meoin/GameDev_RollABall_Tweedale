using UnityEngine;

public class PetNameGenerator : MonoBehaviour
{
    private string[] petNames =
    {
        "Gooby",
        "Floobert",
        "Borbo",
        "Rolbort",
        "Jabob",
        "Umblo",
        "Tromboli",
        "Flibolo",
        "Elbo",
        "Wumbo",
        "Subbosubbo",
        "Bobobo-bo-bo-bobo",
        "Quimble",
        "Jebebiah",
        "Lullber",
        "Vibeo",
        "Bames",
        "Akimbo",
        "Bumbo",
        "Dimble",
        "Bababasheep",
        "Bogosbinted",
        "Bruh",
        "Beady",
        "B",
        "Cuobo",
        "Pibble",
        "Hamburber",
        "Xanber",
        "Zanber",
        "Barley",
        "Bichael",
        "Bam",
        "Basketbalb",
        "Brita",
        "Bucko",
        "Mambo#5",
        "Nubby",
        "Dribble",
        "Orbo",
        "Ibaab",
        "Ybebbe",
        "Habababbaba",
        "Bigchungus",
        "Bebe",
        "Bobama",
        "Boejiden",
        "Blump",
        "Weeble",
        "Bartizard",
        "Bodzilla",
        "Shabooboo",
        "Boo",
        "Beneleven",
        "Simob",
        "Jorbab",
        "Bristle",
        "Bzzzzzzt",
        "Bingle",
        "Frederick"
    };

    public string GetName() 
    {
        int rand = Random.Range(0, petNames.Length);

        return petNames[rand];
    }
}
