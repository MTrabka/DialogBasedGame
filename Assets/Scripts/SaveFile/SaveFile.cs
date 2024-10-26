using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public static class SaveFile
{
    private static string fileName = "dane.csv";

    public static bool WriteDataToCSV(List<string> data, out System.Exception exption)
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, fileName);

        try
        {
            // SprawdŸ, czy plik ju¿ istnieje
            bool fileExists = File.Exists(filePath);

            // Otwórz lub utwórz nowy plik CSV
            StreamWriter sw = new StreamWriter(filePath, append: fileExists, encoding: Encoding.UTF8);

            // Wygeneruj nag³ówki kolumn
            if (!fileExists)
            {
                string header = "Wybór1";
                for (int i = 2; i <= data.Count; i++)
                {
                    header += "," + "Wybór" + i;
                }
                sw.WriteLine(header);
            }

            // Zapisz dane do pliku CSV
            string line = string.Join(",", data);
            sw.WriteLine(line);

            // Zamknij plik
            sw.Close();

            Debug.Log("Dane zosta³y zapisane do pliku CSV na pulpicie.");
            exption = null;
            return true;
        }
        catch (System.Exception ex)
        {
            exption = ex;
            Debug.LogError("Wyst¹pi³ b³¹d podczas zapisywania do pliku CSV: " + ex.Message);
            return false;
        }
    }

    public static bool CheckIfCanWriteToFile(out System.Exception exption)
    {
        string desktopPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktopPath, fileName);

        try
        {
            // SprawdŸ, czy plik ju¿ istnieje
            bool fileExists = File.Exists(filePath);
            exption = null;
            if (!fileExists) return true; //Je¿eli plik nie istnieje -> wsio haraszo

            // Otwórz lub utwórz nowy plik CSV
            StreamReader sw = new StreamReader(filePath, encoding: Encoding.UTF8);
            
            return true;
        }
        catch (System.Exception ex)
        {
            exption = ex;
            Debug.LogError("Wyst¹pi³ b³¹d podczas zapisywania do pliku CSV: " + ex.Message);
            return false;
        }
    }
}
