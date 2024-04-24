using System;
using System.IO;

class CratesRearranger
{
    public static List<LinkedList<string>> rearrangeCargo(string inputFile)
    {
        //stacksList -> lista care va contine toate stack-urile de crate-uri
        List<LinkedList<string>> stacksList = new List<LinkedList<string>>();

        try
        {
            using (StreamReader streamReader = new StreamReader(inputFile))
            {
                string line;

                //In cadrul acestui while se executa prelucrarea input-ului pentru a stoca fiecare crate in "stack-ul" corespunzator.
                while ((line = streamReader.ReadLine()) != null)
                {
                    /*Conditia verifica daca am terminat de citit crate-urile. Daca {line[1]} este un numar inseamna ca am ajuns la linia: "1   2   3"
                     *  [D]    
                        [N] [C]    
                        [Z] [M] [P]
                         1   2   3  <- linia la care conditia este adevarata si se executa instructiunea break, iesind din instructiunea de ciclare
                     */
                    if (char.IsNumber(line[1]))
                    {
                        break;
                    }
                    /*i += 4 deoarece in fiecare linie de input, diferenta dintre index-ul unde incepe eticheta unui crate(Ex: [A]) si index-ul unde incepe eticheta urmatorului crate este 4.*/
                    for (int i = 0; i < line.Length; i += 4)
                    {
                        //Variabila {subString} va avea una din cele 2 valori: 1.Eticheta unui crate. Ex: "[G]" | 2.Un string alcatuit din 3 spatii. Ex: "   "//
                        string subString;

                        subString = line.Substring(i, 3);
                        //Verificare daca in subString este stocata eticheta unui crate
                        if (subString[0] != ' ')
                        {
                            //{stackIndex} reprezinta index-ul stack-ul caruia ii apartine crate-ul curent.I-ul este incrementat cu 4, astfel prin impartire la 4 obtinem index-ul dorit.
                            int stackIndex = i / 4;

                            while (stacksList.Count <= stackIndex)
                            {
                                stacksList.Add(new LinkedList<string>());
                            }

                            stacksList[stackIndex].AddLast(subString);
                        }
                    }
                }

                while ((line = streamReader.ReadLine()) != null) {

                    if (line.StartsWith("move"))
                    {
                        string[] tokens = line.Split();

                        int numOfCratesToMove, fromIndex, toIndex;

                        numOfCratesToMove = Int32.Parse(tokens[1]); //{numOfCratesToMove} reprezinta numarul de crate-uri ce vor fi mutate

                        fromIndex = int.Parse(tokens[3]) - 1; //{fromIndex} reprezinta index-ul stack-ului de pe care vom lua crate-uri

                        toIndex = int.Parse(tokens[5]) - 1; //{toIndex} reprezinta index-ul stack-ului unde vom pune crate-uri

                        for (int i = 0; i < numOfCratesToMove; i++)
                        {
                            if (stacksList[fromIndex].Count > 0)
                            {
                                stacksList[toIndex].AddFirst(stacksList[fromIndex].First());
                                stacksList[fromIndex].RemoveFirst();
                            } else
                            {
                                break;
                            }
                        }
                    }

                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return stacksList;
    }

    public static string getCratesOnTop(List<LinkedList<string>> stacksList)
    {
        string finalAnswer = "";

        foreach (var list in stacksList)
        {
            if (list.Count > 0)
                finalAnswer += (list.First()[1]);
        }

        return finalAnswer;
    }

    static void Main(string[] args)
    {
        List<LinkedList<string>> finalStacks = rearrangeCargo("D:\\Facultate\\CaphyonInternship\\GiantCargoCrane\\GiantCargoCrane\\input.txt");

        Console.WriteLine(getCratesOnTop(finalStacks));
    }
}
