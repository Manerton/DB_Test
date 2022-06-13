using System.Diagnostics;
using System.Text;

namespace DB_Test.Extend
{
    public class BackUpRestore
    {
        private string host = "localhost";
        private string port = "5432";
        private string database = "ResultTests";
        private string user = "postgres";
        private string password = "root";

        public string Restore(IConfiguration config, string inFile)
        {
            if (!File.Exists(inFile))
                return "Резервная копия по адресу (" + inFile + ") не найдена";

            String dumpCommand = "\"" + config["pgRestore"] + "\"" + " -h " + host + " -p " + port + " -c -d " + database + " -U " + user + "";
            String passFileContent = "" + host + ":" + port + ":" + database + ":" + user + ":" + password + "";

            String batFilePath = Path.Combine(
                Environment.CurrentDirectory + "\\backUp",
                Guid.NewGuid().ToString() + ".bat");

            String passFilePath = Path.Combine(
                Environment.CurrentDirectory + "\\backUp",
                Guid.NewGuid().ToString() + ".conf");

            String batchContent = "";
            batchContent += "@" + "set PGPASSFILE=" + passFilePath + "\n";
            batchContent += "@" + dumpCommand + " \"" + inFile + "\"" + "\n";

            PerformOperation(batFilePath, batchContent, passFilePath, passFileContent);
            return "База данных успешно восстановлена";
        }

        public string BackUp(IConfiguration config, string outFile)
        {
            
            String dumpCommand = "\"" + config["pgDump"] + "\"" + " -Fc" + " -h " + host + " -p " + port + " -d " + database + " -U " + user + "";
            String passFileContent = "" + host + ":" + port + ":" + database + ":" + user + ":" + password + "";

            outFile = Environment.CurrentDirectory + "\\backUp\\" + outFile + ".sql";

            String batFilePath = Path.Combine(
                Environment.CurrentDirectory + "\\backUp",
                Guid.NewGuid().ToString() + ".bat");

            String passFilePath = Path.Combine(
                Environment.CurrentDirectory + "\\backUp",
                Guid.NewGuid().ToString() + ".conf");

            String batchContent = "";
            batchContent += "@" + "set PGPASSFILE=" + passFilePath + "\n";
            batchContent += "@" + dumpCommand + "  > " + "\"" + outFile + "\"" + "\n";

            PerformOperation(batFilePath, batchContent, passFilePath, passFileContent);

            if(File.Exists(outFile))
                return "Резервная копия сохранена по адресу - " + outFile;
            return "Ошибка при создании резервной копии";
            
        }

        private void PerformOperation(String batFilePath, String batchContent, String passFilePath, String passFileContent)
        {
            try
            {
                File.WriteAllText(
                    batFilePath,
                    batchContent,
                    Encoding.ASCII);

                File.WriteAllText(
                    passFilePath,
                    passFileContent,
                    Encoding.ASCII);

                ProcessStartInfo oInfo = new ProcessStartInfo(batFilePath);
                oInfo.UseShellExecute = false;
                oInfo.CreateNoWindow = true;

                using (Process proc = System.Diagnostics.Process.Start(oInfo))
                {
                    proc.WaitForExit();
                    proc.Close();
                }
            }
            finally
            {
                if (File.Exists(batFilePath))
                    File.Delete(batFilePath);

                if (File.Exists(passFilePath))
                    File.Delete(passFilePath);
            }
        }

    }
}
