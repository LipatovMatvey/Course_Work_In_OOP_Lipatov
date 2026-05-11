using System;
using System.Collections.Generic;
using System.Configuration;
using Npgsql;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Course_work_in_OOP_Lipatov
{
    public class DataBaseManager
    {
        /// <summary>
        /// Строка подключения к текущей выбранной базе данных
        /// </summary>
        private string? connectionString;

        /// <summary>
        /// Строка подключения к системной базе данных postgres
        /// </summary>
        private string? masterConnectionString;

        /// <summary>
        /// Имя текущей выбранной базы данных
        /// </summary>
        private string? currentDatabaseName;

        /// <summary>
        /// Регулярное выражение для проверки корректности имени базы данных при создании
        /// </summary>
        private static readonly Regex dbNameRegex = new Regex(@"^(?![\s\d]).+", RegexOptions.Compiled);

        /// <summary>
        /// Конструктор менеджера
        /// </summary>
        public DataBaseManager()
        {
            LoadConnectionSettings();
        }

        /// <summary>
        /// Загружает строку подключения к системной БД postgres из файла конфигурации
        /// </summary>
        /// <exception cref="Exception">Выбрасывается, если в конфигурации отсутствует строка MasterDB</exception>
        private void LoadConnectionSettings()
        {
            var masterSetting = ConfigurationManager.ConnectionStrings["MasterDB"];
            if (masterSetting == null || string.IsNullOrEmpty(masterSetting.ConnectionString))
            {
                throw new Exception("В файле конфигурации отсутствует строка подключения MasterDB.");
            }
            masterConnectionString = masterSetting.ConnectionString;
            currentDatabaseName = null;
        }

        /// <summary>
        /// Проверяет, выбрана ли в данный момент какая-либо база данных
        /// </summary>
        /// <returns>true, если база данных выбрана; иначе false</returns>
        private bool EnsureDatabaseSelected()
        {
            if (string.IsNullOrEmpty(currentDatabaseName))
            {
                MessageBox.Show("Сначала выберите или создайте базу данных!",
                    "База не выбрана", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Строит строку подключения к текущей базе данных на основе параметров из masterConnectionString и имени текущей БД
        /// </summary>
        private void BuildConnectionString()
        {
            if (string.IsNullOrEmpty(currentDatabaseName))
            {
                connectionString = null;
                return;
            }

            var builder = new NpgsqlConnectionStringBuilder(masterConnectionString);
            string? host = builder.Host;
            int? port = builder.Port;
            string? username = builder.Username;
            string? password = builder.Password;
            connectionString = $"Host={host};Username={username};Password={password};Database={currentDatabaseName};Port={port}";
        }

        /// <summary>
        /// Инициализирует структуру текущей базы данных: создаёт таблицу patients, если она не существует
        /// </summary>
        /// <exception cref="Exception">Выбрасывается, если БД не существует или произошла другая ошибка</exception>
        private void InitializeDatabase()
        {
            if (!EnsureDatabaseSelected()) return;

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string createTableQuery = @"
                        CREATE TABLE IF NOT EXISTS patients (
                            id SERIAL PRIMARY KEY,
                            full_name VARCHAR(100) NOT NULL,
                            age INTEGER NOT NULL,
                            gender VARCHAR(20) NOT NULL,
                            disease VARCHAR(100) NOT NULL,
                            severity VARCHAR(20) NOT NULL,
                            duration INTEGER NOT NULL,
                            department VARCHAR(50) NOT NULL
                        )";
                    using (var cmd = new NpgsqlCommand(createTableQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (NpgsqlException ex) when (ex.Message.Contains("3D000") || ex.Message.Contains("does not exist"))
            {
                throw new Exception($"База данных '{currentDatabaseName}' не существует. Создайте её через управление БД.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка инициализации БД: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Устанавливает текущую базу данных по имени и перестраивает строку подключения
        /// </summary>
        /// <param name="dbName">Имя базы данных, которую нужно сделать текущей</param>
        public void SetCurrentDatabase(string? dbName)
        {
            currentDatabaseName = dbName;
            BuildConnectionString();
        }

        /// <summary>
        /// Возвращает список всех доступных баз данных
        /// </summary>
        /// <returns>Список имён баз данных</returns>
        public List<string> GetAllDatabases()
        {
            var databases = new List<string>();
            try
            {
                using (var conn = new NpgsqlConnection(masterConnectionString))
                {
                    conn.Open();
                    string query = "SELECT datname FROM pg_database WHERE datistemplate = false AND datname NOT IN ('postgres') ORDER BY datname";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            databases.Add(reader.GetString(0));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка получения списка БД: {ex.Message}");
            }
            return databases;
        }

        /// <summary>
        /// Создаёт новую базу данных с указанным именем, переключается на неё и инициализирует таблицы
        /// </summary>
        /// <param name="dbName">Имя создаваемой базы данных</param>
        public void CreateDatabase(string dbName)
        {
            if (string.IsNullOrWhiteSpace(dbName) || !dbNameRegex.IsMatch(dbName))
            {
                MessageBox.Show("Некорректное имя базы данных. Имя не должно начинаться с пробела или цифры и не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (var conn = new NpgsqlConnection(masterConnectionString))
                {
                    conn.Open();
                    string createDbQuery = $"CREATE DATABASE \"{dbName}\"";
                    using (var cmd = new NpgsqlCommand(createDbQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                SetCurrentDatabase(dbName);
                InitializeDatabase();
                MessageBox.Show($"База данных \"{dbName}\" успешно создана.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка создания БД: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удаляет указанную базу данных. Невозможно удалить текущую активную базу
        /// </summary>
        /// <param name="dbName">Имя удаляемой базы данных</param>
        public void DeleteDatabase(string dbName)
        {
            if (dbName.Equals(currentDatabaseName, StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Нельзя удалить текущую активную базу данных. Сначала переключитесь на другую.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (var conn = new NpgsqlConnection(masterConnectionString))
                {
                    conn.Open();
                    string terminateQuery = $@"
                        SELECT pg_terminate_backend(pg_stat_activity.pid)
                        FROM pg_stat_activity
                        WHERE pg_stat_activity.datname = '{dbName}' AND pid <> pg_backend_pid()";
                    using (var cmd = new NpgsqlCommand(terminateQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    string dropQuery = $"DROP DATABASE \"{dbName}\"";
                    using (var cmd = new NpgsqlCommand(dropQuery, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show($"База данных \"{dbName}\" удалена.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления БД: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Переключается на указанную базу данных и инициализирует её структуру
        /// </summary>
        /// <param name="dbName">Имя базы данных для переключения</param>
        /// <returns>true, если переключение выполнено успешно; иначе false</returns>
        public bool SwitchToDatabase(string? dbName)
        {
            try
            {
                SetCurrentDatabase(dbName);
                InitializeDatabase();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка переключения на БД {dbName}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Проверяет, существует ли в текущей базе данных пациент с точно такими же данными
        /// </summary>
        /// <param name="patient">Проверяемый пациент</param>
        /// <returns>true, если дубликат найден; иначе false</returns>
        public bool IsDuplicate(Patient patient)
        {
            if (!EnsureDatabaseSelected())
            {
                return false;
            }
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    var query = @"
                        SELECT COUNT(*) FROM patients
                        WHERE COALESCE(TRIM(LOWER(full_name)),'') = COALESCE(TRIM(LOWER(@fullName)),'')
                          AND age = @age
                          AND COALESCE(TRIM(LOWER(gender)),'') = COALESCE(TRIM(LOWER(@gender)),'')
                          AND COALESCE(TRIM(LOWER(disease)),'') = COALESCE(TRIM(LOWER(@disease)),'')
                          AND duration = @duration
                          AND COALESCE(TRIM(LOWER(severity)),'') = COALESCE(TRIM(LOWER(@severity)),'')
                          AND COALESCE(TRIM(LOWER(department)),'') = COALESCE(TRIM(LOWER(@department)),'')";

                    if (patient.Id > 0)
                    {
                        query += " AND id <> @id";
                    }
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@fullName", patient.FullName ?? string.Empty);
                        cmd.Parameters.AddWithValue("@age", patient.Age);
                        cmd.Parameters.AddWithValue("@gender", patient.Gender ?? string.Empty);
                        cmd.Parameters.AddWithValue("@disease", patient.Disease ?? string.Empty);
                        cmd.Parameters.AddWithValue("@duration", patient.Duration);
                        cmd.Parameters.AddWithValue("@severity", patient.Severity ?? string.Empty);
                        cmd.Parameters.AddWithValue("@department", patient.Department ?? string.Empty);
                        if (patient.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@id", patient.Id);
                        }
                        var result = cmd.ExecuteScalar();
                        if (result != null && int.TryParse(result.ToString(), out int count))
                        {
                            return count > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке дубликатов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        /// <summary>
        /// Возвращает список всех пациентов из текущей базы данных
        /// </summary>
        /// <returns>Список пациентов</returns>
        public List<Patient> GetAllPatients()
        {
            var patients = new List<Patient>();
            if (!EnsureDatabaseSelected())
            {
                return patients;
            }
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("SELECT * FROM patients ORDER BY id", conn))
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            patients.Add(new Patient
                            {
                                Id = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Age = reader.GetInt32(2),
                                Gender = reader.GetString(3),
                                Disease = reader.GetString(4),
                                Severity = reader.GetString(5),
                                Duration = reader.GetInt32(6),
                                Department = reader.GetString(7)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки пациентов: {ex.Message}");
            }
            return patients;
        }

        /// <summary>
        /// Добавляет нового пациента в текущую базу данных
        /// </summary>
        /// <param name="patient">Данные добавляемого пациента</param>
        public void AddPatient(Patient patient)
        {
            if (!EnsureDatabaseSelected())
            {
                return;
            }
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(
                        "INSERT INTO patients (full_name, age, gender, disease, severity, duration, department) " +
                        "VALUES (@fullName, @age, @gender, @disease, @severity, @duration, @department) RETURNING id", conn))
                    {
                        cmd.Parameters.AddWithValue("@fullName", patient.FullName);
                        cmd.Parameters.AddWithValue("@age", patient.Age);
                        cmd.Parameters.AddWithValue("@gender", patient.Gender);
                        cmd.Parameters.AddWithValue("@disease", patient.Disease);
                        cmd.Parameters.AddWithValue("@severity", patient.Severity);
                        cmd.Parameters.AddWithValue("@duration", patient.Duration);
                        cmd.Parameters.AddWithValue("@department", patient.Department);
                        var newId = cmd.ExecuteScalar();
                        MessageBox.Show($"Пациент добавлен успешно! ID: {newId}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка добавления пациента: {ex.Message}");
            }
        }

        /// <summary>
        /// Обновляет данные существующего пациента в текущей базе данных
        /// </summary>
        /// <param name="patient">Пациент с обновлёнными данными</param>
        public void UpdatePatient(Patient patient)
        {
            if (!EnsureDatabaseSelected())
            {
                return;
            }
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand(
                        "UPDATE patients SET full_name=@fullName, age=@age, gender=@gender, " +
                        "disease=@disease, severity=@severity, duration=@duration, " +
                        "department=@department WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", patient.Id);
                        cmd.Parameters.AddWithValue("@fullName", patient.FullName);
                        cmd.Parameters.AddWithValue("@age", patient.Age);
                        cmd.Parameters.AddWithValue("@gender", patient.Gender);
                        cmd.Parameters.AddWithValue("@disease", patient.Disease);
                        cmd.Parameters.AddWithValue("@severity", patient.Severity);
                        cmd.Parameters.AddWithValue("@duration", patient.Duration);
                        cmd.Parameters.AddWithValue("@department", patient.Department);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Данные пациента обновлены успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления пациента: {ex.Message}");
            }
        }

        /// <summary>
        /// Удаляет пациента по идентификатору из текущей базы данных
        /// </summary>
        /// <param name="id">Идентификатор удаляемого пациента</param>
        public void DeletePatient(int id)
        {
            if (!EnsureDatabaseSelected()) return;

            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var cmd = new NpgsqlCommand("DELETE FROM patients WHERE id=@id", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                            MessageBox.Show("Пациент удален успешно!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка удаления пациента: {ex.Message}");
            }
        }

        /// <summary>
        /// Выполняет поиск пациентов по частичному совпадению ФИО, диагноза или отделения
        /// </summary>
        /// <param name="searchText">Текст поискового запроса</param>
        /// <returns>Список пациентов, удовлетворяющих условию</returns>
        public List<Patient> SearchPatients(string searchText)
        {
            var patients = new List<Patient>();
            if (!EnsureDatabaseSelected()) return patients;
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT * FROM patients 
                        WHERE full_name ILIKE @search 
                           OR disease ILIKE @search 
                           OR department ILIKE @search
                        ORDER BY id";
                    using (var cmd = new NpgsqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@search", $"%{searchText}%");
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                patients.Add(new Patient
                                {
                                    Id = reader.GetInt32(0),
                                    FullName = reader.GetString(1),
                                    Age = reader.GetInt32(2),
                                    Gender = reader.GetString(3),
                                    Disease = reader.GetString(4),
                                    Severity = reader.GetString(5),
                                    Duration = reader.GetInt32(6),
                                    Department = reader.GetString(7)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка поиска: {ex.Message}");
            }
            return patients;
        }

        /// <summary>
        /// Удаляет всех пациентов из текущей базы данных и сбрасывает счётчик идентификаторов
        /// </summary>
        /// <exception cref="Exception">Выбрасывается при ошибке очистки</exception>
        public void ClearAllPatients()
        {
            if (!EnsureDatabaseSelected()) return;
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var transaction = conn.BeginTransaction())
                    {
                        using (var cmd = new NpgsqlCommand("DELETE FROM patients", conn, transaction))
                        {
                            int deletedCount = cmd.ExecuteNonQuery();
                            using (var resetCmd = new NpgsqlCommand("ALTER SEQUENCE patients_id_seq RESTART WITH 1", conn, transaction))
                            {
                                resetCmd.ExecuteNonQuery();
                            }
                            transaction.Commit();
                            MessageBox.Show($"Удалено пациентов: {deletedCount}", "Очистка выполнена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при очистке базы данных: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Показывает, выбрана ли в данный момент какая-либо база данных
        /// </summary>
        public bool HasDatabaseSelected => !string.IsNullOrEmpty(currentDatabaseName);
    }
}