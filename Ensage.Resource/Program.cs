using System;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace Ensage.Resource
{
    class Program
    {
        /// <summary>
        /// Собственно имя
        /// </summary>
        private static readonly string name = "Ensage.Resource";

        /// <summary>
        /// Собственный идентификатор -> Ensage.Resource,v=2016.5.22.15253
        /// </summary>
        private static readonly string himself = name + ",v=" + Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// Постфикс для файла блокировки обновления до пересборки
        /// </summary>
        private static readonly string LockFilename = name + ".firstrun";

        /// <summary>
        /// Приветствие -> Ensage.Resouce,v=2016.5.22.15253 loading...
        /// </summary>
        private static readonly string WelcomeMessage = himself+" loaded";

        /// <summary>
        /// Уведомление количестве обновленных файлов
        /// </summary>
        private static readonly string AmountOfUpdate = "> Installed or updated {0} file(s) in Dota2 directory";

        /// <summary>
        /// Уведомление количестве обновленных файлов
        /// </summary>
        private static readonly string DontNeedUpdate = "> Didnt find any files required for install or update T_T";


        /// <summary>
        /// Текст в случае пропуска обновления из-за файла блокировки обновления
        /// </summary>
        private static readonly string SkipForUpdate = "> Last update at {0}, if you want manually update again - just rebuild Ensage.Resource";

        /// <summary>
        /// Текст в случае невозможности определения последнего обновления сборки
        /// </summary>
        private static readonly string SkipForUpdateUnknown = "> Last update info is UNKNOWN, if you want manually update again - just rebuild Ensage.Resource";

        /// <summary>
        /// Сообщение о необходимости перезагрузки Dota2
        /// </summary>
        private static readonly string FinishUpdate = "> Please restart Dota2 for apply changes!";

        /// <summary>
        /// Сообщение если случился полный пиздец
        /// </summary>
        private static readonly string CannotUpdateFile = "! Cannot access to file rel \"{0}\", Exception \"{1}\"";

        /// <summary>
        /// Необходимо ли обновлять файлы?
        /// </summary>
        private static bool IsUpdateRequired = false;

        /// <summary>
        /// Определяет известна ли последняя версия
        /// </summary>
        private static bool IsVersionWellknown = false;

        /// <summary>
        /// Энумератор рекурсиного списка файлов в каталоге
        /// </summary>
        /// <param name="path">Путь для энумерации</param>
        /// <returns>Список всех файлов в каталоге как энумератор</returns>
        /// -> http://stackoverflow.com/questions/929276/how-to-recursively-list-all-the-files-in-a-directory-in-c
        static IEnumerable<string> GetFiles(string path)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                try
                {
                    foreach (string subDir in Directory.GetDirectories(path))
                    {
                        queue.Enqueue(subDir);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        yield return files[i];
                    }
                }
            }
        }

        /// <summary>
        /// Точка входа в приложение
        /// </summary>
        /// <param name="args">не используется</param>
        static void Main(string[] args)
        {

            // Сохраняем старые цвета
            var oldForeground = Console.ForegroundColor;
            var oldBackground = Console.BackgroundColor;

            // Пишем приветствие
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(WelcomeMessage);

            // извлекаем корневой каталог Dota2 - обычно это "C:\Program Files\Steam\SteamApps\common\dota 2 beta"
            var dota2_root = System.IO.Path.Combine(
                                System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName),
                                "..\\..\\.."
                             );

            // извлекаем корневой каталог Ensage.Resource - обычно это "Ensage.Release"
            var resource_root = System.IO.Path.Combine(BuildInfo.ProjectDir, "Resource");

            // извлекаем корневой катало для Ensage.Sandbox (нужен для файла лока)
            var sandbox_root = (resource_root.Contains("Repositories") && resource_root.Contains("trunk")) ? 
                (System.IO.Path.Combine(BuildInfo.ProjectDir, "..\\..\\..\\..")) :                                          // в случае если это GitRepo
                (System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LS57293A8F") // в случае если это LocalRepo
            );

            // собираем имя файла блокировки
            var lockname = System.IO.Path.Combine(sandbox_root, LockFilename);

            try // куда уж без него
            {
                // если файл блокировки существует, то пытаемся открыть
                if (System.IO.File.Exists(lockname))
                {
                    IsVersionWellknown = true;

                    // если содержимое файла как у текущей сборки
                    if(System.IO.File.ReadAllText(lockname) == BuildInfo.BuildStamp)
                    {
                        IsUpdateRequired = false;
                    } else
                    {
                        IsUpdateRequired = true;
                    }
                } else
                {
                    // если файла нету - запускаем обновление
                    IsUpdateRequired = true;
                    IsVersionWellknown = false;
                }
            } catch(Exception) {
                // если что-то пошло не так, то отменяем обновление
                IsUpdateRequired = false;
                IsVersionWellknown = false;
            }

            // если требуется обновление файлов
            if(IsUpdateRequired)
            {
                // получаем список файлов как энумератор
                var files = GetFiles(resource_root);

                // список файлов ожидающих обновления
                var pending = new Queue<string>();

                // перебираем файлы
                foreach(var f in files)
                {
                    // относительное имя файла
                    var rel = f.Substring(resource_root.Length); // -> \game\dota\materials\ensage_ui\emoticons\aaaah.vmat_c

                    // собираем имена файлов
                    var resource_file = resource_root + rel;
                    var dota2_file =    dota2_root + rel;

                    try
                    {
                        if(System.IO.File.Exists(resource_file)) // глупо, но стоит проверить не исчез ли файл оригинал
                        {
                            if(System.IO.File.Exists(dota2_file))
                            {
                                // если файл уже есть в каталоге Dota2 то проверяем хэш
                                if(MD5.ByFile(resource_file) != MD5.ByFile(dota2_file))
                                {
                                    // если хэши не совпали - то обновляем
                                    pending.Enqueue(rel);
                                } 
                            } else
                            {
                                // если файла нету, то добавляем в список для обновления
                                pending.Enqueue(rel);
                            }
                         }
                    } catch (Exception E) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(String.Format(CannotUpdateFile, rel, E.Message));
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }

                var pendingCount = pending.Count;

                // непосредственно проводим обновление по списку
                foreach(var rel in pending)
                {
                    try
                    {
                        // собираем имена файлов
                        var resource_file = resource_root + rel;
                        var dota2_file = dota2_root + rel;

                        // воссоздаем путь до директории если его нету
                        System.IO.Directory.CreateDirectory(System.IO.Path.GetDirectoryName(dota2_file));

                        // копируем файл через временный буфер
                        var buffer = System.IO.File.ReadAllBytes(resource_file);
                        System.IO.File.WriteAllBytes(dota2_file, buffer);

                    } catch (Exception E) {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(String.Format(CannotUpdateFile, rel, E.Message));
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                }

                // количество фактически обновленных файлов > 0 выводим сообщение
                if (pendingCount > 0)
                {
                    // количество обновленных файлов
                    Console.WriteLine(String.Format(AmountOfUpdate, pendingCount));
                    // сообщение об необходимости перезагрузки
                    Console.WriteLine(FinishUpdate);
                } else
                {
                    // :'(
                    Console.WriteLine(DontNeedUpdate);
                }

                // записываем файл блокировки после успешного обновления
                System.IO.File.WriteAllText(lockname, BuildInfo.BuildStamp);

            } else
            {
                // если версия прошлая известна
                if(IsVersionWellknown)
                {
                    // -> Last update at 22.05.2016 21:38:03, if you want manually update again - just rebuild Ensage.Resource
                    Console.WriteLine(String.Format(SkipForUpdate, System.IO.File.ReadAllText(lockname)));
                } else {
                    // -> Last update info is UNKNOWN, if you want manually update again - just rebuild Ensage.Resource
                    Console.WriteLine(SkipForUpdateUnknown);
                }

            }

            // восстанавливаем прошлые цвета
            Console.ForegroundColor = oldForeground;
            Console.BackgroundColor = oldBackground;
            Console.ResetColor(); // чтобы не ломать консоль для других скриптов

        }
    }
}
