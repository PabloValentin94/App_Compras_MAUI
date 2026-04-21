using SQLite;

namespace App_Compras_MAUI.Helper
{
    public abstract class SQLite
    {
        protected static SQLiteAsyncConnection? manager = null;

        public SQLite()
        {
            string database_file_path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "database.db3");

            manager = new SQLiteAsyncConnection(database_file_path);

            System.Diagnostics.Debug.WriteLine(database_file_path);
        }

        protected void DiscardConnection()
        {
            manager = null;
        }
    }
}
