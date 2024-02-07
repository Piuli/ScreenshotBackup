using System.Reflection;
using System.Resources;

ResourceManager rm = new ResourceManager("ScreenshotBackup.Properties.Resources", Assembly.GetExecutingAssembly());
string destinationPath = rm.GetString("DestinationPath");

string path = Directory.GetCurrentDirectory();

string sourcePath = @"C:\Users\" + Environment.UserName + @"\Desktop";

string fileName = "";
string destFile = "";

string prefix = "Maple_";

int count = 0;

StreamWriter sw = new StreamWriter(destinationPath + "\\log.txt", true);
sw.WriteLine(DateTime.Now + ": Backup started");

try
{
    string[] files = Directory.GetFiles(sourcePath);
    foreach (string file in files)
    {
        fileName = Path.GetFileName(file);
        bool isScreenshot = fileName.StartsWith(prefix);

        if (isScreenshot)
        {
            destFile = Path.Combine(destinationPath, fileName);
            if (!File.Exists(destFile))
            {
                File.Copy(file, destFile, false);
                sw.WriteLine(DateTime.Now + ": " + fileName + " copied");
                File.Delete(file);

                count++;
            }
            else
            {
                sw.WriteLine(DateTime.Now + ": " + fileName + " already exists");
            }
        }

    }

    sw.WriteLine(DateTime.Now + ": " + count + " screenshots transferred");
    sw.WriteLine(DateTime.Now + ": Backup finished ");
    sw.Close();

}
catch (Exception ex)
{
    sw.WriteLine(DateTime.Now + ": " + ex.Message);
}

